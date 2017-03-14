using System.Collections.ObjectModel;
using System.Windows.Input;
using UniversalistDergiRC.Core;
using UniversalistDergiRC.DataAccess;
using UniversalistDergiRC.Model;
using UniversalistDergiRC.Repositories;
using Xamarin.Forms;

namespace UniversalistDergiRC.ViewModels
{
    public class MagazineListViewModel : BaseModel
    {
        private bool _isOpenReadingPageVisible;
        private bool _isRefreshing;
        private ObservableCollection<MagazineSummaryModel> _magazineIssueList;
        private NavigationController _navigationController;
        private string _navigationTitle;
        private ICommand _openMasterCommand;
        private ICommand _openReadingPageCommand;
        private ICommand _refreshCommand;
        private MagazineSummaryModel selectedMagazine;

        public MagazineListViewModel(NavigationController controller)
        {
            _navigationController = controller;
            refreshMagazineIssueList(true);
            NavigationTitle = Constants.UNIVERSALIST_DERGI_TITLE_UPPERCASE;
        }

        public bool IsOpenReadingPageVisible
        {
            get { return _isOpenReadingPageVisible; }
            set
            {
                if (_isOpenReadingPageVisible != value)
                {
                    _isOpenReadingPageVisible = value;
                    OnPropertyChanged(() => IsOpenReadingPageVisible);
                }
            }
        }

        public bool IsRefreshing
        {
            get
            {
                return _isRefreshing;
            }

            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MagazineSummaryModel> MagazineIssueList
        {
            get
            {
                _magazineIssueList = _magazineIssueList ?? new ObservableCollection<MagazineSummaryModel>();
                return _magazineIssueList;
            }
            set
            {
                if (_magazineIssueList != value)
                {
                    _magazineIssueList = value;
                    OnPropertyChanged(() => MagazineIssueList);
                }
            }
        }

        public string NavigationTitle
        {
            get { return _navigationTitle; }
            set
            {
                if (_navigationTitle != value)
                {
                    _navigationTitle = value;
                    OnPropertyChanged(() => NavigationTitle);
                }
            }
        }

        public ICommand OpenMasterCommand
        {
            get
            {
                _openMasterCommand = _openMasterCommand ?? new Command(() => _navigationController.OpenMasterPage());
                return _openMasterCommand;
            }
            set
            {
                if (_openMasterCommand != value)
                {
                    _openMasterCommand = value;
                    OnPropertyChanged(() => OpenMasterCommand);
                }
            }
        }

        public ICommand OpenReadingPageCommand
        {
            get
            {
                _openReadingPageCommand = _openReadingPageCommand ?? new Command(openReadingPage);
                return _openReadingPageCommand;
            }
            set
            {
                if (_openReadingPageCommand != value)
                {
                    _openReadingPageCommand = value;
                    OnPropertyChanged(() => OpenReadingPageCommand);
                }
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                _refreshCommand = _refreshCommand ?? new Command(refreshMagazineIssueListCommand);
                return _refreshCommand;
            }
            set
            {
                if (_refreshCommand != value)
                {
                    _refreshCommand = value;
                    OnPropertyChanged(() => RefreshCommand);
                }
            }
        }

        public MagazineSummaryModel SelectedMagazine
        {
            get
            {
                return selectedMagazine;
            }
            set
            {
                if (selectedMagazine != value)
                {
                    selectedMagazine = value;
                    openSelectedMagazine(selectedMagazine);
                }
                OnPropertyChanged(() => SelectedMagazine);
            }
        }

        public void openSelectedMagazine(object obj)
        {
            MagazineSummaryModel selection = obj as MagazineSummaryModel;
            if (selection == null)
                return;

            _navigationController.OpenReadingPage(selection.Issue, Constants.FIRST_PAGE_NUMBER);
            IsOpenReadingPageVisible = true;
            SelectedMagazine = null;
        }

        private void openReadingPage(object obj)
        {
            _navigationController.OpenReadingPageForContinue();
        }

        private void refreshMagazineIssueList(bool tryLocal)
        {
            IsRefreshing = true;
            try
            {
                MagazineIssueList = DataAccessManager.GetMagazineIssues(tryLocal);
            }
            catch
            {
                MessagingCenter.Send(this, Constants.CONNECTION_ERROR_MESSAGEKEY);
            }
            IsRefreshing = false;
        }

        private void refreshMagazineIssueListCommand(object obj)
        {
            refreshMagazineIssueList(false);
        }
    }
}
