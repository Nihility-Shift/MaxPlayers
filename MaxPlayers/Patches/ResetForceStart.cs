using Gameplay.Hub;
using HarmonyLib;

namespace MaxPlayers.Patches
{
    [HarmonyPatch(typeof(HubQuestManager), "StartQuest")]
    internal class ResetForceStart
    {
        [HarmonyPostfix]
        public static void Patch()
        {
            StartQuest.ToldToStart = false;
        }
    }
}
