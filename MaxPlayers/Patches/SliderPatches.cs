using HarmonyLib;
using HarmonyLib.Tools;
using System;
using System.Diagnostics;
using System.Text;
using UI;
using UI.Settings;
using UnityEngine.UIElements;

namespace MaxPlayers.Patches
{
    [HarmonyPatch(typeof(IntSetting), MethodType.Constructor, new Type[] { typeof(int), typeof(int), typeof(int) })]
    internal class IntSettingOverride
    {
        static void Prefix(IntSetting __instance, ref int defaultValue, ref int min, ref int max)
        {
            var methodName = new StackTrace().GetFrame(2).GetMethod().ReflectedType.Name;
            if (methodName == "LobbyPanel" || methodName == "MatchMakingMenu" || methodName == "MatchmakingRoomSetup")
            {
                defaultValue = Limits.DefaultPlayerLimit;
                max = Limits.SliderLimit;
            }
        }
    }
    [HarmonyPatch(typeof(SliderIntSettingEntryVE), "Init")]
    internal class SliderIntSettingEntryVEOverride
    {
        static void Postfix(SliderIntSettingEntryVE __instance)
        {
            if (__instance.DisplayName.Contains("player_limit") && __instance.setting != null)
            {
                __instance.HighValue = __instance.setting.max;
                __instance.slider.highValue = __instance.setting.max;

                VisualElement stepContainer = __instance.slider.parent.Q(name: "StepContainer", className: null);
                if (stepContainer != null)
                {
                    stepContainer.RemoveChildren();
                    __instance.SetupContainers();
                }
            }
        }
    }
}
