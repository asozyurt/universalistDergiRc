using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UniversalistDergiRC.Model;
using UniversalistDergiRC.Repositories;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace UniversalistDergiRC.DataAccess
{
    public class ClientDataManager
    {
        public static bool DeleteSingleBookMark(int issueNumber, int pageNumber)
        {
            /* Not: Bu metod daha efektif yazilabilir */
            bool result = false;
            if (issueNumber == 0 || pageNumber == 0)
                return result;

            try
            {
                string fileName = Constants.BOOKMARKS_FILENAME;
                string allBookMarks = DependencyService.Get<IFileOperations>().ReadAllText(Constants.BOOKMARKS_FILENAME);

                string serializedBookmarkText = serializeBookmark(issueNumber, pageNumber);

                int startIndex = allBookMarks.IndexOf(serializedBookmarkText);
                StringBuilder stringToBeRemoved = new StringBuilder();
                if (startIndex > 0)
                    stringToBeRemoved.Append(Constants.ITEM_SEPERATOR);

                stringToBeRemoved.Append(serializedBookmarkText);

                if (startIndex ==0 && serializedBookmarkText.Length < allBookMarks.Length)
                    stringToBeRemoved.Append(Constants.ITEM_SEPERATOR);

                allBookMarks = allBookMarks.Replace(stringToBeRemoved.ToString(), string.Empty);

                DependencyService.Get<IFileOperations>().SaveText(Constants.BOOKMARKS_FILENAME, allBookMarks);
                result = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error when deleting bookmark. Program should continue");
                result = false;
            }

            return result;


        }
        
        public static bool SaveSingleBookMark(int issueNumber, int pageNumber, string userDescription = null)
        {
            bool result = false;

            if (issueNumber == 0 || pageNumber == 0)
                return result;

            string serializedBookmarkText = serializeBookmark(issueNumber, pageNumber);

            string fileName = Constants.BOOKMARKS_FILENAME;
            string allBookMarks = DependencyService.Get<IFileOperations>().ReadAllText(fileName);

            if (allBookMarks.Contains(serializedBookmarkText)) return true;

            try
            {
                StringBuilder sb = new StringBuilder(allBookMarks);
                if (allBookMarks != null && allBookMarks.Length > 0)
                    sb.Append(Constants.ITEM_SEPERATOR);

                sb.Append(serializedBookmarkText);
                DependencyService.Get<IFileOperations>().SaveText(fileName, sb.ToString());
                result = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Error when saving bookmark. Program should continue");
                result = false;
            }

            return result;
        }

        internal static ObservableCollection<MagazineSummaryModel> GetMagazineIssues()
        {
            string savedMagazineIssues = DependencyService.Get<IFileOperations>().ReadAllText(Constants.ISSUES_FILENAME);

            ObservableCollection<MagazineSummaryModel> result = new ObservableCollection<MagazineSummaryModel>();

            if (string.IsNullOrEmpty(savedMagazineIssues))
                return result;

            string[] magazineSummaryArray = savedMagazineIssues.Split(new[] { Constants.ITEM_SEPERATOR }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in magazineSummaryArray)
            {
                MagazineSummaryModel magazineSummary = MagazineSummaryModel.GenerateFromSerializedText(item);
                if (magazineSummary != null)
                    result.Add(magazineSummary);
            }
            return result;

        }

        internal static void UpdateMagazineIssues(IEnumerable<MagazineSummaryModel> magazineIssueList)
        {
            StringBuilder sb = new StringBuilder();
            int count = magazineIssueList.Count();
            int index = 0;
            foreach (var item in magazineIssueList)
            {
                sb.Append(serializeMagazineSummary(item));
                if (index < count - 1)
                    sb.Append(Constants.ITEM_SEPERATOR);
                index++;
            }

            DependencyService.Get<IFileOperations>().SaveText(Constants.ISSUES_FILENAME, sb.ToString());
        }

        public static List<BookmarkModel> GetAllBookmarks()
        {
            string allBookMarks = DependencyService.Get<IFileOperations>().ReadAllText(Constants.BOOKMARKS_FILENAME);

            List<BookmarkModel> result = new List<BookmarkModel>();

            if (string.IsNullOrEmpty(allBookMarks))
                return result;

            string[] bookmarkArray = allBookMarks.Split(new[] { Constants.ITEM_SEPERATOR }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in bookmarkArray)
            {
                BookmarkModel bookmark = BookmarkModel.GenerateFromSerializedText(item);
                if (bookmark != null)
                    result.Add(bookmark);
            }
            return result;
        }

        private static string serializeBookmark(int issueNumber, int pageNumber)
        {
            return string.Format(Constants.GENERIC_BOOKMARK_FORMAT, issueNumber, pageNumber);
        }

        private static string serializeMagazineSummary(MagazineSummaryModel magazineSummary)
        {
            return string.Format(Constants.GENERIC_ISSUE_FORMAT, magazineSummary.CoverPage.SourceURL, magazineSummary.Issue, magazineSummary.PageCount, magazineSummary.Period, magazineSummary.Title);
        }
    }
}
