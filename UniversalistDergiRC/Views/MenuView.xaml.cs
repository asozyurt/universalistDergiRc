using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalistDergiRC.Repositories;
using UniversalistDergiRC.ViewModels;
using Xamarin.Forms;

namespace UniversalistDergiRC.Views
{
	public partial class MenuView : ContentPage
	{
        public MenuView(NavigationController nvgController)
        {
			InitializeComponent ();
            BindingContext = new MenuViewModel(nvgController);
            
        }
    }
}
