namespace SCP1162
{
    using UnityEngine;
    using System.Linq;
    using InventorySystem;
    using InventorySystem.Items;
    using PluginAPI.Core.Attributes;
    using PluginAPI.Core.Zones;
    using PluginAPI.Enums;
    using PluginAPI.Core;
    using Random = UnityEngine.Random;
    using CustomPlayerEffects;

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
            var scp1162 = new SimplifiedToy(PrimitiveType.Cylinder, new Vector3(17f, 13f, 3.59f), new Vector3(90f, 0f, 0f),
                new Vector3(1.3f, 0.1f, 1.3f), Color.black, Room.Transform, 0.95f).Spawn();

            SCP1162Position = scp1162.transform.position;
        }

        [PluginEvent(ServerEventType.PlayerDropItem)]
        public void OnPlayerDroppedItem(Player player, ItemBase item)
        {
            if (!Round.IsRoundStarted) return;
            if (Vector3.Distance(SCP1162Position, player.Position) <= Plugin.Instance.Config.SCP1162Distance)
            {
                if (Plugin.Instance.Config.OnlyThrow)
                {
                    player.EffectsManager.EnableEffect<SeveredHands>(1000);
                    return;
                }
                OnUseSCP1162(player, item);
            }
        }

        [PluginEvent(ServerEventType.PlayerThrowItem)]
        public void OnThrowItem(Player player, ItemBase item, Rigidbody rb)
        {
            if (!Round.IsRoundStarted) return;
            if (Vector3.Distance(SCP1162Position, player.Position) <= Plugin.Instance.Config.SCP1162Distance)
            {
                OnUseSCP1162(player, item);
            }
        }

        private void OnUseSCP1162(Player player, ItemBase item)
        {
            if (Plugin.Instance.Config.CuttingHands)
            {
                if (player.CurrentItem != item)
                {
                    if (Plugin.Instance.Config.ChanceCutting >= Random.Range(0, 101))
                    {
                        player.EffectsManager.EnableEffect<SeveredHands>(1000);
                        return;
                    }
                }
            }


            var newItemType = Plugin.Instance.Config.DroppingItems.RandomItem();

            player.ReceiveHint(
            Plugin.Instance.Config.ItemDropMessage.Replace("{dropitem}", item.ItemTypeId.ToString())
                    .Replace("{giveitem}", newItemType.ToString()), 2f);

            player.ReferenceHub.inventory.ServerRemoveItem(item.ItemSerial, item.PickupDropModel);
            player.AddItem(newItemType);
        }
    }
}