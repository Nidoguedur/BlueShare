using Android.Bluetooth;
using Android.OS;
using Android.Util;
using BlueShare.Tasks;
using BlueShare.Threads;
using Java.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlueShare.Miscellaneous
{
    public class ChatService
    {
        public Handler Handler { get; private set; }
        private AcceptTask AcceptTaskSecure;
        private AcceptTask AcceptTaskInsecure;
        public ConnectTask ConnectTask { get; set; }
        public ConnectedTask ConnectedTask { get; private set; }
        public int State { get; set; }
        public int NewState { get; set; }

        /// <summary>
        /// Constructor. Prepares a new BluetoothChat session.
        /// </summary>
        /// <param name='handler'>
        /// A Handler to send messages back to the UI Activity.
        /// </param>
        public ChatService(Handler handler)
        {
            this.State = Constants.STATE_NONE;
            this.NewState = this.State;
            this.Handler = handler;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        void UpdateUserInterfaceTitle()
        {
            this.State = GetState();
            this.NewState = this.State;
            this.Handler.ObtainMessage(Constants.MESSAGE_STATE_CHANGE, this.NewState, -1).SendToTarget();
        }

        /// <summary>
        /// Return the current connection state.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int GetState()
        {
            return this.State;
        }

        // Start the chat service. Specifically start AcceptThread to begin a
        // session in listening (server) mode. Called by the Activity onResume()
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Start()
        {
            if (ConnectedTask != null)
            {
                ConnectedTask = null;
            }

            if (this.ConnectTask != null)
            {
                this.ConnectedTask = null;
            }

            if (AcceptTaskSecure == null)
            {
                this.AcceptTaskSecure = new AcceptTask(this);

                Task.Factory.StartNew(() =>
                {
                    this.AcceptTaskSecure.Run();
                });
            }

            if (this.AcceptTaskInsecure == null)
            {
                this.AcceptTaskInsecure = new AcceptTask(this);

                Task.Factory.StartNew(() =>
                {
                    this.AcceptTaskInsecure.Run();
                });
            }

            UpdateUserInterfaceTitle();
        }

        /// <summary>
        /// Start the ConnectThread to initiate a connection to a remote device.
        /// </summary>
        /// <param name='device'>
        /// The BluetoothDevice to connect.
        /// </param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Connect(BluetoothDevice device, bool secure)
        {
            if (this.State == Constants.STATE_CONNECTING && this.ConnectedTask != null)
            {
                this.ConnectTask.Cancel();
                this.ConnectTask = null;
            }

            if (this.ConnectedTask != null)
            {
                this.ConnectedTask.Cancel();
                this.ConnectedTask = null;
            }

            // Start the thread to connect with the given device
            this.ConnectTask = new ConnectTask(device, this, secure);
            Task.Factory.StartNew(() => 
            {
                this.ConnectTask.Run();
            });

            UpdateUserInterfaceTitle();
        }

        /// <summary>
        /// Start the ConnectedThread to begin managing a Bluetooth connection
        /// </summary>
        /// <param name='socket'>
        /// The BluetoothSocket on which the connection was made.
        /// </param>
        /// <param name='device'>
        /// The BluetoothDevice that has been connected.
        /// </param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Connected(BluetoothSocket socket, BluetoothDevice device, string socketType)
        {
            // Cancel the thread that completed the connection
            if (this.ConnectTask != null)
            {
                this.ConnectTask.Cancel();
                this.ConnectTask = null;
            }

            // Cancel any thread currently running a connection
            if (this.ConnectedTask != null)
            {
                this.ConnectedTask.Cancel();
                this.ConnectedTask = null;
            }

            if (this.AcceptTaskSecure != null)
            {
                this.AcceptTaskSecure.Cancel();
                this.AcceptTaskSecure = null;
            }

            if (this.AcceptTaskInsecure != null)
            {
                this.AcceptTaskInsecure.Cancel();
                this.AcceptTaskInsecure = null;
            }

            // Start the thread to manage the connection and perform transmissions
            this.ConnectedTask = new ConnectedTask(socket, this, socketType);

            Task.Factory.StartNew(() => 
            {
                this.ConnectedTask.Run();
            });

            // Send the name of the connected device back to the UI Activity
            var msg = this.Handler.ObtainMessage(Constants.MESSAGE_DEVICE_NAME);
            Bundle bundle = new Bundle();
            bundle.PutString(Constants.DEVICE_NAME, device.Name);
            msg.Data = bundle;
            this.Handler.SendMessage(msg);

            UpdateUserInterfaceTitle();
        }

        /// <summary>
        /// Stop all threads.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Stop()
        {
            if (this.ConnectTask != null)
            {
                this.ConnectTask.Cancel();
                this.ConnectTask = null;
            }

            if (this.ConnectedTask != null)
            {
                this.ConnectedTask.Cancel();
                this.ConnectedTask = null;
            }

            if (this.AcceptTaskSecure != null)
            {
                this.AcceptTaskSecure.Cancel();
                this.AcceptTaskSecure = null;
            }

            if (this.AcceptTaskInsecure != null)
            {
                this.AcceptTaskInsecure.Cancel();
                this.AcceptTaskInsecure = null;
            }

            this.State = Constants.STATE_NONE;
            UpdateUserInterfaceTitle();
        }

        /// <summary>
        /// Write to the ConnectedThread in an unsynchronized manner
        /// </summary>
        /// <param name='out'>
        /// The bytes to write.
        /// </param>
        public void Write(byte[] @out)
        {
            // Create temporary object
            ConnectedTask r;
            // Synchronize a copy of the ConnectedThread
            lock (this)
            {
                if (this.State != Constants.STATE_CONNECTED)
                {
                    return;
                }

                r = this.ConnectedTask;
            }

            // Perform the write unsynchronized
            r.Write(@out);
        }

        /// <summary>
        /// Indicate that the connection attempt failed and notify the UI Activity.
        /// </summary>
        public void ConnectionFailed()
        {
            this.State = Constants.STATE_LISTEN;

            var msg = this.Handler.ObtainMessage(Constants.MESSAGE_TOAST);
            var bundle = new Bundle();
            bundle.PutString(Constants.TOAST, "Unable to connect device");
            msg.Data = bundle;
            this.Handler.SendMessage(msg);
            Start();
        }

        /// <summary>
        /// Indicate that the connection was lost and notify the UI Activity.
        /// </summary>
        public void ConnectionLost()
        {
            var msg = this.Handler.ObtainMessage(Constants.MESSAGE_TOAST);
            var bundle = new Bundle();
            bundle.PutString(Constants.TOAST, "Unable to connect device.");
            msg.Data = bundle;
            this.Handler.SendMessage(msg);

            this.State = Constants.STATE_NONE;
            UpdateUserInterfaceTitle();
            this.Start();
        }

    }
}
