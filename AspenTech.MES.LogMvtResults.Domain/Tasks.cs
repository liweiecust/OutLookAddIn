using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;

namespace AspenTech.MES.LogMvtResults.Domain
{
    public sealed class Tasks
    {
        Configuration Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        const string sectionName = "LogMvtResultsTasks";
        public const int DeleteTaskFlag = -1;

        public static Tasks GetTasks()
        {
                if (null == _tasks) _tasks = new Tasks();
                return _tasks;
        }

        private static Tasks _tasks = null;

        private Tasks()
        {
            if (Configuration.Sections[sectionName] == null)
            {
                Configuration.Sections.Add(sectionName, new LogMvtResultsTaskSection());
            }
        }

        public bool IsTaskNameExists(LogMvtResultsTask task)
        {
            return Section.IsNotifiedMailSubjectExists(task);
        }

        public bool IsNotifiedMailSubjectExists(LogMvtResultsTask task)
        {
            return Section.IsNotifiedMailSubjectExists(task);
        }

        //public LogMvtResultsTaskSection Section
        //{
        //    get
        //    {
        //        var section = (LogMvtResultsTaskSection)Configuration.Sections[sectionName];
        //        return section;
        //    }
        //project.AppSettings.Settings.Add("peter", "test");
        //LogMvtResultsTask logMvtResultsTask = new LogMvtResultsTask("Task 2", "test", "test");
        //LogMvtResultsTask logMvtResultsTask1 = new LogMvtResultsTask("Task  3", "test1", "test2");
        //LogMvtResultsTaskSection section = new LogMvtResultsTaskSection();

        //if (settings.Sections["LogMvtResultsTasks"] == null)
        //{
        //    settings.Sections.Add("LogMvtResultsTasks", section);
        //    section.TaskCollection.Add(logMvtResultsTask);
        //    section.TaskCollection.Add(logMvtResultsTask1);
        //}
        //section = (LogMvtResultsTaskSection)settings.Sections["LogMvtResultsTasks"];
        //settings.Save(ConfigurationSaveMode.Modified);
        //section.TaskCollection[logMvtResultsTask] = new LogMvtResultsTask("Task   2", "test222", "test222");
        //section.TaskCollection.Remove(logMvtResultsTask1);

        //section.TaskCollection[logMvtResultsTask1] = logMvtResultsTask1;
        //section.TaskCollection[1] = logMvtResultsTask1;
        //project.Sections["Task1"].SectionInformation.Type = "AspenTech.MES.LogMvtResults"
        //ConfigurationSectionGroup task = new ConfigurationSectionGroup();
        ////task.Sections.Add("test", new );
        //project.SectionGroups.Add("task", task);
        //task.Type = "hel";
        //settings.Save(ConfigurationSaveMode.Modified);
        //task.Type =
        //project.SectionGroups.Add("IP21", task);

        //return ""; //project.AppSettings.Settings["peter"].Value;
        //}

       

        public LogMvtResultsTask this[int index]
        {
            get
            {
                return Section.TaskCollection[index];
            }
        }

        public int Count => Section.TaskCollection.Count;

        public int MaxTaskCount => Section.MaxTaskCount;

        public int NextId()
        {
            var tasks = Section.TaskCollection.OfType<LogMvtResultsTask>();
            return (tasks.Count() == 0) ? 1 : tasks.Max(p => p.Id) + 1;
        }

        public LogMvtResultsTask GetExistsByNotifiedMailSubject(string NotifiedMailSubject)
        {
            var tasks = Section.TaskCollection.OfType<LogMvtResultsTask>();
            return tasks.FirstOrDefault(p => p.NotifiedMailSubject == NotifiedMailSubject); 
        }

        public void AddOrUpdate(LogMvtResultsTask logMvtResultsTask)
        {
            Section.TaskCollection.AddOrUpdate(logMvtResultsTask);
            Configuration.Save(ConfigurationSaveMode.Modified);
        }

        public void Remove(LogMvtResultsTask logMvtResultsTask)
        {
            Section.TaskCollection.Remove(logMvtResultsTask);
            Configuration.Save(ConfigurationSaveMode.Modified);
        }

        public void SaveTasks()
        {
            for (int i = 0; i < Count; i++)
            {
                if (Section.TaskCollection[i].Id == Tasks.DeleteTaskFlag)
                    Remove(Section.TaskCollection[i]);
            }
            Configuration.Save(ConfigurationSaveMode.Modified);
            //var settings = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //settings.Sections.Remove("LogMvtResultsTasks");
            //settings.Sections.Add("LogMvtResultsTasks", TaskSection);

            //var temp = (LogMvtResultsTaskSection)settings.Sections["LogMvtResultsTasks"];
            //if (temp == null)
            //{
            //    settings.Sections.Add("LogMvtResultsTasks", _logMvtResultsTaskSection);
            //}

            //if (temp != null)
            //{
            //    for (int i = 0; i < temp.TaskCollection.Count; i++)
            //    {
            //        temp.TaskCollection.Remove(temp.TaskCollection[i]);
            //    }
            //}
            //for (int i = 0; i < TaskSection.TaskCollection.Count; i++)
            //{
            //    temp.TaskCollection.Add(TaskSection.TaskCollection[i]);
            //}
            //var section = GetTaskSettings();
            //LogMvtResultsTask logMvtResultsTask = new LogMvtResultsTask("Task 2", "test", "test");
            //LogMvtResultsTask logMvtResultsTask1 = new LogMvtResultsTask("Task 2", "test1", "test"); ;// new LogMvtResultsTask("Task  3", "test1", "test2");
            //LogMvtResultsTaskSection section = new LogMvtResultsTaskSection();

            //if (settings.Sections["LogMvtResultsTasks"] == null)
            //{
            //    settings.Sections.Add("LogMvtResultsTasks", section);
            //TaskSection.TaskCollection.Add(logMvtResultsTask);
            //TaskSection.TaskCollection.Add(logMvtResultsTask1);
            //}
            //section = (LogMvtResultsTaskSection)settings.Sections["LogMvtResultsTasks"];
            //settings.Save(ConfigurationSaveMode.Modified);


        }

        private LogMvtResultsTaskSection Section
        {
            get
            {
                var section = (LogMvtResultsTaskSection)Configuration.Sections[sectionName];
                return section;
            }
        }


    }
}
