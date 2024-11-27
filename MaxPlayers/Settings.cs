using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxPlayers
{
    internal class Settings
    {
        internal static ConfigEntry<byte> DefaultPlayerLimit;
        internal static ConfigEntry<byte> SliderLimit;
        internal static ConfigEntry<bool> ChairStartEnabled;
    }
}
