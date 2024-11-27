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
            Settings.DefaultPlayerLimit = Config.Bind("Settings", "DefaultPlayerLimit", (byte)8);
            Settings.SliderLimit = Config.Bind("Settings", "SliderLimit", (byte)8);
            Settings.ChairStartEnabled = Config.Bind("Settings", "ChairStartEnabled", true);
            Harmony.PatchAll();
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}