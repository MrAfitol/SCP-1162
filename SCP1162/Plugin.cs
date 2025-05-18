using LabApi.Events.Handlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using System;

namespace SCP1162
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance { get; private set; }

        public override string Name { get; } = "SCP-1162";

        public override string Description { get; } = "A plugin that adds SCP-1162.";

        public override string Author { get; } = "MrAfitol";

        public override Version Version { get; } = new Version(1, 3, 3);

        public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);

        public EventsHandler EventsHandler { get; private set; }


        public override void Enable()
        {
            Instance = this;
            EventsHandler = new EventsHandler();
            ServerEvents.WaitingForPlayers += EventsHandler.OnWaitingForPlayers;
            PlayerEvents.DroppingItem += EventsHandler.OnPlayerDroppingItem;
        }

        public override void Disable()
        {
            ServerEvents.WaitingForPlayers -= EventsHandler.OnWaitingForPlayers;
            PlayerEvents.DroppingItem -= EventsHandler.OnPlayerDroppingItem;
            EventsHandler = null;
            Instance = null;
        }
    }
}