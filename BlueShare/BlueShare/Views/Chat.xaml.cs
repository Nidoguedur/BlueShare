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
	public partial class Chat : ContentPage
	{
		public Chat(GroupModel groups)
		{
			InitializeComponent ();

            this.BindingContext = new ChatViewModel(this, groups);
		}
    }
}