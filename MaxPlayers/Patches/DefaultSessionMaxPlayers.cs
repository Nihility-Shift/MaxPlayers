using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using static VoidManager.Utilities.HarmonyHelpers;

namespace MaxPlayers.Patches
{
    //Remove line setting player limit to ship recommended limit.

    [HarmonyPatch(typeof(GameSessionManager), "LoadGameSessionNetworkedAssets")]
    internal class DefaultSessionMaxPlayers
    {
        private static FieldInfo activeGameSessioninfo = AccessTools.Field(typeof(GameSessionManager), "activeGameSession");
        [HarmonyPrefix]
        public static IEnumerable<CodeInstruction> Replacement(IEnumerable<CodeInstruction> instructions)
        {
            CodeInstruction[] targetSequence = new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Brtrue),
                new CodeInstruction(OpCodes.Call),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Call),
                new CodeInstruction(OpCodes.Callvirt),
                new CodeInstruction(OpCodes.Call),
                new CodeInstruction(OpCodes.Conv_U1),
                new CodeInstruction(OpCodes.Callvirt),
            };

            CodeInstruction[] patchSequence = new CodeInstruction[] {};

            return PatchBySequence(instructions, targetSequence, patchSequence, PatchMode.REPLACE, CheckMode.NEVER);
        }
    }
}
