using BepInEx;
using HarmonyLib;

namespace MaxPlayers
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static readonly Harmony Harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        public static byte PlayerCount = 4;
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            Harmony.PatchAll();
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is patched!");
        }
    }
}