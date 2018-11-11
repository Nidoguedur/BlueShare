using BlueShare.Models;
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
	public partial class Configurations : ContentPage
	{
		public Configurations()
		{
			InitializeComponent();
		}

        public void OnViewCellTappedIsActiveBluetooth(object sender, EventArgs args)
        {
           var cell = (ViewCell)sender;

           var switchCell = (Switch)cell.View.FindByName("SwitchIsActiveBluetooth");

           switchCell.IsToggled = !switchCell.IsToggled;
        }

        public void OnViewCellTappedIsShare(object sender, EventArgs args)
        {
            var cell = (ViewCell)sender;

            var switchCell = (Switch)cell.View.FindByName("SwitchIsShare");

            switchCell.IsToggled = !switchCell.IsToggled;
        }

        public void OnViewCellTappedIsGroupRemember(object sender, EventArgs args)
        {
            var cell = (ViewCell)sender;

            var switchCell = (Switch)cell.View.FindByName("SwitchIsGroupRemember");

            switchCell.IsToggled = !switchCell.IsToggled;
        }
    }
}