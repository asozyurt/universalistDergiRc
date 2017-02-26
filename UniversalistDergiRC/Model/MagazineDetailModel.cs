using System.Collections.ObjectModel;
using UniversalistDergiRC.Core;

namespace UniversalistDergiRC.Model
{
    public class MagazineDetailModel : BaseModel
    {
        private int _issue;
        public int Issue
        {
            get
            {
                return _issue;
            }
            set
            {
                if (_issue != value)
                {
                    _issue = value;
                    OnPropertyChanged(() => Issue);
                }
            }
        }

        private ObservableCollection<MagazinePageModel> _pages;

        public ObservableCollection<MagazinePageModel> Pages
        {
            get
            {
                _pages = _pages ?? new ObservableCollection<MagazinePageModel>();
                return _pages;
            }
            set {
                if (_pages != value)
                {
                    _pages = value;
                    OnPropertyChanged(() => Pages);
                }

            }
        }


    }
}
