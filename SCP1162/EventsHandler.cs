using CustomPlayerEffects;
using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments;
using InventorySystem.Items.Firearms.Modules;
using InventorySystem.Items.Usables.Scp330;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using MapGeneration;
using Mirror;
using PlayerRoles;
using System;
using System.Linq;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;
using PrimitiveObjectToy = AdminToys.PrimitiveObjectToy;
using Random = UnityEngine.Random;

namespace SCP1162
{
    public class EventsHandler
    {
        public static bool IsScp1162Spawn => _scp1162 != null;

        private static PrimitiveObjectToy _scp1162;
        private static Vector3 _scp1162Position;

        public void OnWaitingForPlayers()
        {
            if (Plugin.Instance.Config.Scp1162ChanceToSpawn >= Random.Range(0, 100))
                OnSpawnSCP1162();
        }

        public void OnPlayerDroppingItem(PlayerDroppingItemEventArgs ev)
        {
            if (!Round.IsRoundStarted)
                return;
            ev.IsAllowed = OnUseSCP1162(ev.Player, ev.Item.Base, ev.Player.CurrentItem == ev.Item);
        }

        private bool OnUseSCP1162(Player player, ItemBase item, bool isThrow)
        {
            if (!IsScp1162Spawn || !Round.IsRoundStarted || Vector3.Distance(_scp1162Position, player.Position) > Plugin.Instance.Config.Scp1162Distance || item is Scp330Bag || (player.Role == RoleTypeId.Scp3114 && Plugin.Instance.Config.IgnoreScp3114))
                return true;

            if (Plugin.Instance.Config.CuttingChance > 0 && (!isThrow || !Plugin.Instance.Config.IgnoreThrow))
            {
                if (Plugin.Instance.Config.CuttingChance >= Random.Range(0, 100))
                {
                    player.EnableEffect<SeveredHands>();
                    return true;
                }
            }

            if (Plugin.Instance.Config.DeleteChance >= Random.Range(1, 101))
            {
                player.RemoveItem(item);
                player.SendHint(Plugin.Instance.Config.ItemDeleteMessage.Message
                    .Replace("{dropitem}", GetItemName(item.ItemTypeId))
                    .Replace("{dropstatus}", isThrow ? Plugin.Instance.Config.ItemDeleteMessage.ThrowText : Plugin.Instance.Config.ItemDeleteMessage.DropText), 2f);
                return false;
            }

            var newItemType = Plugin.Instance.Config.DroppingItems.RandomItem();
            player.SendHint(Plugin.Instance.Config.ItemDropMessage.Message
                .Replace("{dropitem}", GetItemName(item.ItemTypeId))
                .Replace("{giveitem}", GetItemName(newItemType))
                .Replace("{dropstatus}", isThrow ? Plugin.Instance.Config.ItemDropMessage.ThrowText : Plugin.Instance.Config.ItemDropMessage.DropText), 2f);

            player.RemoveItem(item);
            ItemBase newItem = player.AddItem(newItemType).Base;

            if (newItem is Firearm firearm && (Plugin.Instance.Config.FillMaxAmmo || Plugin.Instance.Config.RandomAttachments))
            {
                if (Plugin.Instance.Config.RandomAttachments)
                    firearm.ApplyAttachmentsCode(AttachmentsUtils.GetRandomAttachmentsCode(firearm.ItemTypeId), reValidate: true);

                if (Plugin.Instance.Config.FillMaxAmmo && firearm.TryGetModule<MagazineModule>(out MagazineModule magazineModule))
                {
                    magazineModule.ServerInsertEmptyMagazine();
                    magazineModule.AmmoStored = magazineModule.AmmoMax;
                    magazineModule.ServerResyncData();
                }
            }

            return false;
        }

        private string GetItemName(ItemType itemType)
        {
            if (Plugin.Instance.Config.ItemsName.TryGetValue(itemType, out string itemName)) return itemName;
            return itemType.ToString();
        }

        public static void OnSpawnSCP1162()
        {
            if (_scp1162 != null) return;

            CustomRoomLocationData crldTemp = Plugin.Instance.Config.CustomRoomLocations.RandomItem();
            RoomIdentifier room = null;

            if (Enum.TryParse<RoomName>(crldTemp.RoomNameType, out RoomName roomName)) room = RoomIdentifier.AllRoomIdentifiers.First(x => x.Name == roomName);
            else room = RoomIdentifier.AllRoomIdentifiers.First(x => x.name.Contains(crldTemp.RoomNameType));

            if (room == null)
            {
                Logger.Warn($"Room not found for SCP-1162 spawn: {crldTemp.RoomNameType}");
                return;
            }

            GameObject point = new GameObject("Point-1162");
            point.transform.SetParent(room.transform);
            point.transform.localPosition = new Vector3(crldTemp.OffsetX, crldTemp.OffsetY, crldTemp.OffsetZ);
            point.transform.localRotation = Quaternion.Euler(crldTemp.RotationX, crldTemp.RotationY, crldTemp.RotationZ);

            _scp1162 = new SimplifiedToy(PrimitiveType.Cylinder, point.transform.position, point.transform.eulerAngles,
                new Vector3(1.3f, 0.1f, 1.3f), Color.black, null, 0.95f).Spawn();

            GameObject.Destroy(point);
            _scp1162Position = _scp1162.transform.position;
        }

        public static void OnDespawnSCP1162()
        {
            if (_scp1162 == null) return;

            NetworkServer.Destroy(_scp1162.gameObject);
            _scp1162 = null;
        }
    }
}