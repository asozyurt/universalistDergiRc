using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Linq;
using UniversalistDergiRC.Model;
using UniversalistDergiRC.Repositories;
using System;

namespace UniversalistDergiRC.DataAccess
{
    public class DataAccessManager
    {
        public static ObservableCollection<MagazineSummaryModel> GetMagazineIssues(bool tryLocal)
        {
            if (tryLocal)
            {
                ObservableCollection<MagazineSummaryModel> result = ClientDataManager.GetMagazineIssues();

                if (result != null && result.Count > 0)
                    return result;
            }

            XDocument document = XDocument.Load(Constants.MAGAZINE_SUMMARY_LIST_URL);

            var q = from b in document.Descendants("Magazine")
                    select new MagazineSummaryModel
                    {
                        CoverPage = new MagazinePageModel
                        {
                            SourceURL = b.Element("CoverPage").Value
                        },
                        Title = b.Element("Title").Value,
                        SpotDescription = b.Element("SpotDescription").Value,
                        Period = b.Element("Period").Value,
                        Issue = int.Parse(b.Element("Issue").Value),
                        PageCount = int.Parse(b.Element("TotalPageCount").Value)
                    };

            ClientDataManager.UpdateMagazineIssues(q);

            return new ObservableCollection<MagazineSummaryModel>(q);
        }

        public static MagazineDetailModel GetMagazineIssueDetail(int  issueNumber)
        {
            var magazineList = GetMagazineIssues(true);
            var selectedIssue = magazineList.FirstOrDefault(x => x.Issue == issueNumber);

            if (selectedIssue == null) return null;

            MagazineDetailModel magazineDetail = new MagazineDetailModel() {
                Issue = issueNumber
            };
            List<BookmarkModel> bookMarks = ClientDataManager.GetAllBookmarks();

            int pageNumber;
            string url = string.Empty;

            for (int i = 0; i < selectedIssue.PageCount; i++)
            {
                pageNumber = i + 1;
                url = string.Format(Constants.GENERIC_MAGAZINE_PAGES_URL, issueNumber, pageNumber);
                magazineDetail.Pages.Add(
                                   new MagazinePageModel
                                   {
                                       SourceURL = url,
                                       PageNumber = pageNumber,
                                       IsBookMarked = bookMarks.Any(x => x.IssueNumber==issueNumber && x.PageNumber == pageNumber)
                                   });
            }
            return magazineDetail;
        }


    }
}
