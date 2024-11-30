﻿using Gameplay.Quests;
using Photon.Pun;
using System.Collections.Generic;
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
            => new string[] { "setplayerlimit", "spl" };

        public override string Description()
            => "Player Limit";

        public override void Execute(string arguments)
        {
            string[] args = arguments.Split(' ');
            if (args.Length < 1 || !PhotonNetwork.IsMasterClient) return;


            if (int.TryParse(args[0], out int value))
            {
                Limits.PlayerLimit = value;
            }
            Messaging.Echo($"Player limit: {Limits.PlayerLimit}");
        }
    }

    internal class StartQuest : ChatCommand
    {
        public static bool ToldToStart { get; set; } = false;

        public override string[] CommandAliases()
            => new string[] { "startquest", "sq" };

        public override string Description()
            => "Starts the session hub count down. Use \"now\" argument to begin immediately";

        static List<Argument> arguments = new List<Argument>() { new Argument("now") };

        public override List<Argument> Arguments()
        {
            return arguments;
        }

        internal static void ExecuteStartQuest()
        {
            if (!PhotonNetwork.InRoom || !PhotonNetwork.IsMasterClient) return;
            {
                Messaging.Notification("Must be host to use this command.", 10000);
            }
            if (HubQuestManager.Instance.SelectedQuest == null || HubQuestManager.Instance.CurrentShipSelected == null)
            {
                Messaging.Notification("Must have a quest and ship selected.", 10000);
            }
            ToldToStart = !ToldToStart;
            HubQuestManager.Instance.QuestStartProcess.StartProcess();
        }

        public override void Execute(string arguments)
        {
            if (arguments.StartsWith("now", System.StringComparison.OrdinalIgnoreCase))
            {
                Messaging.Echo("Starting Quest Now.", false);
                HubQuestManager.Instance.StartQuest(HubQuestManager.Instance.SelectedQuest);
            }
            else
            {
                ExecuteStartQuest();
            }
        }
    }
}
