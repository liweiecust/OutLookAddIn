using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspenTech.MES.LogMvtResults.Domain
{

    public class LogResultsTaskController : ILogResultsTaskCompleted
    {
        public LogResultsTaskController()
        {

        }

        public void OnNewResultsAvailable(object sender, LogMvtResultsTask e)
        {
            var para = e ?? throw new ArgumentNullException(nameof(LogResultsTaskController));
            _logMvtResultsTask = e;
            this.Execute();

        }

        

        public async void Execute()
        {
            if (_logMvtResultsTask.Activated && ((DateTime.Now - DateTime.Parse( _logMvtResultsTask.LastLogDate)).Days >= _logMvtResultsTask.SchedualInterval))
            {
                try
                {
                    List<CsvResultItem> results;
                    lock (_lock)
                    {
                        var parser = new CsvResultsParser(_logMvtResultsTask.ResultFilePath);
                        results = parser.Parse();
                    }
                    VstsSuite suite = new VstsSuite(_logMvtResultsTask);
                    await suite.PopulateTestCases();
                    if (!suite.IsSingleConfiguration()) throw new Exception("Log results tool can only support single test configuration!");
                    var tran = new CsvToVstsTransformer();
                    var res = tran.CsvListToVstsList(results, _logMvtResultsTask.AdditionalComment, _logMvtResultsTask.LogPassOrKnownIssue);
                    var logged = await suite.LogResults(res);
                    List<CsvResultItem> NotLogged = GenerateNotLoggedList(results, logged);


                    var successMail = new NotificationMailItem("Peter.Zhang@aspentech.com",
                                    $"Log results successful for {_logMvtResultsTask.NotifiedMailSubject}",
                                   GenerateNotLoggedHtmlContent(NotLogged, results.Count, res[0].Comment));
                    _logMvtResultsTask.LastLogDate = DateTime.Now.ToString();
                    Tasks.GetTasks().AddOrUpdate(_logMvtResultsTask);
                    LogResultsTaskCompleted?.Invoke(this, successMail);
                }
                catch (Exception ex)
                {
                    var failMail = new NotificationMailItem("Peter.Zhang@aspentech.com",
                                    "Fail log results!",
                                    ex.Message);
                    LogResultsTaskCompleted?.Invoke(this, failMail);
                }
                    
                //mediaWatcher.NewMediaUploaded += (sender, e) => Task.Factory.StartNew(
                //    () =>
                //    {
                //        if (sourceMedia.CopyTo(destinationMedia))
                //        {
                //            var successMail = new NotificationMailItem(Properties.Settings.Default.To,
                //                String.Format(Properties.Settings.Default.Subject, destinationMedia.ID),
                //                String.Format(Properties.Settings.Default.Body, destinationMedia.Location));

                //            MediaCopyCompleted?.Invoke(this, successMail);
                //        }
                //    }, TaskCreationOptions.LongRunning);
                //Task.Factory.StartNew(() => mediaWatcher.Execute());
            }
        }

        public static List<CsvResultItem> GenerateNotLoggedList(List<CsvResultItem> results, List<string> logged)
        {
            var NotLogged = new List<CsvResultItem>();

            foreach (var item in results)
            {
                if (!logged.Any(p => item.Id.EndsWith(p)))
                    NotLogged.Add(item);
            }

            return NotLogged;
        }

        public static string GenerateNotLoggedHtmlContent(List<CsvResultItem> results, int total, string comment)
        {
            string logged = (total - results.Count).ToString();
            string notLogged = results.Count.ToString();
            var template = Properties.Resources.NotLoggedTemplate;
            template = template.Replace("$Total", total.ToString());
            template = template.Replace("$NotLogged", notLogged);
            template = template.Replace("$logged", logged);
            template = template.Replace("$comment", comment);
            string testCaseDesc = "";
            if (results.Count > 0)
            {
                foreach (var item in results)
                {
                    testCaseDesc = testCaseDesc + "<tr><td class=\"left\">" + item.Id
                        + "</td><td>" + item.Description
                        + "</td><td class=\"pass\">" + "Analysis required" + "</td></tr>";
                }
            }

            return template.Insert(template.IndexOf("<!--marker.table_end-->"), testCaseDesc); ;
        }

        public event EventHandler<NotificationMailItem> LogResultsTaskCompleted;

        //private string _filePath;

        //private string _additionalComment;

        private LogMvtResultsTask _logMvtResultsTask;

        private readonly object _lock = new object();
    }

}
