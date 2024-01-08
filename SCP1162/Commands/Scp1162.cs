using CommandSystem;
using PluginAPI.Core;
using SCP1162.Extensions;
using System;

namespace SCP1162.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Scp1162 : ICommand
    {
        public string Command => "scp1162";

        public string[] Aliases => new string[] { "1162" };

        public string Description => "Command for SCP-1162";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player playerSender = Player.Get(sender);

            if (!IsAllow(playerSender))
            {
                response = $"You don't have permission to use this command!";
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Incorrect command, use: \n.scp1162 spawn \n.scp1162 despawn";
                return false;
            }

            switch (arguments.At(0))
            {
                case "spawn":
                    if (EventHandlers.IsScp1162Spawn)
                    {
                        response = "Scp-1162 has already spawned";
                        return true;
                    }
                    else
                    {
                        EventHandlers.OnSpawnSCP1162();
                        response = "Scp-1162 has been spawned";
                        return true;
                    }
                case "despawn":
                    if (EventHandlers.IsScp1162Spawn)
                    {
                        EventHandlers.OnDespawnSCP1162();
                        response = "Scp-1162 has been despawned";
                        return true;
                    }
                    else
                    {
                        response = "Scp-1162 has already despawned";
                        return true;
                    }
                default:
                    response = "Incorrect command, use: \n.scp1162 spawn \n.scp1162 despawn";
                    return false;
            }
        }

        private bool IsAllow(Player player) => IsAllowFromRank(player) || IsAllowFromUserID(player);

        private bool IsAllowFromUserID(Player player)
        {
            if (!string.IsNullOrEmpty(player.UserId))
                if (Plugin.Config.AllowedUserID?.Count > 0)
                    if (Plugin.Config.AllowedUserID.Contains(player.UserId))
                        return true;
            return false;
        }

        private bool IsAllowFromRank(Player player)
        {
            if (!string.IsNullOrEmpty(player.GetGroupName()))
                if (Plugin.Config.AllowedRank?.Count > 0)
                    if (Plugin.Config.AllowedRank.Contains(player.GetGroupName()))
                        return true;
            return false;
        }
    }
}