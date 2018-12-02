using Android.Bluetooth;
using Android.OS;
using Android.Util;
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
        private Task<AcceptTask> AcceptTask;
        private Task<ConnectedTask> ConnectedTask;
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
            this.State = STATE_NONE;
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
                ConnectedTask.Dispose();
                ConnectedTask = null;
            }

            if (AcceptTask == null)
            {
                this.AcceptTask = new Task<>();
                AcceptTask.Start();
            }

            UpdateUserInterfaceTitle();
        }
    }
}
