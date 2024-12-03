using BepInEx.Configuration;

namespace MaxPlayers
{
    internal class Settings
    {
        internal static ConfigEntry<byte> DefaultPlayerLimit;
        internal const byte defaultplayerlimit = 8;
        internal static ConfigEntry<byte> SliderLimit;
        internal const byte defaultsliderlimit = 24;
        internal static ConfigEntry<bool> ChairStartEnabled;
    }
}
