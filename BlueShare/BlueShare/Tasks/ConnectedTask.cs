using Android.Bluetooth;
using Android.Util;
using BlueShare.Miscellaneous;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlueShare.Threads
{
    public class ConnectedTask
    {
        /// <summary>
        /// This thread runs during a connection with a remote device.
        /// It handles all incoming and outgoing transmissions.
        /// </summary>
        private BluetoothSocket Socket;
        private Stream InStream, OutStream;
        private ChatService Service;

        public ConnectedTask(BluetoothSocket socket, ChatService service, string socketType)
        {
            Log.Debug(Constants.TAG, $"create ConnectedThread: {socketType}");
            this.Socket = socket;
            this.Service = service;
            Stream tmpIn = null;
            Stream tmpOut = null;

            // Get the BluetoothSocket input and output streams
            try
            {
                tmpIn = this.Socket.InputStream;
                tmpOut = this.Socket.OutputStream;
            }
            catch (Java.IO.IOException e)
            {
                Log.Error(Constants.TAG, "temp sockets not created", e);
            }

            this.InStream = tmpIn;
            this.OutStream = tmpOut;
            service.State = Constants.STATE_CONNECTED;
        }

        public void Run()
        {
            Log.Info(Constants.TAG, "BEGIN mConnectedThread");
            byte[] buffer = new byte[1024];
            int bytes;

            // Keep listening to the InputStream while connected
            while (this.Service.GetState() == Constants.STATE_CONNECTED)
            {
                try
                {
                    // Read from the InputStream
                    bytes = this.InStream.Read(buffer, 0, buffer.Length);

                    // Send the obtained bytes to the UI Activity
                    this.Service.Handler.ObtainMessage(Constants.MESSAGE_READ, bytes, -1, buffer).SendToTarget();
                }
                catch (Java.IO.IOException e)
                {
                    Log.Error(Constants.TAG, "disconnected", e);
                    this.Service.ConnectionLost();
                    break;
                }
            }
        }

        /// <summary>
        /// Write to the connected OutStream.
        /// </summary>
        /// <param name='buffer'>
        /// The bytes to write
        /// </param>
        public void Write(byte[] buffer)
        {
            try
            {
                this.OutStream.Write(buffer, 0, buffer.Length);

                // Share the sent message back to the UI Activity
                this.Service.Handler.ObtainMessage(Constants.MESSAGE_WRITE, -1, -1, buffer).SendToTarget();
            }
            catch (Java.IO.IOException e)
            {
                Log.Error(Constants.TAG, "Exception during write", e);
            }
        }

        public void Cancel()
        {
            try
            {
                this.Socket.Close();
            }
            catch (Java.IO.IOException e)
            {
                Log.Error(Constants.TAG, "close() of connect socket failed", e);
            }
        }

    }
}
