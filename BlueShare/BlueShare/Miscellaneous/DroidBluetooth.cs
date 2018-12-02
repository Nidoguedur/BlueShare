using Android.Bluetooth;

namespace BlueShare.Miscellaneous
{
    public class DroidBluetooth
    {
        public static BluetoothAdapter Adapter
        {
            get
            {
                return BluetoothAdapter.DefaultAdapter;
            }
        }

        public static bool Enable()
        {
            if (!Adapter.IsEnabled)
            {
                return Adapter.Enable();
            }

            return false;
        }
    }
}
