using System;
using UniversalistDergiRC.Repositories;
using UniversalistDergiRC.Views;
using Xamarin.Forms;

namespace UniversalistDergiRC
{
    public class App : Application
    {
        NavigationController navigationController;

        public App()
        {
            navigationController = new NavigationController();
        }

        // Only available in Android
        internal bool DroidOnBackPressed()
        {
            // If active page is magazine list, then allow program to execute base.BackButtonPressed
            if (navigationController.IsMagazineListActive())
                return true;

            navigationController.OpenMagazineListPage();
            return false;
        }

        protected override void OnStart()
        {
            // Handle when your app starts

            MagazineListView magazineListView = new MagazineListView(navigationController);
            NavigationPage magazineList = new NavigationPage(magazineListView) { BackgroundColor = Color.FromHex("#80000000") };

            CarouselPage menuCarousel = new CarouselPage();
            MenuView menuView = new MenuView(navigationController);
            menuCarousel.Title = Constants.UNIVERSALIST_DERGI_TITLE;
            menuCarousel.Children.Add(menuView);

            MasterDetailPage masterDetail = new MasterDetailPage()
            {
                Master = menuCarousel,
                Detail = magazineList,
                Title = Constants.UNIVERSALIST_DERGI_TITLE
            };

            masterDetail.IsPresentedChanged += IsPresentedChanged;
            navigationController.InitializeController(masterDetail, magazineList, menuCarousel);

            MainPage = masterDetail;
        }

        private void IsPresentedChanged(object sender, EventArgs e)
        {
            navigationController.CloseBookmarkListPage();
        }
    }
}
