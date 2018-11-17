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
		public Configurations ()
		{
			InitializeComponent ();
		}

        public void OnViewCellTappedIsActiveBluetooth(object sender, EventArgs args)
        {
            SwitchIsActiveBluetooth.IsToggled = !SwitchIsActiveBluetooth.IsToggled;
        }

        public void OnViewCellTappedIsShare(object sender, EventArgs args)
        {
            SwitchIsShare.IsToggled = !SwitchIsShare.IsToggled;
        }

        public void OnViewCellTappedIsGroupRemember(object sender, EventArgs args)
        {
            SwitchIsGroupRemember.IsToggled = !SwitchIsGroupRemember.IsToggled;
        }
    }
}