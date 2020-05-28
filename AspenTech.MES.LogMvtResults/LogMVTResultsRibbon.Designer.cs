namespace AspenTech.MES.LogMvtResults
{
    partial class LogMvtResultsRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public LogMvtResultsRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
            InitTaskRibbonGroups();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LogMvtResultsTab = this.Factory.CreateRibbonTab();
            this.TaskRibbonGroup = this.Factory.CreateRibbonGroup();
            this.AddTaskRibbonButton = this.Factory.CreateRibbonButton();
            this.LogMvtResultsTab.SuspendLayout();
            this.TaskRibbonGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogMvtResultsTab
            // 
            this.LogMvtResultsTab.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.LogMvtResultsTab.Groups.Add(this.TaskRibbonGroup);
            this.LogMvtResultsTab.Label = "Log MVT Results";
            this.LogMvtResultsTab.Name = "LogMvtResultsTab";
            // 
            // TaskRibbonGroup
            // 
            this.TaskRibbonGroup.Items.Add(this.AddTaskRibbonButton);
            this.TaskRibbonGroup.Label = "Task";
            this.TaskRibbonGroup.Name = "TaskRibbonGroup";
            // 
            // AddTaskRibbonButton
            // 
            this.AddTaskRibbonButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.AddTaskRibbonButton.Label = "Add";
            this.AddTaskRibbonButton.Name = "AddTaskRibbonButton";
            this.AddTaskRibbonButton.OfficeImageId = "AddAccount";
            this.AddTaskRibbonButton.ScreenTip = "Add Task";
            this.AddTaskRibbonButton.ShowImage = true;
            this.AddTaskRibbonButton.SuperTip = "Add a new Log Mvt Results task";
            this.AddTaskRibbonButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddTaskRibbonButton_Click);
            // 
            // LogMvtResultsRibbon
            // 
            this.Name = "LogMvtResultsRibbon";
            this.RibbonType = "Microsoft.Outlook.Explorer";
            this.Tabs.Add(this.LogMvtResultsTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.LogMvtResultsRibbon_Load);
            this.LogMvtResultsTab.ResumeLayout(false);
            this.LogMvtResultsTab.PerformLayout();
            this.TaskRibbonGroup.ResumeLayout(false);
            this.TaskRibbonGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab LogMvtResultsTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup TaskRibbonGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton AddTaskRibbonButton;
    }

    partial class ThisRibbonCollection
    {
        internal LogMvtResultsRibbon LogMvtResultsRibbon
        {
            get { return this.GetRibbon<LogMvtResultsRibbon>(); }
        }
    }
}
