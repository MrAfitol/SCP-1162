namespace SCP1162
{
    using System.Collections.Generic;
    using System.ComponentModel;
    
    public class Config
    {
        [Description("What message will be displayed when using SCP-1162?")]
        public string ItemDropMessage { get; set; } = "<b>You dropped a <color=green>{dropitem}</color> through <color=yellow>SCP-1162</color>, and received a <color=red>{giveitem}</color></b>";

        [Description("From what distance can SCP-1162 be used?")]
        public float SCP1162Distance { get; set; } = 2f;

        [Description("Will the hands be cut off if the item is not in the hands?")]
        public bool CuttingHands { get; set; } = true;

        [Description("What is the chance that the hands will be cut off if the item is not in the hands")]
        public int ChanceCutting { get; set; } = 40;

        [Description("List of items that may drop from SCP-1162")]
        public List<ItemType> DroppingItems { get; set; } = new List<ItemType>
        {
            ItemType.SCP500,
            ItemType.KeycardContainmentEngineer,
            ItemType.GunCOM15,
            ItemType.GrenadeFlash,
            ItemType.SCP207,
            ItemType.Adrenaline,
            ItemType.GunCOM18,
            ItemType.Medkit,
            ItemType.KeycardGuard,
            ItemType.Ammo9x19,
            ItemType.KeycardZoneManager,
            ItemType.KeycardResearchCoordinator,
            ItemType.KeycardGuard,
            ItemType.Ammo762x39,
            ItemType.Ammo556x45,
            ItemType.GrenadeFlash,
            ItemType.KeycardScientist,
            ItemType.KeycardJanitor,
            ItemType.Coin,
            ItemType.Flashlight
        };
    }
}