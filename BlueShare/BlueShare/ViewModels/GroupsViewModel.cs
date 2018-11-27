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

        private Page PageOwner { get; set; }

        private List<GroupModel> _ListGroups { get; set; }
        public List<GroupModel> ListGroups { get { return _ListGroups; } set { _ListGroups = value; OnPropertyChanged("ListGroups"); } }

        public Command AddGroupCommand
        {
            get
            {
                return new Command(() =>
                {
                    this.PageOwner.Navigation.PushAsync(new GroupAdd());
                });
            }
        }

        public Command RefreshListGroupsCommand
        {
            get
            {
                return new Command(() =>
                {
                    this.IsRefreshing = true;
                    this.ListGroups = null;
                    this.ListGroups = this._GroupDAO.Search();
                    this.IsRefreshing = false;
                });
            }
        }

        private bool _IsRefreshing = false;
        public bool IsRefreshing { get { return _IsRefreshing; } set { _IsRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); } }

        public GroupsViewModel(Page pageOwner) : base()
        {
            this.PageOwner = pageOwner;
            this._ListGroups = new List<GroupModel>(this._GroupDAO.Search());
            //this.AddGroupCommand = new Command(ShowAddGroup);
            //this.RefreshListGroupsCommand = new Command(RefreshListGroupsData);
            this._IsRefreshing = false;
        }

        private void RefreshListGroupsData()
        {
        }

        private void ShowAddGroup()
        {
        }
    }
}
