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
        [Description("Ya sea que el complemento esté habilitado o no")]
        public bool IsEnabled { get; set; } = true;

        [Description("Cuántos segundos tienen los jugadores para votar")]
        public ushort VotingPeriodDuration { get; set; } = 25;

        [Description("Cuántos segundos los jugadores muestran mensajes en la parte superior de su pantalla")]
        public ushort VoteBroadcastDuration { get; set; } = 8;

        [Description("Cuántos segundos después de que finaliza el reinicio de una votación antes de que se reinicie la ronda")]
        public ushort VoterestartRestartDelay { get; set; } = 5;

        public string VotekickStartedBroadcast { get; set; } = "Votekick: {name} for {reason}\nType \".vote yes\" or \".vote no\" en la consola para votar!";

        public string VotekickSuccessBroadcast { get; set; } = "{name} Fue votado por {reason}!";

        public string VotekickFailBroadcast { get; set; } = "{name} No fue votado!";

        public string VotekickKickMessage { get; set; } = "Votekick: {reason}";

        public string VoterestartStartedBroadcast { get; set; } = "Votar reinicio: Reiniciar la ronda por {}\nType \".vote yes\" or \".vote no\" en la consola para votar!";

        public string VoterestartSuccessBroadcast { get; set; } = "La votación fue exitosa y la ronda se reiniciará en 5 segundos!";

        public string VoterestartFailBroadcast { get; set; } = "La votación no tuvo éxito, por lo que la ronda no se reiniciará!";

        public string VoteDeletedBroadcast { get; set; } = "El voto actual ha sido eliminado!";
    }
}
