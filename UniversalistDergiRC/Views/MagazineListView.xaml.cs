using System;
using UniversalistDergiRC.Repositories;
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
            MessagingCenter.Subscribe<MagazineListViewModel>(this, Constants.CONNECTION_ERROR_MESSAGEKEY, showMessage);
        }

        private void showMessage(MagazineListViewModel obj)
        {
            DisplayAlert(Constants.CONNECTION_ERROR_TITLE, Constants.CONNECTION_ERROR_MESSAGE,Constants.OK);
        }
    }
}
