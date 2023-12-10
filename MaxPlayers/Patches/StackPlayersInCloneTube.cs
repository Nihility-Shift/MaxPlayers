using CG.Game.Player;
using HarmonyLib;
using Photon.Pun;
using System.Reflection;

namespace MaxPlayers.Patches
{
    [HarmonyPatch(typeof(CloneTube), "IsAvailable")]
    internal class StackPlayersInCloneTube
    {
        private static FieldInfo _occupantInfo = AccessTools.Field(typeof(CloneTube), "_occupant");
        [HarmonyPrefix]
        public static bool Replacement(CloneTube __instance, ref bool __result)
        {
            Player _occupant = (Player)_occupantInfo.GetValue(__instance);
            __result = _occupant == null || PhotonNetwork.CurrentRoom.Players.Count > 4;
            return false;
        }
    }
}
