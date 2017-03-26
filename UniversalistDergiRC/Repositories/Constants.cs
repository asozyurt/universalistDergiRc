using System;

namespace UniversalistDergiRC.Repositories
{
    public class Constants
    {
        public static readonly int FIRST_PAGE_INDEX = 0;
        public static readonly int FIRST_PAGE_NUMBER = 1;
        public static readonly string UNIVERSALIST_DERGI_TITLE = "Universalist Dergi";
        public static readonly string UNIVERSALIST_DERGI_TITLE_UPPERCASE = "UNIVERSALIST DERGİ";
        public static readonly char ITEM_SEPERATOR = '|';
        public static readonly char ITEM_PROPERTY_SEPERATOR = '~';
        public static readonly string GENERIC_BOOKMARK_FORMAT = "{0}~{1}";
        public static readonly string GENERIC_STATE_FORMAT = "{0}~{1}";
        public static readonly string GENERIC_ISSUE_FORMAT = "{0}~{1}~{2}~{3}~{4}~{5}";
        public static readonly string GENERIC_BOOKMARK_DESCRIPTION = "Sayı {0} - Sayfa {1}";

        public static readonly string BOOKMARKS_FILENAME = "Bookmarks.aso";
        public static readonly string ISSUES_FILENAME = "Issues.aso";

        public static readonly string MAGAZINE_SUMMARY_LIST_URL = "http://www.harweast.com/Projects/universalist_dergi/unv_sayilar.xml";
        public static readonly string GENERIC_MAGAZINE_PAGES_URL = "http://www.harweast.com/Projects/universalist_dergi/Sayi{0}/{0}-{1}.jpg";

        public static readonly string BOOKMARK_NORMAL_ICON = "bookmark.png";
        public static readonly string BOOKMARK_SAVED_ICON = "savedBookmark.png";

        public static readonly TimeSpan DEFAULT_CACHE_VALIDITY = new TimeSpan(90, 0, 0, 0);

        public static readonly string EMPTY_PAGE = "http://www.harweast.com/Projects/universalist_dergi/empty_page.png";

        public static readonly string CONNECTION_ERROR_TITLE = "Bağlantı Hatası";
        public static readonly string CONNECTION_ERROR_MESSAGE = "İnternet bağlantınızı kontrol ediniz!";
        public static readonly string CONNECTION_ERROR_MESSAGEKEY = "ConnectionErrorMessage";

        public static readonly string OK = "Tamam";

        public static readonly string RESET_IMAGE_POSITION_MESSAGEKEY = "ResetImagePosition";

        public static readonly string ANIMATE_IMAGE_MESSAGEKEY = "AnimateImage";

        public static readonly string READING_PAGE_INITIAL_TITLE = "Okuyucu";

        public static readonly string LEFT_SLIDE = "LEFT_SLIDE";
        public static readonly string RIGHT_SLIDE = "RIGHT_SLIDE";

        public static readonly string DEVELOPERS_WEBSITE = "http://asozyurt.com";
        public static readonly string MAGAZINE_WEBSITE = "http://universalistdergi.org";
    }
}
