using BlueShare.DAO;
using BlueShare.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.MultiSelectListView;
using Android;

namespace BlueShare.ViewModels
{
    public class GroupAddViewModel : BaseViewModel
    {
        private GroupDAO Groups;

        private string _GroupName { get; set; }
        public string GroupName { get { return _GroupName; } set { _GroupName = value; OnPropertyChanged("GroupName"); } }

        public MultiSelectObservableCollection<UserModel> Users { get; }

        public Command SaveGroupCommand { get;  set; }

        public GroupAddViewModel() : base()
        {
            this.GroupName = string.Empty;
            this.Groups = new GroupDAO();
            this.SaveGroupCommand = new Command(SaveGroupAsync);

            this.Users = new MultiSelectObservableCollection<UserModel>
            {
                //this.Groups.Search()
                new UserModel { DeviceId = "1", DeviceName = "Douglas" },
                new UserModel { DeviceId = "2", DeviceName = "Jefferson" },
                new UserModel { DeviceId = "3", DeviceName = "Fernanda" },
                new UserModel { DeviceId = "4", DeviceName = "Mauro" },
                new UserModel { DeviceId = "5", DeviceName = "Peter" }
            };
        }

        private async void SaveGroupAsync()
        {
            if (ValidateNewGroup())
            {
                var userList = new List<UserModel>();
                foreach (UserModel user in this.Users.SelectedItems)
                {
                    userList.Add(user);
                }

                this.Groups.Insert(new GroupModel { Name = this.GroupName, Users = userList });

                await App.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
               // await App.Current.MainPage.DisplayAlert("Aviso", "Grupo não definido", "Ok");
            }
        }

        private bool ValidateNewGroup()
        {
            return this.Users.Count > 0
                && !this._GroupName.Equals(string.Empty);
        }
    }
}
