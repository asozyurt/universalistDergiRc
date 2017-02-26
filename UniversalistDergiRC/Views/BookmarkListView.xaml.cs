using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
