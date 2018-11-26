using BlueShare.DAO;
using BlueShare.Models;
using BlueShare.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BlueShare.ViewModels
{
    public class GroupsViewModel : BaseViewModel
    {
        private readonly GroupDAO _GroupDAO = new GroupDAO();
        public List<GroupModel> ListGroups { get; set; }
        public Command AddGroupCommand { get; set; }

        public GroupsViewModel() : base()
        {
            this.ListGroups = new List<GroupModel>(this._GroupDAO.Search());
            this.AddGroupCommand = new Command(ShowAddGroup);
        }

        private void ShowAddGroup()
        {
            App.Current.MainPage.Navigation.PushAsync(new GroupAdd());
        }
    }
}
