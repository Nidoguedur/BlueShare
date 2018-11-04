using BlueShare.Miscellaneous;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlueShare.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GridImage : Grid
	{
        private int RowHeight = 200;

        public GridImage()
        {
            InitializeComponent();
            CreateGrid();
        }

        private void CreateGrid()
        {
            var photos = Directory.GetFiles(Picture.PicturesPath).ToList();

            if (photos != null)
            {
                int lines = photos.Count / 2;
                if (photos.Count % 2 == 1) lines++;

                for (int line = 0; line < lines; line++)
                {
                    RowDefinitions.Add(new RowDefinition { Height = new GridLength(RowHeight) });

                    Children.Add(CreateImage(photos.First()), 0, line);
                    photos.Remove(photos.First());

                    if (photos.Count > 0)
                    {
                        Children.Add(CreateImage(photos.First()), 1, line);
                        photos.Remove(photos.First());
                    }
                }
            }
        }

        private Image CreateImage(string source)
        {
            return new Image
            {
                Source = source,
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
        }
    }
}