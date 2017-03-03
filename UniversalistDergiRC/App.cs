using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UniversalistDergiRC.DataAccess;
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

        protected override void OnStart()
        {
            // Handle when your app starts
            CarouselPage detailCarousel = new CarouselPage();
            CarouselPage menuCarousel = new CarouselPage();

            MagazineListView magazineListView = new MagazineListView(navigationController);
            detailCarousel.Children.Add(magazineListView);

            MenuView menuView = new MenuView(navigationController);
            menuCarousel.Title = Constants.UNIVERSALIST_DERGI_TITLE;
            menuCarousel.Children.Add(menuView);

            MasterDetailPage masterDetail = new MasterDetailPage()
            {
                Master = menuCarousel,
                Detail = detailCarousel
            };
            masterDetail.Title = Constants.UNIVERSALIST_DERGI_TITLE;
            masterDetail.MasterBehavior = MasterBehavior.SplitOnPortrait;
            masterDetail.IsPresentedChanged += IsPresentedChanged;
            navigationController.InitializeController(masterDetail,detailCarousel, menuCarousel);
            MainPage = masterDetail;
        }

        internal void DroidOnBackPressed()
        {
            navigationController.OpenMagazineListPage();
        }

        private void IsPresentedChanged(object sender, EventArgs e)
        {
            navigationController.CloseBookmarkListPage();
        }

       

        protected override void OnSleep()
        {
            
        }

        protected override void OnResume()
        {
            navigationController.ResumeAsync();
        }
        
    }
}
