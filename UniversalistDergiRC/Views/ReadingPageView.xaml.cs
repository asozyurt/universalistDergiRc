using UniversalistDergiRC.Core;
using UniversalistDergiRC.ViewModels;
using Xamarin.Forms;

namespace UniversalistDergiRC.Views
{
    public partial class ReadingPageView : ContentPage
    {
        public ReadingPageView()
        {
            InitializeComponent();
            BindingContext = new ReadingPageViewModel();
        }
    }
}
