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
                    if (!string.IsNullOrEmpty(_sourceURL))
                    {
                        ImageSource = new UriImageSource
                        {
                            Uri = new Uri(_sourceURL),
                            CachingEnabled = true,
                            CacheValidity = Constants.DEFAULT_CACHE_VALIDITY
                        };
                    }

                    _sourceURL = value;
                    OnPropertyChanged(() => SourceURL);
                }
            }
        }

        private UriImageSource _imageSource;
        public UriImageSource ImageSource
        {

            get
            {
                _imageSource = _imageSource ?? new UriImageSource
                {
                    Uri = new Uri(!string.IsNullOrEmpty(_sourceURL) ? _sourceURL: Constants.EMPTY_PAGE),
                    CachingEnabled = true,
                    CacheValidity = Constants.DEFAULT_CACHE_VALIDITY
                };

                return _imageSource;
            }
            set
            {
                _imageSource = value;
                OnPropertyChanged(() => ImageSource);
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
