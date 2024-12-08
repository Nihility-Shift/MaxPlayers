using CG.Ship.Repair;
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
            int MaxDifficultyConfigPlayerCount = 4;
            
            foreach (var thing in DifficultyPlayerCountTable.Instance.setups)
            {
                if (thing.playerCount > MaxDifficultyConfigPlayerCount)
                    MaxDifficultyConfigPlayerCount = thing.playerCount;
            }

            if (playerCount > MaxDifficultyConfigPlayerCount)
                playerCount = MaxDifficultyConfigPlayerCount;
        }
    }

    // Original code minus error logs.
    [HarmonyPatch(typeof(HullDamageController), "Config", MethodType.Getter)]
    class HullDamageControlerPatch
    { 
        static bool Prefix(HullDamageController __instance, ref HullDamageConfig __result)
        {
            int PlayerCount = GameSessionManager.ActiveSession.PlayerCount;
            if (PlayerCount > __instance.damageConfigs.Length)
                PlayerCount = __instance.damageConfigs.Length;
            __result = __instance.damageConfigs[PlayerCount - 1];
            return false;
        }
    }
}
