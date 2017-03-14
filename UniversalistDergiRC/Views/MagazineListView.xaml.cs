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
            var b = new MagazineListViewModel(controller);
            BindingContext = b;
            MessagingCenter.Subscribe<MagazineListViewModel>(this, Constants.CONNECTION_ERROR_MESSAGEKEY, showMessage);

            //lstMagazines.ItemSelected += (sender, e) => {
            //    if (e.SelectedItem == null) return; // don't do anything if we just de-selected the row
            //                                        // do something with e.SelectedItem
            //    b.openSelectedMagazine(e.SelectedItem);
            //    ((ListView)sender).SelectedItem = null; // de-select the row
            //};

        }

        private void showMessage(MagazineListViewModel obj)
        {
            DisplayAlert(Constants.CONNECTION_ERROR_TITLE, Constants.CONNECTION_ERROR_MESSAGE,Constants.OK);
        }
    }
}
