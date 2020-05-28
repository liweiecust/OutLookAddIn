using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AspenTech.MES.LogMvtResults.Domain;
using Microsoft.Office.Tools.Ribbon;

namespace AspenTech.MES.LogMvtResults
{
    public partial class LogMvtResultsRibbon
    {
        public void UpdateGUI()
        {
            int maxTaskCount = tasks.MaxTaskCount;
            //var currentTasks = tasks.TaskSection.TaskCollection;
            Array.ForEach(TaskRibbonGroups, p => p.Visible = false);
            tasks.SaveTasks();
            for (int i = 0; i < maxTaskCount; i++)
            {
                if (i < tasks.Count)
                {
                    TaskRibbonGroups[i].Task = tasks[i];
                    TaskRibbonGroups[i].Visible = true;
                }
                else
                {
                    TaskRibbonGroups[i].Task = new LogMvtResultsTask();
                }
            }
        }

        public bool IsTaskNameExists(LogMvtResultsTask task)
        {
            return tasks.IsTaskNameExists(task);
        }

        public bool IsNotifiedMailSubjectExists(LogMvtResultsTask task)
        {
            return tasks.IsNotifiedMailSubjectExists(task);
        }

        private void InitTaskRibbonGroups()
        {
            int maxTaskCount = tasks.MaxTaskCount;

            TaskRibbonGroups = new TaskRibbonGroup[maxTaskCount];


            for (int i = 0; i < maxTaskCount; i++)
            {
                if (i < tasks.Count)
                {
                    TaskRibbonGroups[i] = new TaskRibbonGroup(this, tasks[i]) { Visible = true };
                }
                else
                {
                    TaskRibbonGroups[i] = new TaskRibbonGroup(this);
                }
            }
        }

        private void LogMvtResultsRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void AddTaskRibbonButton_Click(object sender, RibbonControlEventArgs e)
        {
            var addNewTask = new TaskSettingsDialog(this) { Text = "Add New Task" };
            if (addNewTask.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                addNewTask.Task.Id = tasks.NextId();
                tasks.AddOrUpdate(addNewTask.Task);
                UpdateGUI();
            }
        }

        internal TaskRibbonGroup[] TaskRibbonGroups { get; private set; }

        Tasks tasks = Tasks.GetTasks();
    }
}
