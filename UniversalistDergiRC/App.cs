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
        NavigationController nvgController;
        public App()
        {
            nvgController = new NavigationController();
        }

        protected override void OnStart()
        {
            

            // Handle when your app starts
            CarouselPage detailCarousel = new CarouselPage();
            CarouselPage menuCarousel = new CarouselPage();

            MagazineListView magazineListView = new MagazineListView(nvgController);
            detailCarousel.Children.Add(magazineListView);

            MenuView menuView = new MenuView(nvgController);
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
            nvgController.SetPages(masterDetail,detailCarousel, menuCarousel);
            MainPage = masterDetail;

        }

        private void IsPresentedChanged(object sender, EventArgs e)
        {
            nvgController.CloseBookmarkListPage();
        }

       

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
