using System;
using UniversalistDergiRC.Core;
using UniversalistDergiRC.Repositories;

namespace UniversalistDergiRC.Model
{
    public class BookmarkModel : BaseModel
    {
        private string _description;
        private int _issueNumber;
        private int _pageNumber;

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }

        public int IssueNumber
        {
            get { return _issueNumber; }
            set
            {
                if (_issueNumber != value)
                {
                    _issueNumber = value;
                    OnPropertyChanged(() => IssueNumber);
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

        public static BookmarkModel GenerateFromSerializedText(string serializedBookmarkText)
        {
            if (string.IsNullOrEmpty(serializedBookmarkText))
                return null;

            string[] bookmarkPropertiesArray = serializedBookmarkText.Split(new[] { Constants.ITEM_PROPERTY_SEPERATOR }, StringSplitOptions.RemoveEmptyEntries);

            if (bookmarkPropertiesArray == null || bookmarkPropertiesArray.Length < 2)
                return null;

            string issue = bookmarkPropertiesArray[0];
            string page = bookmarkPropertiesArray[1];

            BookmarkModel result = new BookmarkModel
            {
                IssueNumber = int.Parse(issue),
                PageNumber = int.Parse(page),
                Description = generateDescription(issue, page)
            };
            
            return result;
        }

        private static string generateDescription(string issue, string page)
        {
            return string.Format(Constants.GENERIC_BOOKMARK_DESCRIPTION, issue, page);
        }

        private static string generateUrl(string issue, string page)
        {
            return string.Format(Constants.GENERIC_MAGAZINE_PAGES_URL, issue, page);
        }
    }
}
