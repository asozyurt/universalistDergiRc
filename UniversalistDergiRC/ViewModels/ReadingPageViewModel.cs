using System;
using System.Windows.Input;
using UniversalistDergiRC.Core;
using UniversalistDergiRC.DataAccess;
using UniversalistDergiRC.Model;
using UniversalistDergiRC.Repositories;
using Xamarin.Forms;

namespace UniversalistDergiRC.ViewModels
{
    public class ReadingPageViewModel : BaseModel
    {
        private int _activePageIndex;
        private UriImageSource _activePageUrl;
        private ICommand _addOrRemoveBookmarkCommand;
        private ICommand _goFirstPageCommand;
        private ICommand _goNextPageCommand;
        private ICommand _goPreviousPageCommand;
        private bool _isBookmarked;
        private bool _isNextPageVisible;
        private bool _isPreviousPageVisible;
        private string _readingPageTitle;
        private ICommand _returnMagazineListPageCommand;
        private MagazineDetailModel activeMagazine;
        private NavigationController navigationController;
        public ReadingPageViewModel(NavigationController navigationController)
        {
            this.navigationController = navigationController;
            MessagingCenter.Subscribe<ZoomImageBehavior>(this, Constants.RIGHT_SLIDE, handleRightSwipe);
            MessagingCenter.Subscribe<ZoomImageBehavior>(this, Constants.LEFT_SLIDE, handleLeftSwipe);
            ReadingPageTitle = Constants.READING_PAGE_INITIAL_TITLE;
        }

        public int ActivePageIndex
        {
            get
            {
                return _activePageIndex;
            }

            set
            {
                _activePageIndex = value;
                OnPropertyChanged(() => ActivePageIndex);
                OnPropertyChanged(() => ActivePageNumber);
            }
        }

        public int ActivePageNumber
        {
            get
            {
                return ActivePageIndex + 1;
            }
        }

        public UriImageSource ActivePageUrl
        {
            get
            {
                _activePageUrl = _activePageUrl ?? new UriImageSource
                {
                    Uri = new Uri(Constants.EMPTY_PAGE),
                    CachingEnabled = true,
                    CacheValidity = Constants.DEFAULT_CACHE_VALIDITY
                };

                return _activePageUrl;
            }
            set
            {
                _activePageUrl = value;
                OnPropertyChanged(() => ActivePageUrl);

            }
        }

        public ICommand AddOrRemoveBookmarkCommand
        {
            get
            {
                _addOrRemoveBookmarkCommand = _addOrRemoveBookmarkCommand ?? new Command(addOrRemoveBookmark);
                return _addOrRemoveBookmarkCommand;
            }
            set
            {
                if (_addOrRemoveBookmarkCommand != value)
                {
                    _addOrRemoveBookmarkCommand = value;
                    OnPropertyChanged(() => AddOrRemoveBookmarkCommand);
                }
            }
        }

        public FileImageSource BookmarkIcon
        {
            get
            {
                return IsBookmarked ? Constants.BOOKMARK_SAVED_ICON : Constants.BOOKMARK_NORMAL_ICON;
            }

        }

        public ICommand GoFirstPageCommand
        {
            get
            {
                _goFirstPageCommand = _goFirstPageCommand ?? new Command(() => openPage(Constants.FIRST_PAGE_INDEX));
                return _goFirstPageCommand;
            }
            set
            {
                if (_goFirstPageCommand != value)
                {
                    _goFirstPageCommand = value;
                    OnPropertyChanged(() => GoFirstPageCommand);
                }
            }
        }

        public ICommand GoNextPageCommand
        {
            get
            {
                _goNextPageCommand = _goNextPageCommand ?? new Command(goNextPage);
                return _goNextPageCommand;
            }
            set
            {
                if (_goNextPageCommand != value)
                {
                    _goNextPageCommand = value;
                    OnPropertyChanged(() => GoNextPageCommand);
                }
            }
        }

