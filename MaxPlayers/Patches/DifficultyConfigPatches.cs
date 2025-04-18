using Gameplay.MissionDifficulty;
using HarmonyLib;

namespace MaxPlayers.Patches
{
    [HarmonyPatch(typeof(DifficultyPlayerCountTable), "GetConfig")]
    internal class DifficultyPlayerCountTablePatch
    {
        [HarmonyPrefix]
        static void GetConfigPrefix(ref int playerCount)
        {
            int MaxDifficultyConfigPlayerCount = 6;
            
            foreach (var thing in DifficultyPlayerCountTable.Instance.setups)
            {
                if (thing.playerCount > MaxDifficultyConfigPlayerCount)
                    MaxDifficultyConfigPlayerCount = thing.playerCount;
            }

            if (playerCount > MaxDifficultyConfigPlayerCount)
                playerCount = MaxDifficultyConfigPlayerCount;
        }
    }
}
