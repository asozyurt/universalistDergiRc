using UniversalistDergiRC.Core;
using UniversalistDergiRC.ViewModels;
using Xamarin.Forms;

namespace UniversalistDergiRC.Views
{
    public partial class ReadingPageView2 : ContentPage
    {
        public ReadingPageView2(NavigationController navigationController)
        {
            InitializeComponent();
            BindingContext = new ReadingPageViewModel2(navigationController);
        }
    }
}
