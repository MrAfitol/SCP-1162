namespace SCP1162
{
    using PluginAPI.Core;
    using PluginAPI.Core.Attributes;
    using PluginAPI.Enums;
    using PluginAPI.Events;
    
    public class Plugin
    {
        public static Plugin Instance { get; private set; }

        [PluginConfig("configs/scp_1162.yml")]
        public Config Config;

        [PluginPriority(LoadPriority.Highest)]
        [PluginEntryPoint("SCP-1162", "1.0.5", "A plugin that adds SCP-1162.", "MrAfitol")]
        void LoadPlugin()
        {
            Instance = this;
            EventManager.RegisterEvents<EventHandlers>(this);

            var handler = PluginHandler.Get(this);
            handler.SaveConfig(this, nameof(Config));
        }
    }
}