using Android.Bluetooth;
using Android.Util;
using BlueShare.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueShare.Tasks
{
    public class ConnectTask
    {
        private BluetoothSocket Socket;
        private BluetoothDevice Device;
        private ChatService Service;
        private string SocketType;

        public ConnectTask(BluetoothDevice device, ChatService service, bool secure)
        {
            this.Device = device;
            this.Service = service;
            BluetoothSocket tmp = null;
            this.SocketType = secure ? "Secure" : "Insecure";

            try
            {
                if (secure)
                {
                    tmp = device.CreateRfcommSocketToServiceRecord(Constants.MY_UUID_SECURE);
                }
                else
                {
                    tmp = device.CreateInsecureRfcommSocketToServiceRecord(Constants.MY_UUID_INSECURE);
                }

            }
            catch (Java.IO.IOException e)
            {
                Log.Error(Constants.TAG, "create() failed", e);
            }
            Socket = tmp;
            service.State = Constants.STATE_CONNECTING;
        }

        public void Run()
        {
            // Always cancel discovery because it will slow down connection
            DroidBluetooth.Adapter.CancelDiscovery();

            // Make a connection to the BluetoothSocket
            try
            {
                // This is a blocking call and will only return on a
                // successful connection or an exception
                Socket.Connect();
            }
            catch (Java.IO.IOException e)
            {
                // Close the socket
                try
                {
                    Socket.Close();
                }
                catch (Java.IO.IOException e2)
                {
                    Log.Error(Constants.TAG, $"unable to close() {this.SocketType} socket during connection failure.", e2);
                }

                // Start the service over to restart listening mode
                this.Service.ConnectionFailed();
                return;
            }

            // Reset the ConnectThread because we're done
            lock (this)
            {
                this.Service.ConnectTask = null;
            }

            // Start the connected thread
            this.Service.Connected(this.Socket, this.Device, this.SocketType);
        }

        public void Cancel()
        {
            try
            {
                Socket.Close();
            }
            catch (Java.IO.IOException e)
            {
                Log.Error(Constants.TAG, "close() of connect socket failed", e);
            }
        }
    }
}
