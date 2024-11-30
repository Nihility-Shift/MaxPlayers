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
            var methodInfo = new StackTrace().GetFrame(2).GetMethod();
            if (methodInfo.ReflectedType.Name == "LobbyPanel" || methodInfo.ReflectedType.Name == "MatchMakingMenu" || methodInfo.ReflectedType.Name == "MatchmakingRoomSetup")
            {
                defaultValue = Limits.DefaultPlayerLimit;
                max = Limits.SliderLimit;
            }
            //else if (max == 4) { BepinPlugin.Log.LogInfo($"PATCH THIS {methodInfo.ReflectedType.Name} IntSetting Values: {defaultValue}, {min}, {max}"); }
        }
        /*static void Postfix(IntSetting __instance, ref int defaultValue, ref int min, ref int max)
        {
            var methodInfo = new StackTrace().GetFrame(2).GetMethod();
            if (methodInfo.ReflectedType.Name == "LobbyPanel" || methodInfo.ReflectedType.Name == "MatchMakingMenu" || methodInfo.ReflectedType.Name == "MatchmakingRoomSetup")
            {
                BepinPlugin.Log.LogInfo($"{methodInfo.ReflectedType.Name} IntSetting Values: {defaultValue}, {min}, {max}");
            }
        }*/
    }
    [HarmonyPatch(typeof(SliderIntSettingEntryVE), "Init")]
    internal class SliderIntSettingEntryVEOverride
    {
        static void Postfix(SliderIntSettingEntryVE __instance)
        {
            //BepinPlugin.Log.LogInfo($"Slider - {__instance.DisplayName}, {__instance.settingsLabel}, {__instance.SettingName} | {__instance.DisplayName.Contains("player_limit")} && {__instance.setting != null} - {__instance.setting.max}");
            if (__instance.DisplayName.Contains("player_limit") && __instance.setting != null)
            {
                __instance.HighValue = __instance.setting.max;
                __instance.slider.highValue = __instance.setting.max;

                VisualElement stepContainer = __instance.slider.parent.Q(name: "StepContainer", className: null);
                if (stepContainer != null)
                {
                    stepContainer.Clear();

                    int num = __instance.setting.max - __instance.LowValue + 2;
                    for (int i = __instance.LowValue; i < num; i++)
                    {
                        VisualElement stepElement = new VisualElement
                        {
                            name = i.ToString()
                        };
                        int stepValue = i;
                        stepElement.RegisterClickEvent(delegate
                        {
                            __instance.setting.SetValue(stepValue, false);
                            __instance.Reload();
                        });
                        stepContainer.Add(stepElement);
                    }
                }
            }
        }
    }
}
