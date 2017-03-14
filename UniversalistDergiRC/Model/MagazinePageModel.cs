using System;
using UniversalistDergiRC.Core;
using UniversalistDergiRC.Repositories;
using Xamarin.Forms;

namespace UniversalistDergiRC.Model
{
    public class MagazinePageModel : BaseModel
    {
        private string _sourceURL;
        private bool _isBookMarked;
        private int _pageNumber;

        public string SourceURL
        {
            get { return _sourceURL; }
            set
            {
                if (_sourceURL != value)
                {
                    _sourceURL = value;
                    OnPropertyChanged(() => SourceURL);
                }
            }
        }

        public UriImageSource PageSource
        {
            get
            {
                return new UriImageSource
                {
                    Uri = string.IsNullOrEmpty(_sourceURL) ? new Uri(Constants.EMPTY_PAGE) : new Uri(_sourceURL),
                    CachingEnabled = true,
                    CacheValidity = Constants.DEFAULT_CACHE_VALIDITY
                };
            }

        }

        public bool IsBookMarked
        {
            get { return _isBookMarked; }
            set
            {
                if (_isBookMarked != value)
                {
                    _isBookMarked = value;
                    OnPropertyChanged(() => IsBookMarked);
                }
            }
        }

        public int PageNumber
        {
            get { return _pageNumber; }
            set
            {
                if (_pageNumber != value)
                {
                    _pageNumber = value;
                    OnPropertyChanged(() => PageNumber);
                }
            }
        }
    }
}
