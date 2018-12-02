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
        const string TAG = "BluetoothChatService";
        const string NAME_SECURE = "BluetoothChatSecure";
        static UUID MY_UUID_SECURE = UUID.FromString("fa87c0d0-afac-11de-8a39-0800200c9a66");

        // The local server socket
        BluetoothServerSocket serverSocket;
        string socketType;
        ChatService Service;

        public AcceptTask(ChatService service)
        {
            BluetoothServerSocket tmp = null;
            socketType = "Secure";
            this.Service = service;

            try
            {
                tmp = DroidBluetooth.Adapter.ListenUsingRfcommWithServiceRecord(NAME_SECURE, MY_UUID_SECURE);
            }
            catch (Java.IO.IOException e)
            {
                Log.Error(TAG, "listen() failed", e);
            }
            serverSocket = tmp;
            service.State = Constants.STATE_LISTEN;
        }

        public void Run()
        {
            BluetoothSocket socket = null;

            while (Service.GetState() != Constants.STATE_CONNECTED)
            {
                try
                {
                    socket = serverSocket.Accept();
                }
                catch (Java.IO.IOException e)
                {
                    Log.Error(TAG, "accept() failed", e);
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
                                this.Service.Connected(socket, socket.RemoteDevice, socketType);
                                break;
                            case Constants.STATE_NONE:
                            case Constants.STATE_CONNECTED:
                                try
                                {
                                    socket.Close();
                                }
                                catch (Java.IO.IOException e)
                                {
                                    Log.Error(TAG, "Could not close unwanted socket", e);
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
                serverSocket.Close();
            }
            catch (Java.IO.IOException e)
            {
                Log.Error(TAG, "close() of server failed", e);
            }
        }
    }
}
