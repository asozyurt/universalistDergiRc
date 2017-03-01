using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace UniversalistDergiRC.Repositories
{
    public class Constants
    {
        public static readonly int FIRST_PAGE_INDEX = 0;
        public static readonly int FIRST_PAGE_NUMBER = 1;

        public static readonly string UNIVERSALIST_DERGI_TITLE = "Universalist Dergi";

        public static readonly char ITEM_SEPERATOR                 = '|';
        public static readonly char ITEM_PROPERTY_SEPERATOR        = '~';
        public static readonly string GENERIC_BOOKMARK_FORMAT      = "{0}~{1}";
        public static readonly string GENERIC_ISSUE_FORMAT         = "{0}~{1}~{2}~{3}~{4}~{5}";
        public static readonly string GENERIC_BOOKMARK_DESCRIPTION = "Sayı {0} - Sayfa {1}";

        public static readonly string BOOKMARKS_FILENAME = "Bookmarks.aso";
        public static readonly string ISSUES_FILENAME = "Issues.aso";

        public static readonly string MAGAZINE_SUMMARY_LIST_URL = "http://www.durumbilgisi.com/unv_sayilar.xml";
        public static readonly string GENERIC_MAGAZINE_PAGES_URL = "http://www.durumbilgisi.com/Sayi{0}/{1}.jpg";

        public static readonly string BOOKMARK_NORMAL_ICON = "bookmark.png";
        public static readonly string BOOKMARK_SAVED_ICON = "savedBookmark.png";

        public static readonly TimeSpan DEFAULT_CACHE_VALIDITY = new TimeSpan(0, 0, 5, 0);

        public static readonly string EMPTY_PAGE = "https://image.freepik.com/free-photo/empty-landscape_1127-111.jpg";

        
    }
}
