using System;
using System.Windows.Input;
using UniversalistDergiRC.Core;
using UniversalistDergiRC.Repositories;
using Xamarin.Forms;

namespace UniversalistDergiRC.ViewModels
{
    public class MenuViewModel : BaseModel
    {
        private ICommand _openDevelopersSiteCommand;
        private ICommand _openOfficialSiteCommand;
        private ICommand _openMagazineListCommand;
        private ICommand _openBookmarkListCommand;
        private string _title;
        private NavigationController navigationController;


        public MenuViewModel(NavigationController controller)
        {
            this.navigationController = controller;
            Title = Constants.UNIVERSALIST_DERGI_TITLE;
        }
        public ICommand OpenDevelopersSiteCommand
        {
            get
            {
                _openDevelopersSiteCommand = _openDevelopersSiteCommand ?? new Command(openDevelopersSite);
                return _openDevelopersSiteCommand;
            }
            set
            {
                if (_openDevelopersSiteCommand != value)
                {
                    _openDevelopersSiteCommand = value;
                    OnPropertyChanged(() => OpenDevelopersSiteCommand);
                }
            }
        }

        public ICommand OpenOfficialSiteCommand
        {
            get
            {
                _openOfficialSiteCommand = _openOfficialSiteCommand ?? new Command(openOfficialSite);
                return _openOfficialSiteCommand;
            }
            set
            {
                if (_openOfficialSiteCommand != value)
                {
                    _openOfficialSiteCommand = value;
                    OnPropertyChanged(() => OpenOfficialSiteCommand);
                }
            }
        }

        public ICommand OpenMagazineListCommand
        {
            get
            {
                _openMagazineListCommand = _openMagazineListCommand ?? new Command(openMagazineList);
                return _openMagazineListCommand;
            }
            set
            {
                if (_openMagazineListCommand != value)
                {
                    _openMagazineListCommand = value;
                    OnPropertyChanged(() => OpenMagazineListCommand);
                }
            }
        }

        public ICommand OpenBookmarkListCommand
        {
            get
            {
                _openBookmarkListCommand = _openBookmarkListCommand ?? new Command(openBookmarkList);
                return _openBookmarkListCommand;
            }
            set
            {
                if (_openBookmarkListCommand != value)
                {
                    _openBookmarkListCommand = value;
                    OnPropertyChanged(() => OpenBookmarkListCommand);
                }
            }
        }

        private void openBookmarkList(object obj)
        {
            navigationController.OpenBookmarkListPage();
        }

        private void openMagazineList(object obj)
        {
            navigationController.OpenMagazineListPage();
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
                OnPropertyChanged(() => Title);

            }
        }

        private void openDevelopersSite(object obj)
        {
            Device.OpenUri(new Uri("http://asozyurt.com"));
        }
        private void openOfficialSite(object obj)
        {
            Device.OpenUri(new Uri("http://universalist.org"));
        }
    }
}
