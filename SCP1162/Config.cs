namespace SCP1162
{
    using System.Collections.Generic;
    using System.ComponentModel;

    public class Config
    {
        [Description("What message will be displayed when using Scp-1162? ({dropitem} - Thrown or dropped item. {giveitem} - Changed item. {dropstatus} - Replaces the text written in the points below in dependence on the drop status.)")]
        public ItemMessage ItemDropMessage { get; set; } = new ItemMessage()
        {
            Message = "<b>You {dropstatus} a <color=green>{dropitem}</color> through <color=yellow>Scp-1162</color>, and received a <color=red>{giveitem}</color></b>",
            DropText = "dropped",
            ThrowText = "throwed"
        };

        [Description("What message will be displayed when item are deleted? ({dropitem} - Thrown or dropped item. {dropstatus} - Replaces the text written in the points below in dependence on the drop status.)")]
        public ItemMessage ItemDeleteMessage { get; set; } = new ItemMessage()
        {
            Message = "<b>You {dropstatus} a <color=green>{dropitem}</color> through <color=yellow>Scp-1162</color>, and got <color=red>nothing</color></b>",
            DropText = "dropped",
            ThrowText = "throwed"
        };

        [Description("From what distance can Scp-1162 be used?")]
        public float Scp1162Distance { get; set; } = 2f;

        [Description("What is the chance of spawning Scp-1162? (100 = always)")]
        public int Scp1162ChanceToSpawn { get; set; } = 100;

        [Description("What is the chance that the hands will be cut off if the item is not in the hands. (Set to 0 to disable)")]
        public int CuttingChance { get; set; } = 30;

        [Description("What is the chance that the item will be deleted. (Set to 0 to disable)")]
        public int DeleteChance { get; set; } = 10;

        [Description("If this item is enabled, the hands will not be cut off only when the player threw item.")]
        public bool IgnoreThrow { get; set; } = true;

        [Description("List of items that may drop from Scp-1162.")]
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
            ItemType.Ammo762x39,
            ItemType.Ammo556x45,
            ItemType.KeycardScientist,
            ItemType.KeycardJanitor,
            ItemType.Coin,
            ItemType.Flashlight
        };

        [Description("List of items and their name when using Scp-1162, if the item is not in the list the name will be the default.")]
        public Dictionary<ItemType, string> ItemsName { get; set; } = new Dictionary<ItemType, string>()
        {
            { ItemType.SCP500, "SCP-500"},
            { ItemType.KeycardContainmentEngineer, "Containment Engineer Keycard"},
            { ItemType.GunCOM15, "COM-15" },
            { ItemType.GrenadeFlash, "Flashbang Grenade" },
            { ItemType.SCP207, "SCP-207" },
            { ItemType.Adrenaline, "Adrenaline" },
            { ItemType.GunCOM18, "COM-18" },
            { ItemType.Medkit, "Medkit" },
            { ItemType.KeycardGuard, "Guard Keycard" },
            { ItemType.Ammo9x19, "Ammo9x19" },
            { ItemType.KeycardZoneManager, "Zone Manager Keycard" },
            { ItemType.KeycardResearchCoordinator, "Research Supervisor Keycard" },
            { ItemType.Ammo762x39, "Ammo762x39" },
            { ItemType.Ammo556x45, "Ammo556x45" },
            { ItemType.KeycardScientist, "Scientist Keycard" },
            { ItemType.KeycardJanitor, "Janitor Keycard" },
            { ItemType.Coin, "Coin" },
            { ItemType.Flashlight, "Flashlight" }
        };

        [Description("The name of the role that can use commands for Scp-1162.")]
        public List<string> AllowedRank { get; set; } = new List<string>()
        {
            "owner"
        };

        [Description("User ID that can use commands for the Scp-1162.")]
        public List<string> AllowedUserID { get; set; } = new List<string>()
        {
            "SomeOtherSteamId64@steam"
        };
    }
}

public class ItemMessage
{
    public string Message { get; set; }
    public string DropText { get; set; }
    public string ThrowText { get; set; }
}