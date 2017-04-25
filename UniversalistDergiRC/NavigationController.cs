using UniversalistDergiRC.ViewModels;
using UniversalistDergiRC.Views;
using Xamarin.Forms;

namespace UniversalistDergiRC
{
    public class NavigationController
    {
        private NavigationPage magazineListPage;
        private MasterDetailPage mainPage;
        private CarouselPage menuCarouselPage;
        private ContentPage readingPage;

        public void InitializeController(MasterDetailPage mainMasterDetail, NavigationPage magazineList, CarouselPage menuCarousel)
        {
            menuCarouselPage = menuCarousel;
            magazineListPage = magazineList;
            mainPage = mainMasterDetail;
        }

        public void OpenReadingPage(int issueNumber, int pageNumber)
        {
            if (readingPage == null)
            {
                readingPage = new ReadingPageView(this);
            }

            mainPage.IsPresented = false;

            ReadingPageViewModel vmReadingPage = readingPage.BindingContext as ReadingPageViewModel;
            if (vmReadingPage == null) return;

            vmReadingPage.OpenMagazine(issueNumber, pageNumber);

            if ((magazineListPage.CurrentPage as ReadingPageView) == null)
                magazineListPage.Navigation.PushAsync(readingPage);
        }

        internal void CloseBookmarkListPage()
        {
            if (menuCarouselPage == null || menuCarouselPage.Children.Count == 0)
                return;
            menuCarouselPage.CurrentPage = menuCarouselPage.Children[0];
        }

        internal bool IsMagazineListActive()
        {
            return (magazineListPage.CurrentPage as MagazineListView) != null;
        }

        internal void OpenBookmarkListPage()
        {
            if (menuCarouselPage == null)
                return;

            if (menuCarouselPage.Children.Count == 1)
                menuCarouselPage.Children.Add(new BookmarkListView(this));

            BookmarkListView bookmarkListPage = menuCarouselPage.Children[1] as BookmarkListView;

            if (bookmarkListPage == null) return;

            BookmarkListViewModel vmBookmarkList = bookmarkListPage.BindingContext as BookmarkListViewModel;
            if (vmBookmarkList == null) return;

            menuCarouselPage.IsBusy = true;
            vmBookmarkList.ListAllBookMarks();
            menuCarouselPage.IsBusy = false;

            menuCarouselPage.CurrentPage = menuCarouselPage.Children[1];
        }

        internal void OpenMagazineListPage()
        {
            mainPage.IsPresented = false;
            if (!IsMagazineListActive())
            {
                magazineListPage.PopToRootAsync();
            }
        }

        internal void OpenMasterPage()
        {
            mainPage.IsPresented = true;
        }

        internal void OpenReadingPageForContinue()
        {
            mainPage.IsPresented = false;
            if (IsMagazineListActive())
            {
                magazineListPage.PushAsync(readingPage);
            }
        }
    }
}
