using BlueShare.DAO;
using BlueShare.Models;
using BlueShare.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlueShare.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GroupAdd : ContentPage
	{
		public GroupAdd ()  
		{
            InitializeComponent();

            this.BindingContext = new GroupAddViewModel();
        }
    }
}