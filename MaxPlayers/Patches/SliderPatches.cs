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
    [HarmonyPatch(typeof(SliderIntSettingEntryVE), "Reload")]
    internal class SliderIntSettingEntryVEOverride
    {
        static void Postfix(SliderIntSettingEntryVE __instance)
        {
            if (__instance.DisplayName.Contains("player_limit") && __instance.setting != null && __instance.HighValue != Limits.SliderLimit)
            {
                __instance.HighValue = Limits.SliderLimit;
                __instance.slider.highValue = Limits.SliderLimit;
                __instance.setting.max = Limits.SliderLimit;

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
