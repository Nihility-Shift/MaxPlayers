using Photon.Pun;

namespace MaxPlayers
{
    internal class Limits
    {
        public static int DefaultPlayerLimit
        {
            get
            {
                return Settings.DefaultPlayerLimit.Value;
            }
            set
            {
                if (!(value < 256 && value > -1))
                {
                    BepinPlugin.Log.LogWarning("Cannont assign DefaultPlayerLimit to value not between 0 and 255. Attempted setting as " + value.ToString());
                    return;
                }

                Settings.DefaultPlayerLimit.Value = (byte)value;
            }
        }

        public static int PlayerLimit
        {
            get
            {
                if(!PhotonNetwork.InRoom)
                {
                    return 0;
                }
                return PhotonService.Instance.GetCurrentPlayerLimit();
            }
            set
            {
                if (!(value < 256 && value > -1))
                {
                    BepinPlugin.Log.LogWarning("Cannont assign PlayerLimit to value not between 0 and 255. Attempted setting as " + value.ToString());
                    return;
                }

                PhotonService.Instance.SetCurrentRoomPlayerLimit(value);
            }
        }

        public static int SliderLimit
        {
            get
            {
                return Settings.SliderLimit.Value;
            }
            set
            {
                if (!(value < 256 && value > -1))
                {
                    BepinPlugin.Log.LogWarning("Cannont assign SliderLimit to value not between 0 and 255. Attempted setting as " + value.ToString());
                    return;
                }

                Settings.SliderLimit.Value = (byte)value;
            }
        }
    }
}
