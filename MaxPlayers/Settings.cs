using BepInEx.Configuration;

namespace MaxPlayers
{
    internal class Settings
    {
        internal static ConfigEntry<byte> DefaultPlayerLimit;
        internal const byte defaultplayerlimit = 16;
        internal static ConfigEntry<byte> SliderLimit;
        internal const byte defaultsliderlimit = 20;
        internal static ConfigEntry<bool> ChairStartEnabled;
    }
}
