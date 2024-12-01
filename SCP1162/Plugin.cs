using PluginAPI.Core.Attributes;
using PluginAPI.Events;

namespace SCP1162
{
    public class Plugin
    {
        public static Plugin Instance { get; private set; }

        [PluginConfig("configs/scp_1162.yml")]
        public static Config Config;

        [PluginEntryPoint("SCP-1162", "1.2.2", "A plugin that adds SCP-1162.", "MrAfitol")]
        public void LoadPlugin()
        {
            Instance = this;
            EventManager.RegisterEvents<EventHandlers>(this);
        }
    }
}