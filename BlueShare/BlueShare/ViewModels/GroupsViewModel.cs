using BlueShare.DAO;
using BlueShare.Models;
using BlueShare.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.MultiSelectListView;

namespace BlueShare.ViewModels
{
    public class GroupsViewModel : BaseViewModel
    {
        private readonly GroupDAO _GroupDAO = new GroupDAO();    
        private Page PageOwner { get; set; }

        private List<GroupModel> _ListGroups;
        public List<GroupModel> ListGroups
        {
            get
            {
                return _ListGroups;
            }
            set
            {
                _ListGroups = value;
                OnPropertyChanged("ListGroups");
            }
        }

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
                    this.ListGroups.Clear();

                    this._GroupDAO.Search().ForEach(group =>
                    {
                        this.ListGroups.Add(group);
                    });

                    this.IsRefreshing = false;
                });
            }
        }

        //public Command RemoveGroupsSelectedCommand
        //{
        //    get
        //    {
        //        return new Command(() =>
        //        {
        //            foreach (GroupModel group in this.ListGroups.SelectedItems)
        //            {
        //                this._GroupDAO.Delete(group);
        //            }
        //        });

        //    }
        //}

        private bool _IsRefreshing = false;
        public bool IsRefreshing { get { return _IsRefreshing; } set { _IsRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); } }

        public GroupsViewModel(Page pageOwner) : base()
        {
            this.PageOwner = pageOwner;
            this.ListGroups = new List<GroupModel>(this._GroupDAO.Search());
            this._IsRefreshing = false;
        }
    }
}
