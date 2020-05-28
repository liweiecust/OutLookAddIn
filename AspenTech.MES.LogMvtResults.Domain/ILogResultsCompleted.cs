using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspenTech.MES.LogMvtResults.Domain
{
    public interface ILogResultsTaskCompleted
    {
        event EventHandler<NotificationMailItem> LogResultsTaskCompleted;
    }
}
