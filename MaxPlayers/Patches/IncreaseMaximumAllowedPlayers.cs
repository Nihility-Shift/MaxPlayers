using Gameplay.Quests;
using HarmonyLib;

namespace MaxPlayers.Patches
{
    [HarmonyPatch(typeof(QuestGenerator), "Create")]
    internal class IncreaseMaximumAllowedPlayers
    {
        [HarmonyPostfix]
        public static void Patch(ref Quest __result)
        {
            if (__result.Asset.scenarioSetup.MaximumPlayers >= 4) __result.Asset.scenarioSetup.MaximumPlayers = int.MaxValue;
        }
    }
}
