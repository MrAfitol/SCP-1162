using InventorySystem;
using InventorySystem.Items;
using PluginAPI.Core;

namespace SCP1162
{
    using PluginAPI.Enums;
    using UnityEngine;
    using System.Linq;
    using PluginAPI.Core.Zones;
    using PluginAPI.Core.Attributes;
    
    public class EventHandlers
    {
        private Vector3 SCP1162Position;
        
        [PluginEvent(ServerEventType.MapGenerated)]
        public void OnGenerationMap()
        {
            Spawn1162();
        }
        
        public void Spawn1162()
        {
            var Room = LightZone.Rooms.FirstOrDefault(x => x.GameObject.name == "LCZ_173");
            Log.Debug(Room.Position.ToString());
            var scp1162 = new SimplifiedToy(PrimitiveType.Cylinder, new Vector3(17f, 13f, 3.59f), new Vector3(90f, 0f, 0f),
                new Vector3(1.3f, 0.1f, 1.3f), Color.black, Room.Transform, 0.95f).Spawn();

            SCP1162Position = scp1162.transform.position;
        }

        [PluginEvent(ServerEventType.PlayerDropItem)]
        public void OnPlayerDroppedItem(Player player, ItemBase item)
        {
            if (Vector3.Distance(SCP1162Position, player.Position) <= Plugin.Instance.Config.SCP1162Distance)
            {
                var newItemType = Plugin.Instance.Config.DroppingItems.RandomItem();

                player.ReceiveHint(
                    Plugin.Instance.Config.ItemDropMessage.Replace("{dropitem}", item.ItemTypeId.ToString())
                        .Replace("{giveitem}", newItemType.ToString()), 2f);

                player.ReferenceHub.inventory.ServerRemoveItem(item.ItemSerial, item.PickupDropModel);
                player.AddItem(newItemType);
            }
        }

        [PluginEvent(ServerEventType.PlayerThrowItem)]
        public void OnThrowItem(Player player, ItemBase item, Rigidbody rb)
        {
            if (Vector3.Distance(SCP1162Position, player.Position) <= Plugin.Instance.Config.SCP1162Distance)
            {
                var newItemType = Plugin.Instance.Config.DroppingItems.RandomItem();

                player.ReceiveHint(
                    Plugin.Instance.Config.ItemDropMessage.Replace("{dropitem}", item.ItemTypeId.ToString())
                        .Replace("{giveitem}", newItemType.ToString()), 2f);

                player.ReferenceHub.inventory.ServerRemoveItem(item.ItemSerial, item.PickupDropModel);
                player.AddItem(newItemType);
            }
        }
    }
}