using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspenTech.MES.LogMvtResults.Domain
{
    public class NotificationMailItem
    {
        public NotificationMailItem(string aTo, string aSubject, string aBody)
        {
            To = aTo ?? throw new ArgumentNullException(nameof(To));
            Subject = aSubject ?? throw new ArgumentNullException(nameof(Subject));
            Body = aBody ?? throw new ArgumentNullException(nameof(Body));

        }

        public string To { get; }

        public string Subject { get; }

        public string Body { get; }
    }
}
