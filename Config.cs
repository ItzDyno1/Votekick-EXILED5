using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votekick
{
    internal class Config : IConfig
    {
        [Description("Whether or not the plugin is enabled")]
        public bool IsEnabled { get; set; } = true;

        [Description("How many seconds players have to vote")]
        public ushort VotingPeriodDuration { get; set; } = 25;

        [Description("How many seconds players are show messages at the top of their screen")]
        public ushort VoteBroadcastDuration { get; set; } = 5;

        [Description("How many seconds after a vote restart ends before the round is restarted")]
        public ushort VoterestartRestartDelay { get; set; } = 5;

        public string VotekickStartedBroadcast { get; set; } = "Votekick: {name} for {reason}\nType \".vote yes\" or \".vote no\" in the console to vote!";

        public string VotekickSuccessBroadcast { get; set; } = "{name} was votekicked for {reason}!";

        public string VotekickFailBroadcast { get; set; } = "{name} was not votekicked!";

        public string VotekickKickMessage { get; set; } = "Votekick: {reason}";

        public string VoterestartStartedBroadcast { get; set; } = "Vote restart: Restart the round for {}\nType \".vote yes\" or \".vote no\" in the console to vote!";

        public string VoterestartSuccessBroadcast { get; set; } = "The vote was successful and the round will restart in 5 seconds!";

        public string VoterestartFailBroadcast { get; set; } = "The vote was unsuccessful so the round will not restart!";

        public string VoteDeletedBroadcast { get; set; } = "The current vote has been deleted!";
    }
}