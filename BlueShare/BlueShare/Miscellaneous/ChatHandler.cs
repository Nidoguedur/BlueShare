using Android;
using Android.OS;
using System.Text;
using BlueShare.ViewModels;

namespace BlueShare.Miscellaneous
{
    public class ChatHandler : Handler
    {
        ChatViewModel chat;
        public ChatHandler(ChatViewModel frag)
        {
            chat = frag;
        }

        public override void HandleMessage(Message msg)
        {
            switch (msg.What)
            {
                case Constants.MESSAGE_STATE_CHANGE:
                    switch (msg.What)
                    {
                        case Constants.STATE_CONNECTED:
                            chat.Message = string.Empty;
                            break;
                    }
                    break;
                case Constants.MESSAGE_WRITE:
                    var writeBuffer = (byte[])msg.Obj;
                    var writeMessage = Encoding.ASCII.GetString(writeBuffer);
                    chat.Message += writeMessage;
                    break;
                case Constants.MESSAGE_READ:
                    var readBuffer = (byte[])msg.Obj;
                    var readMessage = Encoding.ASCII.GetString(readBuffer);
                    chat.Message += readMessage;
                    break;
            }
        }
    }
}
