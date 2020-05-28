using Microsoft.TeamFoundation.TestManagement.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspenTech.MES.LogMvtResults.Domain
{
    public class CsvToVstsTransformer
    {
        public VstsTestCaseResult CsvToVsts(CsvResultItem csvRawData, string additionalComment = null)
        {
            if (null == csvRawData) throw new ArgumentNullException("Raw data is null!");

            var res = new VstsTestCaseResult();
            //res.Id = csvRawData.Id.Replace("VSTS", string.Empty);
            res.Id = Regex.Replace(csvRawData.Id, @"^VSTS", string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (csvRawData.Result.ToUpper() == "PASS") res.Outcome = "passed";
            else if (csvRawData.Result == "TBD")
            {
                var defects = GetKnownDefects(csvRawData.Description);
                if (defects == null) throw new Exception($"Test {csvRawData.Id} fail without defect linked!");
                //if (defects == null) return default(VstsTestCaseResult);
                if (defects.Count != 0)
                {
                    if (FailTagRegex.IsMatch(csvRawData.Description))
                        res.Outcome = "failed";
                    else if (PassTagRegex.IsMatch(csvRawData.Description))
                        res.Outcome = "passed";
                    //else return default(VstsTestCaseResult);
                    else throw new Exception($"Test {csvRawData.Id} outcome is not following specification!");
                    res.AssociatedBugs = new List<string>();
                    res.AssociatedBugs.AddRange(defects);
                }
            }
            else
            {
                //return default(VstsTestCaseResult);
                throw new Exception($"Test {csvRawData.Id} outcome is not following specification!");
            }

            res.Description = csvRawData.Description;

            res.Comment = Regex.Match(csvRawData.Note, @"(?<media>Media\d+)\.iso",  RegexOptions.IgnoreCase|RegexOptions.Compiled).Groups["media"].Value;
            if (!string.IsNullOrWhiteSpace(additionalComment)) res.Comment = additionalComment + "," + res.Comment;

            return res;
        }

        public List<VstsTestCaseResult> CsvListToVstsList(List<CsvResultItem> csvs, string additionalComment = null, bool logPassOrKnownIssueResults = true)
        {
            var res = new List<VstsTestCaseResult>();
            foreach (var item in csvs)
            {
                try
                {
                    res.Add(CsvToVsts(item, additionalComment));
                }
                catch (Exception)
                {
                    if (!logPassOrKnownIssueResults) throw;
                }
            }
            return res;
        }

        public List<string> GetKnownDefects(string taggedDescription)
        {
            List<string> failId = null;
            if (FailTagRegex.IsMatch(taggedDescription))
                failId = FailTagRegex.Match(taggedDescription).Groups["failID"].Value.Split('|').Select(p => p.Trim()).ToList();
            else if (PassTagRegex.IsMatch(taggedDescription))
                failId = PassTagRegex.Match(taggedDescription).Groups["failID"].Value.Split('|').Select(p => p.Trim()).ToList();
            return failId;
        }

        public static Regex FailTagRegex => new Regex(@"\[fail.*\s(?<failID>[\d|]+)\s*\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static Regex PassTagRegex => new Regex(@"\[pass.*\s(?<failID>[\d|]+)\s*\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }
}
