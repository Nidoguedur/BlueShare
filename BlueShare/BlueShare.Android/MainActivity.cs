using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.CurrentActivity;
using Android.Content;
using Android.Bluetooth;
using BlueShare.Miscellaneous;
using Android.Support.V4.Content;
using Android;

namespace BlueShare.Droid
{
    [Activity(Label = "BlueShare", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            App.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);
            App.ScreenHeight = (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);

            //CrossCurrentActivity.Current.Init(this, savedInstanceState);

            //FileSystemWatcher watcher = new FileSystemWatcher
            //{
            //    Path = App.PicturesPath
            //};
            //watcher.Created += Watcher_Created;
        }

        protected override void OnStart()
        {
            base.OnStart();

            var adapter = DroidBluetooth.Adapter;

            // If the adapter is null, then Bluetooth is not supported
            if (adapter == null)
            {
                Toast.MakeText(this, "Bluetooth não disponível", ToastLength.Long).Show();
                Finish();
                return;
            }

            StartActivityForResult(new Intent(BluetoothAdapter.ActionRequestEnable), 2);

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) == Permission.Denied)
            {
                Toast.MakeText(this, "Armazenamento externo não disponível", ToastLength.Long).Show();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}