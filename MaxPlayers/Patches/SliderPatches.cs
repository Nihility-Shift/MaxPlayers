using HarmonyLib;
using System;
using System.Diagnostics;
using UI;
using UI.Settings;
using UnityEngine.UIElements;

namespace MaxPlayers.Patches
{
    /*[HarmonyPatch(typeof(IntSetting), MethodType.Constructor, new Type[] { typeof(int), typeof(int), typeof(int) })]
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
    }*/
    [HarmonyPatch(typeof(SliderIntSettingEntryVE), "Init")]
    internal class SliderIntSettingEntryVEOverride
    {
        static void Postfix(SliderIntSettingEntryVE __instance)
        {
            if (__instance.DisplayName.Contains("player_limit") && __instance.setting != null)
            {
                __instance.setting.max = Limits.SliderLimit;
                __instance.HighValue = __instance.setting.max;
                __instance.slider.highValue = __instance.setting.max;
                __instance.slider.value = Limits.DefaultPlayerLimit;

                VisualElement stepContainer = __instance.slider.parent.Q(name: "StepContainer", className: null);
                if (stepContainer != null)
                {
                    stepContainer.RemoveChildren();
                    __instance.SetupContainers();
                }
            }
        }
    }

    /*
    [HarmonyPatch(typeof(MatchMakingMenu), "Init")]
    internal class MatchmakingPanelSlider
    {
        static void Prefix(MatchMakingMenu __instance)
        {
            __instance._playerLimit.max = Limits.SliderLimit;
            __instance._playerLimit.defaultValue = Limits.DefaultPlayerLimit;
            __instance._playerLimit.SetValue(Limits.DefaultPlayerLimit);
        }

        static void Postfix(MatchMakingMenu __instance)
        {
            var playerLimitVE = __instance._playerLimitVE;
            playerLimitVE.HighValue = Limits.SliderLimit;
            playerLimitVE.Init(__instance._playerLimit);
            VisualElement stepContainer = playerLimitVE.Q("StepContainer", null, null);
            stepContainer.RemoveChildren();
            playerLimitVE.SetupContainers();
        }
    }
    
    [HarmonyPatch(typeof(LobbyPanel), MethodType.Constructor, new Type[] { typeof(VisualElement), typeof(VisualTreeAsset) })]
    internal class LobbyPanelSlider
    {
        static void Postfix(LobbyPanel __instance)
        {
            SliderIntSettingEntryVE playerLimitVE = __instance._playerLimitVE;

            __instance._playerLimit.max = Limits.SliderLimit;
            __instance._playerLimit.defaultValue = Limits.DefaultPlayerLimit;
            __instance._playerLimit.SetValue(Limits.DefaultPlayerLimit);
            playerLimitVE.HighValue = Limits.SliderLimit;
            playerLimitVE.Init(__instance._playerLimit);
            VisualElement stepContainer = playerLimitVE.Q("StepContainer", null, null);
            stepContainer.RemoveChildren();
            playerLimitVE.SetupContainers();
            BepinPlugin.Log.LogInfo($"LPVE set to {playerLimitVE.HighValue}");
        }
    }aw
    
    [HarmonyPatch(typeof(MatchmakingRoomSetup), MethodType.Constructor, new Type[] { typeof(VisualElement) })]
    internal class MatchMakingRoomSetupSlider
    {
        static void Postfix(MatchmakingRoomSetup __instance)
        {
            SliderIntSettingEntryVE playerLimitVE = __instance.playerLimitVE;

            __instance.playerLimit.max = Limits.SliderLimit;
            __instance.playerLimit.defaultValue = Limits.DefaultPlayerLimit;
            __instance.playerLimit.SetValue(Limits.DefaultPlayerLimit);
            playerLimitVE.HighValue = Limits.SliderLimit;
            playerLimitVE.Init(__instance.playerLimit);
            playerLimitVE.SetupContainers();
            BepinPlugin.Log.LogInfo($"MMRSVE set to {playerLimitVE.HighValue}");
        }
    }*/
}
