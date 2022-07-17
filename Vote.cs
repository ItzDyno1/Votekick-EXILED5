using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votekick
{
    internal abstract class Vote
    {
        protected static Config Configs => Votekick.Instance.Config;
        protected static ushort BroadcastTime => Configs.VoteBroadcastDuration;
        protected static ushort Duration => Configs.VotingPeriodDuration;

        internal string Reason;
        internal int[] Votes;
        internal List<Player> AlreadyVoted;
        internal CoroutineHandle ActiveCoro;

        internal const byte Yes = 0;
        internal const byte No = 1;

        protected static void BroadcastToAllPlayers(string message)
        {
            foreach (var player in Player.List)
            {
                player.ClearBroadcasts();
                player.Broadcast(BroadcastTime, message);
            }
        }
    }

    internal class KickVote : Vote
    {
        public Player Target;

        public KickVote(string reason, Player target)
        {
            Reason = reason;
            Target = target;

            Votes = new int[2] { 0, 0 };
            AlreadyVoted = new List<Player>();

            BroadcastToAllPlayers(Configs.VotekickStartedBroadcast.Replace("{name}", Target.Nickname).Replace("{reason}", Reason));

            ActiveCoro = Timing.CallDelayed(Duration, () =>
            {
                if (Votes[Yes] > Votes[No])
                {
                    Target.Kick(Configs.VotekickKickMessage.Replace("{reason}", Reason));
                    BroadcastToAllPlayers(Configs.VotekickSuccessBroadcast.Replace("{name}", Target.Nickname).Replace("{reason}", Reason));
                }
                else
                {
                    BroadcastToAllPlayers(Configs.VotekickFailBroadcast.Replace("{name}", Target.Nickname));
                }

                Votekick.Instance.ActiveVote = null;
            });
        }
    }

    internal class RestartVote : Vote
    {
        public RestartVote(string reason)
        {
            Reason = reason;

            Votes = new int[2] { 0, 0 };
            AlreadyVoted = new List<Player>();

            BroadcastToAllPlayers(Configs.VoterestartStartedBroadcast.Replace("{reason}", Reason));

            ActiveCoro = Timing.CallDelayed(Duration, () =>
            {
                if (Votes[0] > Votes[1])
                {
                    Timing.CallDelayed(Configs.VoterestartRestartDelay, () =>
                    {
                        Round.Restart();
                    });
                    BroadcastToAllPlayers(Configs.VoterestartSuccessBroadcast);
                }
                else
                {
                    BroadcastToAllPlayers(Configs.VoterestartFailBroadcast);
                }

                Votekick.Instance.ActiveVote = null;
            });
        }
    }
}