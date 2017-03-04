//using System;
//using System.Collections.Generic;
//using System.Windows.Input;
//using UniversalistDergiRC.Core;
//using UniversalistDergiRC.DataAccess;
//using UniversalistDergiRC.Model;
//using UniversalistDergiRC.Repositories;
//using Xamarin.Forms;

//namespace UniversalistDergiRC.ViewModels
//{
//    public class ReadingPageViewModel2 : BaseModel
//    {
//        private int _activePageIndex;
//        private ICommand _addOrRemoveBookmarkCommand;
//        private bool _isBookmarked;

//        private MagazineDetailModel _activeMagazine;
//        public MagazineDetailModel ActiveMagazine
//        {
//            get { return _activeMagazine; }
//            set
//            {
//                _activeMagazine = value;
//                OnPropertyChanged(() => ActiveMagazine);
//            }
//        }
//        private NavigationController navigationController;

//        public ReadingPageViewModel2(NavigationController navigationController)
//        {
//            this.navigationController = navigationController;
//        }

//        public int ActivePageIndex
//        {
//            get
//            {
//                return _activePageIndex;
//            }

//            set
//            {
//                _activePageIndex = value;
//                OnPropertyChanged(() => ActivePageIndex);
//                OnPropertyChanged(() => ActivePageNumber);
//            }
//        }

//        public int ActivePageNumber
//        {
//            get
//            {
//                return ActivePageIndex + 1;
//            }
//        }


//        public ICommand AddOrRemoveBookmarkCommand
//        {
//            get
//            {
//                _addOrRemoveBookmarkCommand = _addOrRemoveBookmarkCommand ?? new Command(addOrRemoveBookmark);
//                return _addOrRemoveBookmarkCommand;
//            }
//            set
//            {
//                if (_addOrRemoveBookmarkCommand != value)
//                {
//                    _addOrRemoveBookmarkCommand = value;
//                    OnPropertyChanged(() => AddOrRemoveBookmarkCommand);
//                }
//            }
//        }

//        public FileImageSource BookmarkIcon
//        {
//            get
//            {
//                return IsBookmarked ? Constants.BOOKMARK_SAVED_ICON : Constants.BOOKMARK_NORMAL_ICON;
//            }

//        }
        
//        public bool IsBookmarked
//        {
//            get
//            {
//                return _isBookmarked;
//            }

//            set
//            {
//                _isBookmarked = value;
//                OnPropertyChanged(() => IsBookmarked);
//                OnPropertyChanged(() => BookmarkIcon);

//            }
//        }

//        // Default değeri özellikle sıfır yapmadım, bu metodun sayfa numarasıyla çalışmasını istiyorum.
//        internal void OpenMagazine(int issueNumber, int pageNumber = 1)
//        {
//            ActiveMagazine = DataAccessManager.GetMagazineIssueDetail(issueNumber);

//            openPage(pageNumber - 1);
//        }

//        private void addOrRemoveBookmark(object obj)
//        {
//            bool processSuccessful = false;
//            if (IsBookmarked)
//            {
//                processSuccessful = ClientDataManager.DeleteSingleBookMark(ActiveMagazine.Issue, ActivePageNumber);
//            }
//            else
//            {
//                processSuccessful = ClientDataManager.SaveSingleBookMark(ActiveMagazine.Issue, ActivePageNumber);
//            }

//            if (processSuccessful)
//            {
//                IsBookmarked = !IsBookmarked;
//                ActiveMagazine.Pages[ActivePageIndex].IsBookMarked = IsBookmarked;
//            }
//        }

//        private void goNextPage(object obj)
//        {
//            openPage(ActivePageIndex + 1);
//        }

//        private void goPreviousPage(object obj)
//        {
//            if (ActivePageIndex == 0)
//            {
//                navigationController.OpenMagazineListPage();
//            }
//            else
//            {
//                openPage(ActivePageIndex - 1);
//            }
//        }

//        private void openPage(int pageIndex)
//        {
//            if (pageIndex < 0 || ActiveMagazine == null || pageIndex >= ActiveMagazine.Pages.Count)
//            {
//                navigationController.SetCurrentPageForResume(0, 0);
//                return;
//            }
//            ActivePageIndex = pageIndex;
//            IsBookmarked = ActiveMagazine.Pages[pageIndex].IsBookMarked;
//            ActivePageUrl.Uri = new Uri(ActiveMagazine.Pages[pageIndex].SourceURL);

//            navigationController.SetCurrentPageForResume(ActiveMagazine.Issue, ActivePageNumber);
//        }
//    }

//}
