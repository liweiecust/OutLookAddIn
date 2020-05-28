using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspenTech.MES.LogMvtResults.Domain
{
    public class LogMvtResultsTaskSection : ConfigurationSection
    {
        [ConfigurationProperty(nameof(Name))]
        public string Name
        {
            get
            {
                return (string)base[nameof(Name)];
            }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public LogMvtResultsTaskCollection TaskCollection
        {
            get
            {
                return (LogMvtResultsTaskCollection)base[""];
            }
        }

        [ConfigurationProperty(nameof(MaxTaskCount), IsRequired = true, DefaultValue = 5)]
        public int MaxTaskCount
        {
            get
            {
                return (int)base[nameof(MaxTaskCount)];
            }
        }

        public bool IsTaskNameExists(LogMvtResultsTask task)
        {
            return TaskCollection.OfType<LogMvtResultsTask>().Where(p=>p.Id != task.Id).Any(p => p.TaskName == task.TaskName);
        }

        public bool IsNotifiedMailSubjectExists(LogMvtResultsTask task)
        {
            //if tasks contains current task(which id is already in task collection), regards as a update.
            return TaskCollection.OfType<LogMvtResultsTask>().Where(p => p.Id != task.Id).Any(p => p.NotifiedMailSubject == task.NotifiedMailSubject);
        }

    }
}
