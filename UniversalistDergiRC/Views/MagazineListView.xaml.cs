using UniversalistDergiRC.ViewModels;
using Xamarin.Forms;

namespace UniversalistDergiRC.Views
{
    public partial class MagazineListView : ContentPage
	{
        public MagazineListView(NavigationController controller)
        {
            InitializeComponent();
            BindingContext = new MagazineListViewModel(controller);
        }
    }
}
