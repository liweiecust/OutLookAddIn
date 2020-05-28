using AspenTech.MES.LogMvtResults.Domain;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AspenTech.MES.LogMvtResults
{
    internal class TaskRibbonGroup
    {

        internal void InitializeComponent()
        {
            
            this.innerTaskRibbonGroup = _addinRibbonTab.Factory.CreateRibbonGroup();
            this.ActivateTaskRibbonToggleButton = _addinRibbonTab.Factory.CreateRibbonToggleButton();
            this.SettingsRibbonButton = _addinRibbonTab.Factory.CreateRibbonButton();
            this.DeleteTaskRibbonButton = _addinRibbonTab.Factory.CreateRibbonButton();

            _addinRibbonTab.LogMvtResultsTab.Groups.Add(this.innerTaskRibbonGroup);
            this.innerTaskRibbonGroup.Visible = false;
            this.innerTaskRibbonGroup.Items.Add(this.ActivateTaskRibbonToggleButton);
            this.innerTaskRibbonGroup.Items.Add(this.SettingsRibbonButton);
            this.innerTaskRibbonGroup.Items.Add(this.DeleteTaskRibbonButton);
            this.innerTaskRibbonGroup.Label = "Unused Task";
            //this.CurrentTaskRibbonGroup.Name = "Task1TaskRibbonGroup";
            // 
            // Activate
            // 
            this.ActivateTaskRibbonToggleButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ActivateTaskRibbonToggleButton.Label = "Activate";
            //this.ActivateTaskRibbonToggleButton.Name = "ActivateTaskRibbonToggleButton";
            this.ActivateTaskRibbonToggleButton.OfficeImageId = "PersonaStatusOnline";
            this.ActivateTaskRibbonToggleButton.ShowImage = true;
            this.ActivateTaskRibbonToggleButton.ScreenTip = "Activate Task";
            this.ActivateTaskRibbonToggleButton.SuperTip = "Activate/Deactive this task.";
            this.ActivateTaskRibbonToggleButton.Click += new RibbonControlEventHandler(this.ActivateTaskRibbonToggleButton_Click);
            // 
            // SettingsRibbonButton
            // 
            this.SettingsRibbonButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.SettingsRibbonButton.Label = "Settings";
            //this.SettingsRibbonButton.Name = "SettingsRibbonButton";
            this.SettingsRibbonButton.OfficeImageId = "AdministrationHome";
            this.SettingsRibbonButton.ShowImage = true;
            this.SettingsRibbonButton.ScreenTip = "Task Settings";
            this.SettingsRibbonButton.SuperTip = "Configure task settings.";
            this.SettingsRibbonButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SettingsRibbonButton_Click);
            // 
            // DeleteTaskRibbonButton
            // 
            this.DeleteTaskRibbonButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.DeleteTaskRibbonButton.Label = "Delete";
            //this.DeleteTaskRibbonButton.Name = "DeleteTaskRibbonButton";
            this.DeleteTaskRibbonButton.OfficeImageId = "DeleteItem";
            this.DeleteTaskRibbonButton.ShowImage = true;
            this.DeleteTaskRibbonButton.ScreenTip = "Delete Task";
            this.DeleteTaskRibbonButton.SuperTip = "Delete this task.";
            this.DeleteTaskRibbonButton.Click += new RibbonControlEventHandler(DeleteTaskRibbonButton_Click);
        }

        private void DeleteTaskRibbonButton_Click(object sender, RibbonControlEventArgs e)
        {
            var result = MessageBox.Show($"Please confirm task {Task.TaskName} is to be deleted.", "Log Mvt Results Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Task.Id = Tasks.DeleteTaskFlag;
                this.Visible = false;
                UpdateGUI();
                _addinRibbonTab.UpdateGUI();
            }
        }

        public bool Visible
        {
            get { return this.innerTaskRibbonGroup.Visible; }
            set { this.innerTaskRibbonGroup.Visible = value; }
        }

        public bool Activated
        {
            get
            {
                return this.ActivateTaskRibbonToggleButton.Checked;
            }
            set
            {
                this.ActivateTaskRibbonToggleButton.Checked = value;
                //Update Togglebutton image.
                if (value) this.ActivateTaskRibbonToggleButton.OfficeImageId = "PersonaStatusOnline";
                else this.ActivateTaskRibbonToggleButton.OfficeImageId = "PersonaStatusOffline";
            }
        }

        private void SettingsRibbonButton_Click(object sender, RibbonControlEventArgs e)
        {
            var settingsDialog = new TaskSettingsDialog(this._addinRibbonTab,Task);
            if (settingsDialog.ShowDialog() == DialogResult.OK)
            {
                UpdateGUI();
                _addinRibbonTab.UpdateGUI();
            }
        }

        private void ActivateTaskRibbonToggleButton_Click(object sender, RibbonControlEventArgs e)
        {
            Task.Activated = this.Activated;
            UpdateGUI();
            _addinRibbonTab.UpdateGUI();
        }

        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton ActivateTaskRibbonToggleButton;
        internal RibbonButton SettingsRibbonButton;
        internal RibbonButton DeleteTaskRibbonButton;
        internal RibbonGroup innerTaskRibbonGroup;
        

        public TaskRibbonGroup(LogMvtResultsRibbon addinRibbonTab, LogMvtResultsTask task = null)
        {
           
            _addinRibbonTab = addinRibbonTab;
            InitializeComponent();
            if (null == task) task = new LogMvtResultsTask();
            Task = task;
            UpdateGUI();
        }

        public void UpdateGUI()
        {
            this.innerTaskRibbonGroup.Label = Task.TaskName;
            this.Activated = Task.Activated;
        }

        private LogMvtResultsRibbon _addinRibbonTab;

        public LogMvtResultsTask Task
        {
            get
            {
                return _task;
            }
            set
            {
                _task = value;
                UpdateGUI();
            }
        }

        private LogMvtResultsTask _task;
    }
}
