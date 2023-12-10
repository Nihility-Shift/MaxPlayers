using CG.Client.Quests;
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
