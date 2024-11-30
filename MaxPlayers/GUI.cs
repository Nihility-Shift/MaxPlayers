using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoidManager.CustomGUI;
using VoidManager.Utilities;
using WebSocketSharp;
using UnityEngine;

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
                if(int.TryParse(PlayerlimitStr, out int result))
                {
                    if(result > 0 && result < 256)
                    {
                        Limits.PlayerLimit = result;
                        ErrorText = null;
                    }
                    else
                    {
                        ErrorText = "<color=red>Error: Player Limit must be between 1 and 255</color>";
                    }
                }
            }
        }

        public override void OnOpen()
        {
            PlayerlimitStr = Limits.PlayerLimit.ToString();
        }
    }
}
