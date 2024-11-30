using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace MaxPlayers
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.USERS_PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Void Crew.exe")]
    [BepInDependency(VoidManager.MyPluginInfo.PLUGIN_GUID)]
    public class BepinPlugin : BaseUnityPlugin
    {
        internal static readonly Harmony Harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

        internal static ManualLogSource Log;

        private void Awake()
        {
            // Plugin startup logic
            Log = Logger;
            Settings.DefaultPlayerLimit = Config.Bind("Settings", "DefaultPlayerLimit", Settings.defaultplayerlimit, "The player limit you'll default to.");
            Settings.SliderLimit = Config.Bind("Settings", "SliderLimit", Settings.defaultsliderlimit, "How high the slider can slide.");
            Settings.ChairStartEnabled = Config.Bind("Settings", "ChairStartEnabled", true, "While enabled, players can start a session solely by sitting down. Disable if you want the host to maintain control.");
            Harmony.PatchAll();
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}