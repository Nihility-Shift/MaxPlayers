using HarmonyLib;
using PlatformAbstractionLayer.Steam;
using System.Collections.Generic;
using System.Reflection.Emit;
using static VoidManager.Utilities.HarmonyHelpers;

namespace MaxPlayers.Patches
{
    //Default steam lobby size is hardcoded to 6. Fix by Hard-Coding to 255 (non-dynamic since the number is only changed on lobby create).
    [HarmonyPatch(typeof(SteamMatchmakingHandler), "CreateLobby")]
    internal class SteamLobbyLimitPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> targetSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldc_I4_6)
            };
            List<CodeInstruction> patchSequence = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldc_I4, 255)
            };

            return PatchBySequence(instructions, targetSequence, patchSequence, PatchMode.REPLACE, CheckMode.NEVER);
        }
    }
}
