using HarmonyLib;
using UI;
using UI.Settings;
using UnityEngine.UIElements;

namespace MaxPlayers.Patches
{
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
}
