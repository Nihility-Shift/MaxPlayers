using Gameplay.MissionDifficulty;
using HarmonyLib;

namespace MaxPlayers.Patches
{
    /*[HarmonyPatch(typeof(QuestDifficultyContainer), "GetConfig")]
    internal class QuestDifficultyContainerPatche
    {
        [HarmonyPrefix]
        static void GetConfigPrefix(ref Difficulty df)
        {
            if ((int)df > 4)
            {
                df = (Difficulty)4;
            }
        }
    }*/

    [HarmonyPatch(typeof(DifficultyPlayerCountTable), "GetConfig")]
    internal class DifficultyPlayerCountTablePatch
    {
        [HarmonyPrefix]
        static void GetConfigPrefix(ref int playerCount)
        {
            int MaxDifficultyConfigPlayerCount = 4;
            foreach(var thing in DifficultyPlayerCountTable.Instance.setups)
            {
                if (thing.playerCount > MaxDifficultyConfigPlayerCount)
                {
                    MaxDifficultyConfigPlayerCount = thing.playerCount;
                }
            }

            if (playerCount > MaxDifficultyConfigPlayerCount)
            {
                playerCount = MaxDifficultyConfigPlayerCount;
            }
        }
    }
}
