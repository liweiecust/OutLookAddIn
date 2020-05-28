using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspenTech.MES.LogMvtResults.Domain
{
    [ConfigurationCollection(typeof(LogMvtResultsTask), AddItemName = "task")]
    public class LogMvtResultsTaskCollection : ConfigurationElementCollection
    {
        internal LogMvtResultsTaskCollection()
        {
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new LogMvtResultsTask();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LogMvtResultsTask)element).Id;
        }

        public LogMvtResultsTask this[LogMvtResultsTask task]
        {
            get
            {
                return (LogMvtResultsTask)BaseGet((object) task.Id);
            }
            set
            {
                int index = BaseIndexOf(task);
                if (index != -1)
                {
                    BaseRemoveAt(index);
                    BaseAdd(index, value);
                }
                else
                {
                    BaseAdd(value);
                }
            }
        }

        public LogMvtResultsTask this[int index]
        {
            get
            {
                return (LogMvtResultsTask)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public void AddOrUpdate(LogMvtResultsTask task)
        {
            this.BaseAdd(task);
        }

        public void Remove(LogMvtResultsTask task)
        {
            this.BaseRemove(task.Id);
        }

        protected override string ElementName => "task";

        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;
    }
}
