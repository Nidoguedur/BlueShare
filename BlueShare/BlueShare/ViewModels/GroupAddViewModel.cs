using BlueShare.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms.MultiSelectListView;

namespace BlueShare.ViewModels
{
    public class GroupAddViewModel : BaseViewModel
    {
        public MultiSelectObservableCollection<UserModel> Users { get; }

        public GroupAddViewModel() : base()
        {
            this.Users = new MultiSelectObservableCollection<UserModel>();

            this.Users.Add(new UserModel { DeviceId = "1", DeviceName = "Douglas" });
            this.Users.Add(new UserModel { DeviceId = "2", DeviceName = "Jefferson" });
            this.Users.Add(new UserModel { DeviceId = "3", DeviceName = "Fernanda" });
            this.Users.Add(new UserModel { DeviceId = "4", DeviceName = "Mauro" });
            this.Users.Add(new UserModel { DeviceId = "5", DeviceName = "Peter" });
        }
    }
}
