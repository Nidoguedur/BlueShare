using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BlueShare.Interface;
using System.IO;
using BlueShare.Droid.DataBase;
using Xamarin.Forms;

[assembly:Dependency(typeof(DataBaseDroid))]
namespace BlueShare.Droid.DataBase
{
    public class DataBaseDroid : IDataPath
    {
        public string GetPath(string datasetName)
        {           
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), datasetName);          
        }
    }
}