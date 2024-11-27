using HarmonyLib;

namespace MaxPlayers.Patches
{
    [HarmonyPatch(typeof(MatchMakingMenu), MethodType.Constructor)]
    internal class MatchmakingPanelSlider
    {
        static void Postfix(MatchMakingMenu __instance)
        {
            __instance._playerLimit.defaultValue = Limits.DefaultPlayerLimit;
            __instance._playerLimit.SetValue(Limits.DefaultPlayerLimit);
            __instance._playerLimit.max = Limits.SliderLimit;
        }
    }

    [HarmonyPatch(typeof(LobbyPanel), MethodType.Constructor)]
    internal class LobbyPanelSlider
    {
        static void Postfix(LobbyPanel __instance)
        {
            __instance._playerLimit.defaultValue = Limits.DefaultPlayerLimit;
            __instance._playerLimit.SetValue(Limits.DefaultPlayerLimit);
            __instance._playerLimit.max = Limits.SliderLimit;
        }
    }
}
