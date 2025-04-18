using Gameplay.Quests;
using HarmonyLib;
using Photon.Pun;
using System;
using UnityEngine;

namespace MaxPlayers.Patches
{
    [HarmonyPatch(typeof(QuestStartProcess), "UpdateQuestProcess")]
    internal class FourPlayersStartSession
    {
        [HarmonyPrefix]
        public static bool Replacement(QuestStartProcess __instance, int playersInZoneCount, ref float ____timeLeft, ref bool ____allPlayersWereInZone)
        {
            if(!PhotonNetwork.InRoom)
            {
                return false;
            }

            float MaxTimeLeft = QuestStartProcess.START_PROCESS_DURATION;
            int playerCount = Math.Min(PhotonNetwork.CurrentRoom.Players.Count, 6); //Clamp room player count to 6
            float EmergencyStopAndDelta;

            //emergency stop if not told to start and not enough plaeyrs currently in zone to start or continue with start
            if (!StartQuest.ToldToStart && !(__instance.HasEnoughPlayersToBeginCountdown(playerCount, playersInZoneCount) && Settings.ChairStartEnabled.Value))
            {
                EmergencyStopAndDelta = QuestStartProcess.START_PROCESS_DURATION;
            }
            else
            {
                if (Settings.ChairStartEnabled.Value) //only run countdown reset code fi chair start enabled.
                {
                    if (playerCount == playersInZoneCount) //drop countdown to 6 if all players sitting
                    {
                        ____allPlayersWereInZone = true;
                        MaxTimeLeft = QuestStartProcess.START_PROCESS_DURATION_ALLPLAYERS;
                    }
                    else if (____allPlayersWereInZone) //Reset countdown to 30 if players stood up
                    {
                        ____allPlayersWereInZone = false;
                        ____timeLeft = QuestStartProcess.START_PROCESS_DURATION;
                        MaxTimeLeft = QuestStartProcess.START_PROCESS_DURATION;
                    }
                }
                EmergencyStopAndDelta = -Time.deltaTime;
            }
            ____timeLeft = Mathf.Clamp(____timeLeft + EmergencyStopAndDelta, 0f, MaxTimeLeft);

            //Stop if int value not changed
            int timeLeftAsInt32 = (int)____timeLeft;
            if (timeLeftAsInt32 == __instance.SecondsLeft)
            {
                return false;
            }

            //Actually assigns time as read by HubQuestManager. Why this is a method and not utilizing the private setter, I will never know.
            __instance.SetSecondsLeft(timeLeftAsInt32);

            return false;
        }
    }
}
