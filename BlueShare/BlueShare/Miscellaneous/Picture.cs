using System;
using System.Collections.Generic;
using System.Text;

namespace BlueShare.Miscellaneous
{
    public class Picture
    {
        private static string _PicturesPath = string.Empty;
        public static string PicturesPath
        {
            get
            {
                if (string.IsNullOrEmpty(_PicturesPath))
                {
                    _PicturesPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).AbsolutePath;
                }

                return _PicturesPath;
            }
        }

        public static string NextName { get { return Next(); } }

        private static string Next()
        {
            string prefix = "BlueShare";

            return $"{prefix}_{DateTime.Now.Ticks}";
        }
    }
}
