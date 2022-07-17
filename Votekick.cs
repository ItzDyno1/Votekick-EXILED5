using Exiled.API.Features;
using System;
using MEC;

namespace Votekick
{
    internal class Votekick : Plugin<Config>
    {
        internal static Votekick Instance;

        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 1);
        public override Version Version { get; } = new Version(1, 0, 0);

        public override string Name { get; } = "Votekick";
        public override string Author { get; } = "TeamEXAngus#5525";

        internal Vote ActiveVote;

        public Votekick()
        {
            Instance = this;
        }

        public override void OnEnabled()
        {
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            if (ActiveVote is not null)
            {
                Timing.KillCoroutines(ActiveVote.ActiveCoro);
                ActiveVote = null;
            }

            base.OnDisabled();
        }
    }
}