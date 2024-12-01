using AdminToys;
using CustomPlayerEffects;
using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments;
using InventorySystem.Items.Firearms.Modules;
using InventorySystem.Items.Usables.Scp330;
using Mirror;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Core.Items;
using PluginAPI.Core.Zones;
using PluginAPI.Events;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SCP1162
{
    public class EventHandlers
    {
        private static Vector3 Scp1162Position;
        private static PrimitiveObjectToy Scp1162;

        public static bool IsScp1162Spawn
        {
            get => Scp1162 != null;
        }

        [PluginEvent]
        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            if (Plugin.Config.Scp1162ChanceToSpawn >= Random.Range(0, 100)) OnSpawnSCP1162();
        }

        [PluginEvent]
        public bool OnPlayerDroppedItem(PlayerDropItemEvent ev) => OnUseSCP1162(ev.Player, ev.Item, ev.Player.CurrentItem == ev.Item);

        private bool OnUseSCP1162(Player player, ItemBase item, bool isThrow)
        {
            if (!IsScp1162Spawn) return true;
            if (!Round.IsRoundStarted) return true;
            if (Vector3.Distance(Scp1162Position, player.Position) > Plugin.Config.Scp1162Distance) return true;
            if (item is Scp330Bag) return true;

            if (isThrow ? !Plugin.Config.IgnoreThrow && Plugin.Config.CuttingChance > 0 : Plugin.Config.CuttingChance > 0)
            {
                if (Plugin.Config.CuttingChance >= Random.Range(0, 100))
                {
                    player.EffectsManager.EnableEffect<SeveredHands>(1000);
                    return true;
                }
            }

            if (Plugin.Config.DeleteChance > 0)
            {
                if (Plugin.Config.DeleteChance >= Random.Range(0, 100))
                {
                    player.RemoveItem(new Item(item));
                    player.ReceiveHint(Plugin.Config.ItemDeleteMessage.Message
                        .Replace("{dropitem}", GetItemName(item.ItemTypeId))
                        .Replace("{dropstatus}", isThrow ? Plugin.Config.ItemDeleteMessage.ThrowText : Plugin.Config.ItemDeleteMessage.DropText), 2f);
                    return true;
                }
            }

            var newItemType = Plugin.Config.DroppingItems.RandomItem();

            player.ReceiveHint(Plugin.Config.ItemDropMessage.Message
                        .Replace("{dropitem}", GetItemName(item.ItemTypeId))
                        .Replace("{giveitem}", GetItemName(newItemType))
                        .Replace("{dropstatus}", isThrow ? Plugin.Config.ItemDropMessage.ThrowText : Plugin.Config.ItemDropMessage.DropText), 2f);
            player.RemoveItem(new Item(item));

            ItemBase NewItem = player.AddItem(newItemType);

            if (Plugin.Config.FillMaxAmmo || Plugin.Config.RandomAttachments)
            {
                if (NewItem is Firearm firearm)
                {
                    if (Plugin.Config.RandomAttachments)
                        firearm.ApplyAttachmentsCode(AttachmentsUtils.GetRandomAttachmentsCode(firearm.ItemTypeId), reValidate: true);

                    if (!Plugin.Config.FillMaxAmmo)
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
            if (Plugin.Config.ItemsName.TryGetValue(itemType, out string itemName)) return itemName;
            return itemType.ToString();
        }

        public static void OnSpawnSCP1162()
        {
            if (Scp1162 != null) return;

            CustomRoomLocationData crldTemp = Plugin.Config.CustomRoomLocations.RandomItem();

            var Room = LightZone.Rooms.FirstOrDefault(x => x.Identifier.Name == crldTemp.RoomNameType);
            Scp1162 = new SimplifiedToy(PrimitiveType.Cylinder, new Vector3(crldTemp.OffsetX, crldTemp.OffsetY, crldTemp.OffsetZ), new Vector3(crldTemp.RotationX, crldTemp.RotationY, crldTemp.RotationZ),
                new Vector3(1.3f, 0.1f, 1.3f), Color.black, Room.Transform, 0.95f).Spawn();

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