using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using AspenTech.MES.LogMvtResults.Domain;

namespace AspenTech.MES.LogMvtResults
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            this.Application.NewMailEx += new Outlook.ApplicationEvents_11_NewMailExEventHandler(Application_NewMailEx);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        private void Application_NewMailEx(string entryIdItem)
        {
            Outlook.NameSpace ns = Application.GetNamespace("MAPI");
            var item = ns.GetItemFromID(entryIdItem);
            try
            {
                if (item is Outlook.MailItem)
                {
                    var processor = new MailItemProcessor(item as Outlook.MailItem);
                    var controller = new LogResultsTaskController();
                    processor.NewResultsAvailable += controller.OnNewResultsAvailable;
                    controller.LogResultsTaskCompleted += SendSuccessMail;
                    processor.ProcessMailRule();
                    return;
                }
            }
            catch (AggregateException ex)
            {
                var p = from innerEx in ex.InnerExceptions
                        select innerEx.Message;
                string errorMessages = String.Join<string>("\n", p.ToList());
                SendErrorMail(errorMessages);
            }
            catch (Exception ex)
            {
                SendErrorMail(ex.Message);
            }

        }

        private void SendErrorMail(string errorMessages)
        {
            Outlook.MailItem completeMail = Application.CreateItem(Outlook.OlItemType.olMailItem);
            completeMail.To = Application.Session.CurrentUser.Name;
            completeMail.Subject = "Error Log Results";
            completeMail.Body = errorMessages;
            completeMail.Send();
        }

        private void SendSuccessMail(object sender, NotificationMailItem successMail)
        {
            Outlook.MailItem completeMail = Application.CreateItem(Outlook.OlItemType.olMailItem);
            completeMail.To = Application.Session.CurrentUser.Name;
            completeMail.Subject = successMail.Subject;
            completeMail.HTMLBody = successMail.Body;
            completeMail.Send();
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
