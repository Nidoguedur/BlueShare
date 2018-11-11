using Android.Content;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BlueShare
{
    public partial class App : Application
    {
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }

        public App()
        {
            InitializeComponent();

            var vMainPage = new MainPage() { Title = "BlueShare" };

            var nav = new NavigationPage(vMainPage);
            nav.ToolbarItems.Add(new ToolbarItem { Order = ToolbarItemOrder.Primary } );
            nav.ToolbarItems.Add(new ToolbarItem { Text = "Configurações", Order = ToolbarItemOrder.Secondary, Command = new Command(vMainPage.ConfigurationToolBarItemClickedAsync) });
            nav.ToolbarItems.Add(new ToolbarItem { Text = "Sobre", Order = ToolbarItemOrder.Secondary });

            App.Current.MainPage = nav;
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
