using CG.GameLoopStateMachine.GameStates;
using HarmonyLib;
using PlatformAbstractionLayer;
using System;

namespace MaxPlayers.Patches
{
    //PAL attempts to join the lobby after the user has already joined the lobby. This sets the PAL currentLobbyID to the current lobby for joining,
    // which should prevent a duplicate join. PlatformService.ExecuteJoinLobby checks that the currentLobbyID does equal the current room.

    // Didn't work -_-.

    [HarmonyPatch(typeof(GSJoinExternalGame), "SetPhotonRoomToJoin")]
    internal class BullshitVanillaRejoinFuckupPatch
    {
        static void Postfix(string room)
        {
            try
            {
                PlatformService.Instance.currentLobbyID = new LobbyID(room);
            }
            catch(Exception ex)
            {
                BepinPlugin.Log.LogError(ex);
            }
        }
    }
}
