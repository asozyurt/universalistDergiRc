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
            CarouselPage detailCarousel = new CarouselPage();
            CarouselPage menuCarousel = new CarouselPage();

            MagazineListView magazineListView = new MagazineListView(navigationController);

            MenuView menuView = new MenuView(navigationController);
            menuCarousel.Title = Constants.UNIVERSALIST_DERGI_TITLE;
            menuCarousel.Children.Add(menuView);

            TabbedPage detailTabPage = new TabbedPage();

            detailTabPage.BarBackgroundColor = Color.FromHex("#80000000");
            detailTabPage.Children.Add(magazineListView);

            MasterDetailPage masterDetail = new MasterDetailPage()
            {
                Master = menuCarousel,
                Detail = detailTabPage
            };
            masterDetail.Title = Constants.UNIVERSALIST_DERGI_TITLE;
            masterDetail.MasterBehavior = MasterBehavior.SplitOnPortrait;
            masterDetail.IsPresentedChanged += IsPresentedChanged;
            navigationController.InitializeController(masterDetail, detailTabPage, menuCarousel);
            MainPage = masterDetail;
        }

        private void IsPresentedChanged(object sender, EventArgs e)
        {
            navigationController.CloseBookmarkListPage();
        }
    }
}
