using System;
using UniversalistDergiRC.Core;
using UniversalistDergiRC.Repositories;

namespace UniversalistDergiRC.Model
{
    public class MagazineSummaryModel : BaseModel
    {
        private int _issue;
        private string _title;
        private MagazinePageModel _coverPage;
        private string _period;
        private int _pageCount;
        private string _spotDescription;
        public string SpotDescription
        {
            get
            {
                return _spotDescription;
            }
            set
            {
                if (_spotDescription != value)
                {
                    _spotDescription = value;
                    OnPropertyChanged(() => SpotDescription);
                }
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(() => Title);
                }
            }
        }

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

        public MagazinePageModel CoverPage
        {
            get
            {
                return _coverPage;
            }
            set
            {
                if (_coverPage != value)
                {
                    _coverPage = value;
                    OnPropertyChanged(() => CoverPage);
                }
            }
        }

        public string Period
        {
            get
            {
                return _period;
            }
            set
            {
                if (_period != value)
                {
                    _period = value;
                    OnPropertyChanged(() => Period);
                }
            }
        }

        public int PageCount
        {
            get
            {
                return _pageCount;
            }
            set
            {
                if (_pageCount != value)
                {
                    _pageCount = value;
                    OnPropertyChanged(() => PageCount);
                }
            }
        }

        public static MagazineSummaryModel GenerateFromSerializedText(string serializedMagazineSummaryText)
        {
            if (string.IsNullOrEmpty(serializedMagazineSummaryText))
                return null;

            string[] magazineSummaryArray = serializedMagazineSummaryText.Split(new[] { Constants.ITEM_PROPERTY_SEPERATOR }, StringSplitOptions.RemoveEmptyEntries);

            if (magazineSummaryArray == null || magazineSummaryArray.Length < 6)
                return null;

            string coverPage = magazineSummaryArray[0];
            string issue = magazineSummaryArray[1];
            string pageCount = magazineSummaryArray[2];
            string period = magazineSummaryArray[3];
            string title = magazineSummaryArray[4];
            string spotDescription = magazineSummaryArray[5];

            MagazineSummaryModel result = new MagazineSummaryModel
            {
                CoverPage = new MagazinePageModel
                {
                    SourceURL = coverPage
                },
                Issue = int.Parse(issue),
                PageCount = int.Parse(pageCount),
                Period = period,
                Title = title,
                SpotDescription = spotDescription
            };

            return result;
        }
    }
}
