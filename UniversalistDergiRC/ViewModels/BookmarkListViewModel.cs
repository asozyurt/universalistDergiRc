using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UniversalistDergiRC.Core;
using UniversalistDergiRC.DataAccess;
using UniversalistDergiRC.Model;
using UniversalistDergiRC.Repositories;
using Xamarin.Forms;

namespace UniversalistDergiRC.ViewModels
{
    public class BookmarkListViewModel : BaseModel
    {
        private NavigationController _navigationController;

        public BookmarkListViewModel(NavigationController controller)
        {
            this._navigationController = controller;
        }

        private ObservableCollection<BookmarkModel> _bookmarkList;

        public ObservableCollection<BookmarkModel> BookmarkList
        {
            get
            {
                _bookmarkList = _bookmarkList ?? new ObservableCollection<BookmarkModel>();
                return _bookmarkList;
            }
            set
            {
                if (_bookmarkList != value)
                {
                    _bookmarkList = value;
                    OnPropertyChanged(() => BookmarkList);
                }
            }
        }
        internal void ListAllBookMarks()
        {
            var allBookmarks = ClientDataManager.GetAllBookmarks();
            BookmarkList = new ObservableCollection<BookmarkModel>(allBookmarks);
        }

        private ICommand _openBookmarkedPageCommand;
        public ICommand OpenBookmarkedPageCommand
        {
            get
            {
                _openBookmarkedPageCommand = _openBookmarkedPageCommand ?? new Command(openBookmarkedPage);
                return _openBookmarkedPageCommand;
            }
            set
            {
                if (_openBookmarkedPageCommand != value)
                {
                    _openBookmarkedPageCommand = value;
                    OnPropertyChanged(() => OpenBookmarkedPageCommand);
                }
            }
        }

        private BookmarkModel selectedBookmark;
        public BookmarkModel SelectedBookmark {
            get
            {
                return selectedBookmark;
            }
            set
            {
                if (selectedBookmark != value)
                {
                    selectedBookmark = value;
                    openBookmarkedPage(selectedBookmark);
                    OnPropertyChanged(() => SelectedBookmark);
                }
            }
        }

        private void openBookmarkedPage(object obj)
        {
            BookmarkModel selectedBookmark = obj as BookmarkModel;
            if (selectedBookmark == null)
                return;

            _navigationController.OpenReadingPage(selectedBookmark.IssueNumber,selectedBookmark.PageNumber);
        }

    }

}
