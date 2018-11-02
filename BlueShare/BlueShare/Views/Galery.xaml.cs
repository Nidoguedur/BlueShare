using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace BlueShare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Galery : ContentPage
    {
        public Galery()
        {
            InitializeComponent();

            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Auto }
                }
            };

            List<string> imagens = Directory.GetFiles("").ToList();
            //ler as imagens da pasta definida pela gente

            int lines = imagens.Count / 2;
            if (imagens.Count % 2 == 1) lines++;

            for (int line = 0; line < lines; line++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100) });

                grid.Children.Add(new Image { Source = imagens.First() }, line, 0);
                imagens.Remove(imagens.First());

                if (imagens.Count > 0)
                {
                    grid.Children.Add(new Image { Source = imagens.First() }, line, 1);
                    imagens.Remove(imagens.First());
                }
            }

            Content = grid;
        }
    }
}