using CG.Client.Quests;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Reflection;
using UnityEngine;

namespace MaxPlayers.Patches
{
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
}
