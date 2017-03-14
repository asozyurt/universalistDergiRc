using UniversalistDergiRC.ViewModels;
using Xamarin.Forms;

namespace UniversalistDergiRC.Views
{
    public partial class BookmarkListView : ContentPage
	{
		public BookmarkListView (NavigationController controller)
		{
			InitializeComponent ();
            BindingContext = new BookmarkListViewModel(controller);
        }
    }
}
