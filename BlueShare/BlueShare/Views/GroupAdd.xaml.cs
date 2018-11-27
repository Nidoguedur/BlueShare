using BlueShare.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlueShare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupAdd : ContentPage
    {
        public GroupAdd()
        {
            InitializeComponent();

            this.BindingContext = new GroupAddViewModel(this);
        }
    }
}