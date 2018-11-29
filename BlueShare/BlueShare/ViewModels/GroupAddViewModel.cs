using BlueShare.DAO;
using BlueShare.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.MultiSelectListView;
using Android;
using BlueShare.Miscellaneous;
using Android.Bluetooth;

namespace BlueShare.ViewModels
{
    public class GroupAddViewModel : BaseViewModel
    {
        private GroupDAO _GroupsDAO = new GroupDAO();
        private UserDAO _UsersDAO = new UserDAO();

        private Page PageOwner { get; set; }

        private string _GroupName { get; set; }
        public string GroupName { get { return _GroupName; } set { _GroupName = value; OnPropertyChanged("GroupName"); } }

        public MultiSelectObservableCollection<UserModel> Users { get; }

        public Command SaveGroupCommand { get;  set; }

        public GroupAddViewModel(Page pageOwner) : base()
        {
            this.GroupName = string.Empty;
            this.SaveGroupCommand = new Command(SaveGroupAsync);
            this.PageOwner = pageOwner;

            this.Users = new MultiSelectObservableCollection<UserModel>();

            foreach (BluetoothDevice device in DroidBluetooth.Adapter.BondedDevices)
            {
                if (this._UsersDAO.GetUserById(device.Address) == null)
                {
                    this.Users.Add(new UserModel { DeviceId = device.Address, DeviceName = device.Name });
                }
            }
        }

        private async void SaveGroupAsync()
        {
            if (ValidateNewGroup())
            {
                this._GroupsDAO.Insert(new GroupModel { Name = this.GroupName });
                var group = this._GroupsDAO.GetGroupByName(this.GroupName);

                foreach (UserModel user in this.Users.SelectedItems)
                {
                    user.IdGroup = group.Id;
                    this._UsersDAO.Insert(user);
                }

                await App.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await PageOwner.DisplayAlert("Aviso", "Grupo não definido corretamente", "Ok");
            }
        }

        private bool ValidateNewGroup()
        {
            return this.Users.SelectedItems != null 
                && !this._GroupName.Equals(string.Empty);
        }
    }
}