        public ICommand GoPreviousPageCommand
        {
            get
            {
                _goPreviousPageCommand = _goPreviousPageCommand ?? new Command(goPreviousPage);
                return _goPreviousPageCommand;
            }
            set
            {
                if (_goPreviousPageCommand != value)
                {
                    _goPreviousPageCommand = value;
                    OnPropertyChanged(() => GoPreviousPageCommand);
                }
            }
        }


        public bool IsBookmarked
        {
            get
            {
                return _isBookmarked;
            }

            set
            {
                _isBookmarked = value;
                OnPropertyChanged(() => IsBookmarked);
                OnPropertyChanged(() => BookmarkIcon);

            }
        }

        public bool IsNextPageVisible
        {
            get
            {
                return _isNextPageVisible;
            }

            set
            {
                _isNextPageVisible = value;
                OnPropertyChanged(() => IsNextPageVisible);
            }
        }

        public bool IsPreviousPageVisible
        {
            get
            {
                return _isPreviousPageVisible;
            }

            set
            {
                _isPreviousPageVisible = value;
                OnPropertyChanged(() => IsPreviousPageVisible);
            }
        }

        public string ReadingPageTitle
        {
            get { return _readingPageTitle; }
            set
            {
                if (_readingPageTitle != value)
                {
                    _readingPageTitle = value;
                    OnPropertyChanged(() => ReadingPageTitle);
                }
            }
        }

        public ICommand ReturnMagazineListPageCommand
        {
            get
            {
                _returnMagazineListPageCommand = _returnMagazineListPageCommand ?? new Command(() => navigationController.OpenMagazineListPage());
                return _returnMagazineListPageCommand;
            }
            set
            {
                if (_returnMagazineListPageCommand != value)
                {
                    _returnMagazineListPageCommand = value;
                    OnPropertyChanged(() => ReturnMagazineListPageCommand);
                }
            }
        }
        // Default değeri özellikle sıfır yapmadım, bu metodun sayfa numarasıyla çalışmasını istiyorum.
        internal void OpenMagazine(int issueNumber, int pageNumber = 1)
        {
            activeMagazine = DataAccessManager.GetMagazineIssueDetail(issueNumber);
            ReadingPageTitle = "Universalist " + activeMagazine.Issue + ". Sayı";
            openPage(pageNumber - 1);
        }

        private void addOrRemoveBookmark(object obj)
        {
            bool processSuccessful = false;
            if (IsBookmarked)
            {
                processSuccessful = ClientDataManager.DeleteSingleBookMark(activeMagazine.Issue, ActivePageNumber);
            }
            else
            {
                processSuccessful = ClientDataManager.SaveSingleBookMark(activeMagazine.Issue, ActivePageNumber);
            }

            if (processSuccessful)
            {
                IsBookmarked = !IsBookmarked;
                activeMagazine.Pages[ActivePageIndex].IsBookMarked = IsBookmarked;
                MessagingCenter.Send(this, Constants.ANIMATE_IMAGE_MESSAGEKEY);
            }
        }

        private void goNextPage(object obj)
        {
            openPage(ActivePageIndex + 1);
        }

        private void goPreviousPage(object obj)
        {
            openPage(ActivePageIndex - 1);
        }

        private void handleLeftSwipe(ZoomImageBehavior obj)
        {
            goPreviousPage(null);
        }

        private void handleRightSwipe(ZoomImageBehavior obj)
        {
            goNextPage(null);
        }

        private void openPage(int pageIndex)
        {

            if (pageIndex < 0 || activeMagazine == null || pageIndex >= activeMagazine.Pages.Count)
            {
                return;
            }

            ActivePageIndex = pageIndex;
            IsBookmarked = activeMagazine.Pages[pageIndex].IsBookMarked;
            ActivePageUrl = new UriImageSource
            {
                Uri = new Uri(activeMagazine.Pages[pageIndex].SourceURL),
                CachingEnabled = true,
                CacheValidity = Constants.DEFAULT_CACHE_VALIDITY
            };

            IsPreviousPageVisible = ActivePageIndex > 0;
            IsNextPageVisible = ActivePageIndex < activeMagazine.Pages.Count - 1;

            MessagingCenter.Send(this, Constants.RESET_IMAGE_POSITION_MESSAGEKEY);
        }
    }
}
