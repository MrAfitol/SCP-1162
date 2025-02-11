using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
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

            if (!playerSender.HasAnyPermission("scp1162.*", "scp1162.control"))
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
                    if (EventsHandler.IsScp1162Spawn)
                    {
                        response = "Scp-1162 has already spawned";
                        return true;
                    }
                    else
                    {
                        EventsHandler.OnSpawnSCP1162();
                        response = "Scp-1162 has been spawned";
                        return true;
                    }
                case "despawn":
                    if (EventsHandler.IsScp1162Spawn)
                    {
                        EventsHandler.OnDespawnSCP1162();
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
    }
}