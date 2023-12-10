using HarmonyLib;
using Photon.Pun;
using System.Reflection;
using ToolClasses;
using UnityEngine;

namespace MaxPlayers.Patches
{
    [HarmonyPatch(typeof(GameSessionManager), "LoadGameSessionNetworkedAssets")]
    internal class DefaultSessionMaxPlayers
    {
        private static FieldInfo activeGameSessioninfo = AccessTools.Field(typeof(GameSessionManager), "activeGameSession");
        [HarmonyPrefix]
        public static bool Replacement(GameSessionManager __instance)
        {
            GameSession activeGameSession = (GameSession)activeGameSessioninfo.GetValue(__instance);
            activeGameSession.LoadShip();
            activeGameSession.SetupProgressTracker();
            foreach (GameSessionAsset gameSessionAsset in activeGameSession.NetAssets)
            {
                gameSessionAsset.InstantiateNetworked(null, null);
            }
            activeGameSession.LoadSectorNetworkedObjects();
            PunSingleton<PhotonService>.Instance.SetCurrentRoomInHub(activeGameSession.IsHub);
            if (activeGameSession.IsHub)
            {
                PhotonNetwork.CurrentRoom.MaxPlayers = Plugin.PlayerCount;
                return false;
            }
            PhotonNetwork.CurrentRoom.MaxPlayers = (byte)Mathf.Min(Plugin.PlayerCount, activeGameSession.MaxPlayerCount);
            return false;
        }
    }
}
