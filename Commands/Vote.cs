using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace Votekick.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class VoteCommand : ICommand
    {
        private static Vote ActiveVote => Votekick.Instance.ActiveVote;

        public string Command { get; } = "vote";

        public string[] Aliases { get; } = { };

        public string Description { get; } = "Used for starting votekicks, round restart votes, or for voting yes/no.";

        public bool Execute(ArraySegment<string> Arguments, ICommandSender sender, out string response)
        {
            if (Arguments.Count == 0)
            {
                response = "Incorrect usage! \"vote start\" or \"vote yes\" or \"vote no\"";
                return false;
            }

            switch (Arguments.At(0).ToLower())
            {
                case "start":
                    if (Arguments.Count < 2)
                    {
                        response = "Incorrect usage! \"vote start <player> [reason]\"";
                        return false;
                    }
                    return Start(Arguments, sender, out response);

                case "yes":
                case "no":
                case "y":
                case "n":
                    return SendVote(Arguments, sender, out response);

                default:
                    response = "Incorrect usage! Valid sub-commands are \"start\", \"yes\", \"no\"";
                    return false;
            }
        }

        public bool Start(ArraySegment<string> Arguments, ICommandSender sender, out string response)
        {
            if (!CanStart(sender as CommandSender))
            {
                response = "You're not allowed to use this command!";
                return false;
            }

            if (ActiveVote is not null)
            {
                response = "You cannot start a vote whilst another vote is currently active!";
                return false;
            }

            switch (Arguments.At(1).ToLower())
            {
                case "kick":
                    return StartVotekick(Arguments, out response);

                case "restart":
                    return StartVoterestart(Arguments, out response);

                default:
                    response = "That is not a valid sub-command! Use \"vote start kick\" or \"vote start restart\"!";
                    return false;
            }
        }

        public bool StartVotekick(ArraySegment<string> Arguments, out string response)
        {
            Player TargetPlayer = Player.Get(Arguments.At(1));

            if (ImmuneToVotekick(TargetPlayer))
            {
                response = "The player you entered cannot be votekicked or they do not exist!";
                return false;
            }

            string reason = ExtractReason(Arguments, NumArguments: 3);

            Votekick.Instance.ActiveVote = new KickVote(reason, TargetPlayer);

            response = "Succesfully stared the votekick!";
            return true;
        }

        public bool StartVoterestart(ArraySegment<string> Arguments, out string response)
        {
            string reason = ExtractReason(Arguments, NumArguments: 2);

            Votekick.Instance.ActiveVote = new RestartVote(reason);

            response = "Succesfully stared the restart vote!";
            return true;
        }

        public bool SendVote(ArraySegment<string> Arguments, ICommandSender sender, out string response)
        {
            var PlayerSender = Player.Get((sender as CommandSender)?.SenderId);

            if (ActiveVote is null)
            {
                response = "There is currently no vote in progress!";
                return false;
            }

            if (ActiveVote.AlreadyVoted.Contains(PlayerSender))
            {
                response = "You have already voted!";
                return false;
            }

            if (!CanVote(sender as CommandSender))
            {
                response = "You're not allowed to use this command!";
                return false;
            }

            switch (Arguments.At(0))
            {
                case "yes":
                case "y":
                    ActiveVote.Votes[Vote.Yes]++;
                    ActiveVote.AlreadyVoted.Add(PlayerSender);
                    response = "Voted yes!";
                    return true;

                case "no":
                case "n":
                    ActiveVote.Votes[Vote.No]++;
                    ActiveVote.AlreadyVoted.Add(PlayerSender);
                    response = "Voted no!";
                    return true;
            }

            response = "Something went wrong";
            return false;
        }

        public bool CanStart(CommandSender player)
        {
            return player.CheckPermission("vk.start");
        }

        public bool CanVote(CommandSender player)
        {
            return player.CheckPermission("vk.vote");
        }

        public bool ImmuneToVotekick(Player player)
        {
            return player?.CheckPermission("vk.immune") ?? true;
        }

        private string ExtractReason(ArraySegment<string> Arguments, int NumArguments)
        {
            if (Arguments.Count >= NumArguments)
            {
                string output = "";

                for (int i = 2; i < Arguments.Count; i++)
                {
                    output += Arguments.At(i) + " ";
                }

                return output;
            }

            return "No reason given";
        }
    }
}