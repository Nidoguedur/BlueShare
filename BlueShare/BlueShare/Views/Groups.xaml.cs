using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BlueShare.DAO;

namespace BlueShare.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Groups : ContentPage
	{
        public Groups()
        {
            InitializeComponent();

            InitListGroups();
        }
       
        public void InitListGroups()
        {
            var groups = new GroupDAO();

            ListViewGroups.ItemsSource = groups.Search();

            AddGroupButton.Clicked += AddGroupButton_Clicked;
        }

        public void AddGroupButton_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new GroupAdd() { Title = "Novo grupo" });
        }
    }
}