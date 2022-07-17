using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votekick
{
    internal class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public ushort VotingPeriodDuration { get; set; } = 25;

        public ushort VoteBroadcastDuration { get; set; } = 5;

        public ushort VoterestartRestartDelay { get; set; } = 5;

        public string VotekickStartedBroadcast { get; set; } = "Votekick: {name} for {reason}\nType \".vote yes\" or \".vote no\" in the console to vote!";

        public string VotekickSuccessBroadcast { get; set; } = "{name} was votekicked for {reason}!";

        public string VotekickFailBroadcast { get; set; } = "{name} was not votekicked!";

        public string VotekickKickMessage { get; set; } = "Votekick: {reason}";

        public string VoterestartStartedBroadcast { get; set; } = "Vote restart: Restart the round for {}\nType \".vote yes\" or \".vote no\" in the console to vote!";

        public string VoterestartSuccessBroadcast { get; set; } = "The vote was successful and the round will restart in 5 seconds!";

        public string VoterestartFailBroadcast { get; set; } = "The vote was unsuccessful so the round will not restart!";
    }
}