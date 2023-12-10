using Gameplay.MissionDifficulty;
using HarmonyLib;

namespace MaxPlayers
{
    [HarmonyPatch(typeof(QuestDifficultyContainer), "GetConfig")]
    internal class DifficultyConfigPatch
    {
        [HarmonyPrefix]
        static void GetConfigPrefix(ref Difficulty df)
        {
            if((int)df >= 4)
            {
                df = (Difficulty)4;
            }
        }
    }

    [HarmonyPatch(typeof(DifficultyPlayerCountTable), "GetConfig")]
    internal class DifficultyPlayerCountTablePatch
    {
        [HarmonyPrefix]
        static void GetConfigPrefix(ref int playerCount)
        {
            if (playerCount >= 4)
            {
                playerCount = 4;
            }
        }
    }
}
