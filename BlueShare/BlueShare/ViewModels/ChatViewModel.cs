using BlueShare.DAO;
using BlueShare.Miscellaneous;
using BlueShare.Models;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BlueShare.ViewModels
{
    public partial class ChatViewModel : BaseViewModel
    {
        private string _Message;
        public string Message { get { return _Message; } set { _Message = value; OnPropertyChanged("Message"); } }

        private Page PageOwner { get; set; }

        private UserDAO UsersDAO = new UserDAO();
        private ChatService ChatService;
        private ChatHandler _Handler;

        public Command SendMessageCommand
        {
            get
            {
                return new Command(() => {
                    if (this.ChatService.GetState() != Constants.STATE_CONNECTED)
                    {
                        this.PageOwner.DisplayAlert("Serviço não conectado", "O serviço de chat não está disponível no momento", "Ok");
                        return;
                    }

                    if (this.Message.Length > 0)
                    {
                        var bytes = Encoding.ASCII.GetBytes(this.Message);
                        this.ChatService.Write(bytes);
                        this.Message = string.Empty;
                    }
                });
            }           
        }

        public ChatViewModel(Page pageOwner, GroupModel groups)
        {
            this.PageOwner = pageOwner;
            this.Message = string.Empty;
            this._Handler = new ChatHandler(this);
            this.ChatService = new ChatService(this._Handler);
            this.ChatService.Start();

            foreach(UserModel user in this.UsersDAO.GetUsersByGroupId(groups.Id))
            {
                var device = DroidBluetooth.Adapter.GetRemoteDevice(user.DeviceId);

                if (device != null)
                {
                    this.ChatService.Connect(device, true);
                }
            }
        }
    }
}
