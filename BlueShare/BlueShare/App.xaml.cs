using Android.Content;
using BlueShare.Miscellaneous;
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

            App.Current.MainPage = new MainPage();
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
