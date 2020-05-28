using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Text.RegularExpressions;
using AspenTech.MES.LogMvtResults.Domain;
using System.IO;
using System.Reflection;

namespace AspenTech.MES.LogMvtResults
{
    public class MailItemProcessor : INewResultsAvailable
    {
        public MailItemProcessor(Outlook.MailItem anMailItem)
        {
            _Item = anMailItem;
        }

        public event EventHandler<LogMvtResultsTask> NewResultsAvailable;

        public void ProcessMailRule()
        {
            var tasks = Tasks.GetTasks();
            var task = tasks.GetExistsByNotifiedMailSubject(_Item.Subject.Trim());
            if (task == null) return;
            if (_Item.Attachments.Count == 0) return;
            var attachment = _Item.Attachments[1];
            if (attachment.FileName != "ExecutionResult.csv") return;
            _Item.UnRead = false;
            
            var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), attachment.FileName);
            if (File.Exists(filePath)) File.Delete(filePath);
            attachment.SaveAsFile(filePath);
            task.ResultFilePath = filePath;
            NewResultsAvailable?.Invoke(this, task);
        }

        Outlook.MailItem _Item;
    }
}
