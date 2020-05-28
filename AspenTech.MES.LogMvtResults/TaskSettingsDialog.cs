using AspenTech.MES.LogMvtResults.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AspenTech.MES.LogMvtResults
{
    public partial class TaskSettingsDialog : Form
    {
        public TaskSettingsDialog(LogMvtResultsRibbon addinRibbonTab)
            : this(addinRibbonTab, new LogMvtResultsTask())
        {

        }

        public TaskSettingsDialog(LogMvtResultsRibbon addinRibbonTab, LogMvtResultsTask task)
        {
            _addinRibbonTab = addinRibbonTab;
            this.Task = task;
            InitializeComponent();
            this.AcceptButton = this.OKPushButton;
            this.CancelButton = this.CancelPushButton;
            this.TaskNameTextBox.DataBindings.Add(nameof(TaskNameTextBox.Text), this.Task, nameof(Task.TaskName));
            this.NotifiedMailSubjectTextBox.DataBindings.Add(nameof(NotifiedMailSubjectTextBox.Text), this.Task, nameof(Task.NotifiedMailSubject));
            this.PersonalAccessTokenTextBox.DataBindings.Add(nameof(PersonalAccessTokenTextBox.Text), this.Task, nameof(Task.PersonalAccessToken));
            this.TestSutieUriTextBox.DataBindings.Add(nameof(TestSutieUriTextBox.Text), this.Task, nameof(Task.FullTestSuiteUri));
            this.AdditionalCommentTextBox.DataBindings.Add(nameof(AdditionalCommentTextBox.Text), this.Task, nameof(Task.AdditionalComment));
            this.ActivaedCheckBox.DataBindings.Add(nameof(ActivaedCheckBox.Checked), this.Task, nameof(Task.Activated));
            this.SchedualIntervalNumericUpDown.DataBindings.Add(nameof(SchedualIntervalNumericUpDown.Value), this.Task, nameof(Task.SchedualInterval));
            this.LogResolvedcheckBox.DataBindings.Add(nameof(LogResolvedcheckBox.Checked), this.Task, nameof(Task.LogPassOrKnownIssue));
        }

        public bool RequiredTextBoxsAreEmpty()
        {
            return string.IsNullOrWhiteSpace(TaskNameTextBox.Text)
                || string.IsNullOrWhiteSpace(NotifiedMailSubjectTextBox.Text)
                || string.IsNullOrWhiteSpace(PersonalAccessTokenTextBox.Text)
                || string.IsNullOrWhiteSpace(TestSutieUriTextBox.Text);
        }

        private void CancelPushButton_Click(object sender, EventArgs e)
        {
            _validate = false;
            this.Close();
        }

        private void OKPushButton_Click(object sender, EventArgs e)
        {
            //if (RequiredTextBoxsAreEmpty())
            //{
            //    MessageBox.Show("Task Name, Notified Mail Subject, Personal Access Token, Test Sutie Uri can't empty.\n" +
            //        "Please input required information!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    //return;
            //}
            //if (_addinRibbonTab.CheckNameAndNotifiedMailExists(Task, out string message))
            //{
            //    if (MessageBox.Show(message, "Input Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //        this.Close();
            //    //return;
            //}
            this._validate = true;
            this.OKPushButton.Focus();
            this.Close();
        }

        public LogMvtResultsTask Task { get; set; }

        LogMvtResultsRibbon _addinRibbonTab;

        private void TaskSettingsDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_validate == false) return;
            if (RequiredTextBoxsAreEmpty())
            {
                e.Cancel = true;
                MessageBox.Show("Task Name, Notified Mail Subject, Personal Access Token, Test Sutie Uri can't empty.\n" +
                    "Please input required information!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Task.IsValidTestSuiteUri())
            {
                e.Cancel = true;
                MessageBox.Show("Test suite uri is not valid, please input a valid uri.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (_addinRibbonTab.IsNotifiedMailSubjectExists(Task))
            {
                e.Cancel = true;
                MessageBox.Show("Notified mail subject {task.NotifiedMailSubject} already exists.\n" +
                    "Must be unqie otherwise it may leads to log duplicate results.\n", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_addinRibbonTab.IsTaskNameExists(Task))
            {
                if (MessageBox.Show("Task name {task.TaskName} already exists, may cause confusion.\n" +
                    "Are you sure to procced?", "Input Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
                    e.Cancel = true;
                return;
            }

        }

        private void Trim_Text(object sender, EventArgs e)
        {
            if (!(sender is TextBox textbox)) return;
            textbox.Text = textbox.Text.Trim();
        }

        private bool _validate = true;
    }
}
