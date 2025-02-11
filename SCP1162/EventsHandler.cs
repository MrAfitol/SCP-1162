using AdminToys;
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
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SCP1162
{
    public class EventsHandler
    {
        private static Vector3 Scp1162Position;
        private static PrimitiveObjectToy Scp1162;

        public static bool IsScp1162Spawn
        {
            get => Scp1162 != null;
        }

        public void OnWaitingForPlayers()
        {
            if (Plugin.Instance.Config.Scp1162ChanceToSpawn >= Random.Range(0, 100))
                OnSpawnSCP1162();
        }

        public void OnPlayerDroppingItem(PlayerDroppingItemEventArgs ev) => ev.IsAllowed = OnUseSCP1162(ev.Player, ev.Item.Base, ev.Player.CurrentItem == ev.Item);

        private bool OnUseSCP1162(Player player, ItemBase item, bool isThrow)
        {
            if (!IsScp1162Spawn || !Round.IsRoundStarted || Vector3.Distance(Scp1162Position, player.Position) > Plugin.Instance.Config.Scp1162Distance || item is Scp330Bag) return true;

            if (isThrow ? !Plugin.Instance.Config.IgnoreThrow && Plugin.Instance.Config.CuttingChance > 0 : Plugin.Instance.Config.CuttingChance > 0)
            {
                if (Plugin.Instance.Config.CuttingChance >= Random.Range(0, 100))
                {
                    player.EnableEffect<SeveredHands>();
                    return true;
                }
            }

            if (Plugin.Instance.Config.DeleteChance > 0)
            {
                if (Plugin.Instance.Config.DeleteChance >= Random.Range(0, 100))
                {
                    player.RemoveItem(item);
                    player.SendHint(Plugin.Instance.Config.ItemDeleteMessage.Message
                        .Replace("{dropitem}", GetItemName(item.ItemTypeId))
                        .Replace("{dropstatus}", isThrow ? Plugin.Instance.Config.ItemDeleteMessage.ThrowText : Plugin.Instance.Config.ItemDeleteMessage.DropText), 2f);
                    return false;
                }
            }

            var newItemType = Plugin.Instance.Config.DroppingItems.RandomItem();

            player.SendHint(Plugin.Instance.Config.ItemDropMessage.Message
                        .Replace("{dropitem}", GetItemName(item.ItemTypeId))
                        .Replace("{giveitem}", GetItemName(newItemType))
                        .Replace("{dropstatus}", isThrow ? Plugin.Instance.Config.ItemDropMessage.ThrowText : Plugin.Instance.Config.ItemDropMessage.DropText), 2f);
            player.RemoveItem(item);

            ItemBase NewItem = player.AddItem(newItemType).Base;

            if (Plugin.Instance.Config.FillMaxAmmo || Plugin.Instance.Config.RandomAttachments)
            {
                if (NewItem is Firearm firearm)
                {
                    if (Plugin.Instance.Config.RandomAttachments)
                        firearm.ApplyAttachmentsCode(AttachmentsUtils.GetRandomAttachmentsCode(firearm.ItemTypeId), reValidate: true);

                    if (!Plugin.Instance.Config.FillMaxAmmo)
                        return false;

                    if (firearm.TryGetModule<MagazineModule>(out MagazineModule magazineModule))
                    {
                        magazineModule.ServerInsertEmptyMagazine();
                        magazineModule.AmmoStored = magazineModule.AmmoMax;
                        magazineModule.ServerResyncData();
                    }
                }
            }

            return false;
        }

        public string GetItemName(ItemType itemType)
        {
            if (Plugin.Instance.Config.ItemsName.TryGetValue(itemType, out string itemName)) return itemName;
            return itemType.ToString();
        }

        public static void OnSpawnSCP1162()
        {
            if (Scp1162 != null) return;

            CustomRoomLocationData crldTemp = Plugin.Instance.Config.CustomRoomLocations.RandomItem();

            var Room = RoomIdentifier.AllRoomIdentifiers.FirstOrDefault(x => x.Name == crldTemp.RoomNameType);
            Scp1162 = new SimplifiedToy(PrimitiveType.Cylinder, new Vector3(crldTemp.OffsetX, crldTemp.OffsetY, crldTemp.OffsetZ), new Vector3(crldTemp.RotationX, crldTemp.RotationY, crldTemp.RotationZ),
                new Vector3(1.3f, 0.1f, 1.3f), Color.black, Room.transform, 0.95f).Spawn();

            Scp1162Position = Scp1162.transform.position;
        }

        public static void OnDespawnSCP1162()
        {
            if (Scp1162 == null) return;

            NetworkServer.Destroy(Scp1162.gameObject);
            Scp1162 = null;
        }
    }
}