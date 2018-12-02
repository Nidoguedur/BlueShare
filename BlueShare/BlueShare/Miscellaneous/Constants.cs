using Java.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueShare.Miscellaneous
{
    public static class Constants
    {
        public static UUID MY_UUID_SECURE = UUID.FromString("fa87c0d0-afac-11de-8a39-0800200c9a66");
        public static UUID MY_UUID_INSECURE = UUID.FromString("8ce255c0-200a-11e0-ac64-0800200c9a66");

        public const int MESSAGE_STATE_CHANGE = 1;
        public const int MESSAGE_READ = 2;
        public const int MESSAGE_WRITE = 3;
        public const int MESSAGE_DEVICE_NAME = 4;
        public const int MESSAGE_TOAST = 5;

        public const int STATE_NONE = 0;       
        public const int STATE_LISTEN = 1;     
        public const int STATE_CONNECTING = 2; 
        public const int STATE_CONNECTED = 3;

        public const string NAME_SECURE = "BluetoothChatSecure";
        public const string NAME_INSECURE = "BluetoothChatInsecure";

        public const string DEVICE_NAME = "device_name";
        public const string TOAST = "toast";
        public const string TAG = "BlueShareBluetooth";
    }
}
