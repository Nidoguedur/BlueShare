using Android.Bluetooth;
using Android.Util;
using BlueShare.Miscellaneous;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlueShare.Threads
{
    public class AcceptTask
    {
        // The local server socket
        private BluetoothServerSocket ServerSocket;
        private string SocketType;
        private ChatService Service;

        public AcceptTask(ChatService service)
        {
            BluetoothServerSocket tmp = null;

            this.SocketType = "Secure";
            this.Service = service;

            try
            {
                tmp = DroidBluetooth.Adapter.ListenUsingRfcommWithServiceRecord(Constants.NAME_SECURE, Constants.MY_UUID_SECURE);
            }
            catch (Java.IO.IOException e)
            {
                Log.Error(Constants.TAG, "listen() failed", e);
            }
            ServerSocket = tmp;
            service.State = Constants.STATE_LISTEN;
        }

        public void Run()
        {
            BluetoothSocket socket = null;

            while (Service.GetState() != Constants.STATE_CONNECTED)
            {
                try
                {
                    socket = ServerSocket.Accept();
                }
                catch (Java.IO.IOException e)
                {
                    Log.Error(Constants.TAG, "accept() failed", e);
                    break;
                }

                if (socket != null)
                {
                    lock (this)
                    {
                        switch (Service.GetState())
                        {
                            case Constants.STATE_LISTEN:
                            case Constants.STATE_CONNECTING:
                                this.Service.Connected(socket, socket.RemoteDevice, SocketType);
                                break;
                            case Constants.STATE_NONE:
                            case Constants.STATE_CONNECTED:
                                try
                                {
                                    socket.Close();
                                }
                                catch (Java.IO.IOException e)
                                {
                                    Log.Error(Constants.TAG, "Could not close unwanted socket", e);
                                }
                                break;
                        }
                    }
                }
            }
        }

        public void Cancel()
        {
            try
            {
                ServerSocket.Close();
            }
            catch (Java.IO.IOException e)
            {
                Log.Error(Constants.TAG, "close() of server failed", e);
            }
        }
    }
}
