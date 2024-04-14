using MaxPlayers.Patches;
using Photon.Pun;
using VoidManager.Chat.Router;
using VoidManager.Utilities;

namespace MaxPlayers
{
    internal class CountCommand : ChatCommand
    {
        public override string[] CommandAliases()
            => new string[] { "playercount", "pc" };

        public override string Description()
            => "Count of players.";

        public override void Execute(string arguments)
        {
            if (!PhotonNetwork.InRoom) return;
            Messaging.Echo($"Players: {PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}");
        }
    }
    internal class IncreaseCommand : ChatCommand
    {
        public override string[] CommandAliases()
            => new string[] { "setplayercount", "spc" };

        public override string Description()
            => "Count of players.";

        public override void Execute(string arguments)
        {
            string[] args = arguments.Split(' ');
            if (args.Length < 1 || !PhotonNetwork.IsMasterClient) return;
            if (int.TryParse(args[0], out int value))
            {
                BepinPlugin.PlayerCount = (byte)value;
            }
            if (PhotonNetwork.InRoom) PhotonNetwork.CurrentRoom.MaxPlayers = BepinPlugin.PlayerCount;
            Messaging.Echo($"Max player count: desired-{BepinPlugin.PlayerCount} : current-{PhotonNetwork.CurrentRoom.MaxPlayers}");
        }
    }

    internal class StartQuest : ChatCommand
    {
        public static bool ToldToStart = false;
        public override string[] CommandAliases()
            => new string[] { "startquest", "sq" };

        public override string Description()
            => "Starts the session hub count down.";

        public override void Execute(string arguments)
        {
            if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient) return;
            ToldToStart = !ToldToStart;
            FourPlayersStartSession.QuestStartProcess.StartProcess();
        }
    }
}
