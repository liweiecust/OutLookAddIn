using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspenTech.MES.LogMvtResults.Domain
{
    public interface INewResultsAvailable
    {
        event EventHandler<LogMvtResultsTask> NewResultsAvailable;
    }
}
