using System;
using System.Collections.Generic;
using System.Text;

namespace BlueShare.Miscellaneous
{
    public static class Constants
    {
        public const int MESSAGE_STATE_CHANGE = 1;
        public const int MESSAGE_READ = 2;
        public const int MESSAGE_WRITE = 3;
        public const int MESSAGE_DEVICE_NAME = 4;
        public const int MESSAGE_TOAST = 5;

        public const int STATE_NONE = 0;       
        public const int STATE_LISTEN = 1;     
        public const int STATE_CONNECTING = 2; 
        public const int STATE_CONNECTED = 3;  

        public const string DEVICE_NAME = "device_name";
        public const string TOAST = "toast";
    }
}
