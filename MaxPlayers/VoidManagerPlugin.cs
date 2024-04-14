using VoidManager;
using VoidManager.MPModChecks;

namespace MaxPlayers
{
    internal class VoidManagerPlugin : VoidPlugin
    {
        public override string Author => "Mest, Dragon";

        public override string Description => "Allows for more than 4 players to join the same game.";

        public override MultiplayerType MPType => MultiplayerType.Client;
    }
}
