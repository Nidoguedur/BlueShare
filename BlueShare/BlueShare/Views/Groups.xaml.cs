using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BlueShare.DAO;
using BlueShare.ViewModels;
using BlueShare.Models;

namespace BlueShare.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Groups : ContentPage
	{
        public Groups()
        {
            InitializeComponent();

            this.BindingContext = new GroupsViewModel(this);
        }
    }
}