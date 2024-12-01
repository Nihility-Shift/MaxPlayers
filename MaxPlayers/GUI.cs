using Photon.Pun;
using UnityEngine;
using VoidManager.CustomGUI;
using VoidManager.Utilities;
using WebSocketSharp;

namespace MaxPlayers
{
    internal class GUI : ModSettingsMenu
    {
        public override string Name()
        {
            return MyPluginInfo.USERS_PLUGIN_NAME;
        }

        string PlayerlimitStr;
        string ErrorText;

        public override void Draw()
        {
            if (!ErrorText.IsNullOrEmpty())
            {
                GUILayout.Label(ErrorText);
            }

            GUITools.DrawTextField("Slider Limit", ref Settings.SliderLimit);

            if (GUITools.DrawTextField("Player Limit", ref PlayerlimitStr, Limits.DefaultPlayerLimit.ToString()))
            {
                if (int.TryParse(PlayerlimitStr, out int result))
                {
                    if (!PhotonNetwork.InRoom)
                    {
                        ErrorText = "<color=red>Error: Cannot change Player Limit while not in room</color>";
                    }
                    else if (result <= 0 || result >= 256)
                    {
                        ErrorText = "<color=red>Error: Player Limit must be between 1 and 255</color>";
                    }
                    else
                    {
                        Limits.PlayerLimit = result;
                        ErrorText = null;
                    }
                }
            }

            GUITools.DrawTextField("Default Player Limit", ref Settings.DefaultPlayerLimit);

            GUITools.DrawCheckbox("Enable chair starting from hub", ref Settings.ChairStartEnabled);

            if(PhotonNetwork.InRoom && PhotonNetwork.IsMasterClient && GameSessionManager.InHub && GUILayout.Button("Start Quest"))
            {
                StartQuest.ExecuteStartQuest();
            }
        }

        public override void OnOpen()
        {
            PlayerlimitStr = Limits.PlayerLimit.ToString();
        }
    }
}
