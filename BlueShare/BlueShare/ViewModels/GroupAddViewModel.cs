using BlueShare.DAO;
using BlueShare.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.MultiSelectListView;

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
            this.SaveGroupCommand = new Command(SaveGroup);

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

        private void SaveGroup()
        {
            var userList = new List<UserModel>();
            foreach(UserModel user in this.Users.SelectedItems)
            {
                userList.Add(user);
            }

            this.Groups.Insert(new GroupModel { Name = this.GroupName, Users = userList });

            App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
