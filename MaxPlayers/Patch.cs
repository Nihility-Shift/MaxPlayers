using CG.Client.Quests;
using CG.Client.Quests.Generation;
using CG.Game;
using CG.Game.Player;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Linq;
using System.Reflection;
using ToolClasses;
using UnityEngine;

namespace MaxPlayers
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
    [HarmonyPatch(typeof(QuestStartProcess), "UpdateQuestProcess")]
    internal class FourPlayersStartSession
    {
        public static QuestStartProcess QuestStartProcess { get; private set; } = null;
        private static MethodInfo HasEnoughPlayersToBeginCountdownInfo = AccessTools.Method(typeof(QuestStartProcess), "HasEnoughPlayersToBeginCountdown");
        private static FieldInfo _allPlayersWereInZoneInfo = AccessTools.Field(typeof(QuestStartProcess), "_allPlayersWereInZone");
        private static FieldInfo _timeLeftInfo = AccessTools.Field(typeof(QuestStartProcess), "_timeLeft");
        private static MethodInfo SetSecondsLeftInfo = AccessTools.Method(typeof(QuestStartProcess), "SetSecondsLeft");
        [HarmonyPrefix]
        public static bool Replacement(QuestStartProcess __instance, int playersInZoneCount)
        {
            QuestStartProcess = __instance;
            float max = 30f;
            int count = Math.Min(PhotonNetwork.CurrentRoom.Players.Count, 4);
            float num;
            float _timeLeft = (float)_timeLeftInfo.GetValue(__instance);
            if (!StartQuest.ToldToStart && !(bool)HasEnoughPlayersToBeginCountdownInfo.Invoke(__instance, new object[] { count, playersInZoneCount }))
            {
                num = 30f;
            }
            else
            {
                bool _allPlayersWereInZone = (bool)_allPlayersWereInZoneInfo.GetValue(__instance);
                if (!StartQuest.ToldToStart)
                {
                    if (count == playersInZoneCount)
                    {
                        _allPlayersWereInZone = true;
                        max = 6f;
                    }
                    else if (_allPlayersWereInZone)
                    {
                        _allPlayersWereInZone = false;
                        _timeLeft = 30f;
                        _timeLeftInfo.SetValue(__instance, 30f);
                        max = 30f;
                    }
                }
                num = -Time.deltaTime;
            }
            _timeLeft = Mathf.Clamp(_timeLeft + num, 0f, max);
            int num2 = (int)_timeLeft;
            _timeLeftInfo.SetValue(__instance, _timeLeft);
            if (num2 == __instance.SecondsLeft)
            {
                return false;
            }
            SetSecondsLeftInfo.Invoke(__instance, new object[] { num2 });
            return false;
        }
    }
    [HarmonyPatch(typeof(HubQuestManager), "StartQuest")]
    internal class ResetForceStart
    {
        [HarmonyPostfix]
        public static void Patch()
        {
            StartQuest.ToldToStart = false;
        }
    }
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
