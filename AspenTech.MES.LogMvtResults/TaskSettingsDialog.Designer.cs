namespace AspenTech.MES.LogMvtResults
{
    partial class TaskSettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskSettingsDialog));
            this.TaskNameLabel = new System.Windows.Forms.Label();
            this.TaskNameTextBox = new System.Windows.Forms.TextBox();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ActivaedCheckBox = new System.Windows.Forms.CheckBox();
            this.PersonalAccessTokenTextBox = new System.Windows.Forms.TextBox();
            this.TestSutieUriTextBox = new System.Windows.Forms.TextBox();
            this.AdditionalCommentTextBox = new System.Windows.Forms.TextBox();
            this.NotifiedMailSubjectTextBox = new System.Windows.Forms.TextBox();
            this.SchedualIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.LogResolvedcheckBox = new System.Windows.Forms.CheckBox();
            this.PersonalAccessTokenLabel = new System.Windows.Forms.Label();
            this.SuiteUriLabel = new System.Windows.Forms.Label();
            this.OKPushButton = new System.Windows.Forms.Button();
            this.CancelPushButton = new System.Windows.Forms.Button();
            this.AdditionalCommentLabel = new System.Windows.Forms.Label();
            this.NotifiedMailSubjectLabel = new System.Windows.Forms.Label();
            this.SchedualLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SchedualIntervalNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // TaskNameLabel
            // 
            this.TaskNameLabel.AutoSize = true;
            this.TaskNameLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TaskNameLabel.Location = new System.Drawing.Point(14, 25);
            this.TaskNameLabel.Name = "TaskNameLabel";
            this.TaskNameLabel.Size = new System.Drawing.Size(68, 13);
            this.TaskNameLabel.TabIndex = 0;
            this.TaskNameLabel.Text = "Task Name: ";
            this.TaskNameLabel.UseMnemonic = false;
            // 
            // TaskNameTextBox
            // 
            this.TaskNameTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TaskNameTextBox.Location = new System.Drawing.Point(146, 22);
            this.TaskNameTextBox.MaxLength = 25;
            this.TaskNameTextBox.Name = "TaskNameTextBox";
            this.TaskNameTextBox.Size = new System.Drawing.Size(136, 20);
            this.TaskNameTextBox.TabIndex = 1;
            this.ToolTip.SetToolTip(this.TaskNameTextBox, "An unique task name.\nNotes: Name is case insensitive.\ni.e. Task1, task1, TASK1 ar" +
        "e all same name.");
            this.TaskNameTextBox.Leave += new System.EventHandler(this.Trim_Text);
            // 
            // ActivaedCheckBox
            // 
            this.ActivaedCheckBox.AutoSize = true;
            this.ActivaedCheckBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ActivaedCheckBox.Location = new System.Drawing.Point(288, 26);
            this.ActivaedCheckBox.Name = "ActivaedCheckBox";
            this.ActivaedCheckBox.Size = new System.Drawing.Size(71, 17);
            this.ActivaedCheckBox.TabIndex = 2;
            this.ActivaedCheckBox.Text = "Activated";
            this.ToolTip.SetToolTip(this.ActivaedCheckBox, "Task is activated or not. ");
            this.ActivaedCheckBox.UseVisualStyleBackColor = true;
            // 
            // PersonalAccessTokenTextBox
            // 
            this.PersonalAccessTokenTextBox.Location = new System.Drawing.Point(146, 106);
            this.PersonalAccessTokenTextBox.MaxLength = 60;
            this.PersonalAccessTokenTextBox.Name = "PersonalAccessTokenTextBox";
            this.PersonalAccessTokenTextBox.Size = new System.Drawing.Size(572, 20);
            this.PersonalAccessTokenTextBox.TabIndex = 6;
            this.ToolTip.SetToolTip(this.PersonalAccessTokenTextBox, "VSTS Personal Access Token");
            this.PersonalAccessTokenTextBox.UseSystemPasswordChar = true;
            this.PersonalAccessTokenTextBox.Leave += new System.EventHandler(this.Trim_Text);
            // 
            // TestSutieUriTextBox
            // 
            this.TestSutieUriTextBox.Location = new System.Drawing.Point(146, 148);
            this.TestSutieUriTextBox.MaxLength = 512;
            this.TestSutieUriTextBox.Name = "TestSutieUriTextBox";
            this.TestSutieUriTextBox.Size = new System.Drawing.Size(572, 20);
            this.TestSutieUriTextBox.TabIndex = 7;
            this.ToolTip.SetToolTip(this.TestSutieUriTextBox, "VSTS test suite full uri");
            this.TestSutieUriTextBox.Leave += new System.EventHandler(this.Trim_Text);
            // 
            // AdditionalCommentTextBox
            // 
            this.AdditionalCommentTextBox.Location = new System.Drawing.Point(146, 190);
            this.AdditionalCommentTextBox.MaxLength = 25;
            this.AdditionalCommentTextBox.Name = "AdditionalCommentTextBox";
            this.AdditionalCommentTextBox.Size = new System.Drawing.Size(572, 20);
            this.AdditionalCommentTextBox.TabIndex = 8;
            this.ToolTip.SetToolTip(this.AdditionalCommentTextBox, "Additional comments need to add to test result.");
            this.AdditionalCommentTextBox.Leave += new System.EventHandler(this.Trim_Text);
            // 
            // NotifiedMailSubjectTextBox
            // 
            this.NotifiedMailSubjectTextBox.Location = new System.Drawing.Point(146, 62);
            this.NotifiedMailSubjectTextBox.MaxLength = 60;
            this.NotifiedMailSubjectTextBox.Name = "NotifiedMailSubjectTextBox";
            this.NotifiedMailSubjectTextBox.Size = new System.Drawing.Size(572, 20);
            this.NotifiedMailSubjectTextBox.TabIndex = 5;
            this.ToolTip.SetToolTip(this.NotifiedMailSubjectTextBox, "Notified Email Subject which you want to monitor.");
            this.NotifiedMailSubjectTextBox.Leave += new System.EventHandler(this.Trim_Text);
            // 
            // SchedualIntervalNumericUpDown
            // 
            this.SchedualIntervalNumericUpDown.Location = new System.Drawing.Point(681, 23);
            this.SchedualIntervalNumericUpDown.Name = "SchedualIntervalNumericUpDown";
            this.SchedualIntervalNumericUpDown.Size = new System.Drawing.Size(37, 20);
            this.SchedualIntervalNumericUpDown.TabIndex = 4;
            this.ToolTip.SetToolTip(this.SchedualIntervalNumericUpDown, "Schedual interval, Unit is Day.");
            // 
            // LogResolvedcheckBox
            // 
            this.LogResolvedcheckBox.AutoSize = true;
            this.LogResolvedcheckBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LogResolvedcheckBox.Location = new System.Drawing.Point(363, 26);
            this.LogResolvedcheckBox.Name = "LogResolvedcheckBox";
            this.LogResolvedcheckBox.Size = new System.Drawing.Size(176, 17);
            this.LogResolvedcheckBox.TabIndex = 3;
            this.LogResolvedcheckBox.Text = "Log pass or known issue results";
            this.ToolTip.SetToolTip(this.LogResolvedcheckBox, "Partially log pass or pass/fail with know issue results, ingore doesn\'t resolved " +
        "results.");
            this.LogResolvedcheckBox.UseVisualStyleBackColor = true;
            // 
            // PersonalAccessTokenLabel
            // 
            this.PersonalAccessTokenLabel.AutoSize = true;
            this.PersonalAccessTokenLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.PersonalAccessTokenLabel.Location = new System.Drawing.Point(14, 109);
            this.PersonalAccessTokenLabel.Name = "PersonalAccessTokenLabel";
            this.PersonalAccessTokenLabel.Size = new System.Drawing.Size(126, 13);
            this.PersonalAccessTokenLabel.TabIndex = 2;
            this.PersonalAccessTokenLabel.Text = "Personal Access Token: ";
            this.PersonalAccessTokenLabel.UseMnemonic = false;
            // 
            // SuiteUriLabel
            // 
            this.SuiteUriLabel.AutoSize = true;
            this.SuiteUriLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SuiteUriLabel.Location = new System.Drawing.Point(14, 151);
            this.SuiteUriLabel.Name = "SuiteUriLabel";
            this.SuiteUriLabel.Size = new System.Drawing.Size(77, 13);
            this.SuiteUriLabel.TabIndex = 4;
            this.SuiteUriLabel.Text = "Test Sutie Uri: ";
            this.SuiteUriLabel.UseMnemonic = false;
            // 
            // OKPushButton
            // 
            this.OKPushButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKPushButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.OKPushButton.Location = new System.Drawing.Point(545, 241);
            this.OKPushButton.Name = "OKPushButton";
            this.OKPushButton.Size = new System.Drawing.Size(75, 23);
            this.OKPushButton.TabIndex = 7;
            this.OKPushButton.Text = "OK";
            this.OKPushButton.UseVisualStyleBackColor = true;
            this.OKPushButton.Click += new System.EventHandler(this.OKPushButton_Click);
            // 
            // CancelPushButton
            // 
            this.CancelPushButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelPushButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CancelPushButton.Location = new System.Drawing.Point(645, 241);
            this.CancelPushButton.Name = "CancelPushButton";
            this.CancelPushButton.Size = new System.Drawing.Size(75, 23);
            this.CancelPushButton.TabIndex = 8;
            this.CancelPushButton.Text = "Cancel";
            this.CancelPushButton.UseVisualStyleBackColor = true;
            this.CancelPushButton.Click += new System.EventHandler(this.CancelPushButton_Click);
            // 
            // AdditionalCommentLabel
            // 
            this.AdditionalCommentLabel.AutoSize = true;
            this.AdditionalCommentLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.AdditionalCommentLabel.Location = new System.Drawing.Point(14, 193);
            this.AdditionalCommentLabel.Name = "AdditionalCommentLabel";
            this.AdditionalCommentLabel.Size = new System.Drawing.Size(106, 13);
            this.AdditionalCommentLabel.TabIndex = 10;
            this.AdditionalCommentLabel.Text = "Additional Comment: ";
            this.AdditionalCommentLabel.UseMnemonic = false;
            // 
            // NotifiedMailSubjectLabel
            // 
            this.NotifiedMailSubjectLabel.AutoSize = true;
            this.NotifiedMailSubjectLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.NotifiedMailSubjectLabel.Location = new System.Drawing.Point(14, 65);
            this.NotifiedMailSubjectLabel.Name = "NotifiedMailSubjectLabel";
            this.NotifiedMailSubjectLabel.Size = new System.Drawing.Size(116, 13);
            this.NotifiedMailSubjectLabel.TabIndex = 12;
            this.NotifiedMailSubjectLabel.Text = "Notified Email Subject: ";
            this.NotifiedMailSubjectLabel.UseMnemonic = false;
            // 
            // SchedualLabel
            // 
            this.SchedualLabel.AutoSize = true;
            this.SchedualLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SchedualLabel.Location = new System.Drawing.Point(554, 27);
            this.SchedualLabel.Name = "SchedualLabel";
            this.SchedualLabel.Size = new System.Drawing.Size(121, 13);
            this.SchedualLabel.TabIndex = 13;
            this.SchedualLabel.Text = "Schedual Every (Days): ";
            // 
            // TaskSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 290);
            this.Controls.Add(this.LogResolvedcheckBox);
            this.Controls.Add(this.SchedualIntervalNumericUpDown);
            this.Controls.Add(this.SchedualLabel);
            this.Controls.Add(this.NotifiedMailSubjectTextBox);
            this.Controls.Add(this.NotifiedMailSubjectLabel);
            this.Controls.Add(this.AdditionalCommentTextBox);
            this.Controls.Add(this.AdditionalCommentLabel);
            this.Controls.Add(this.ActivaedCheckBox);
            this.Controls.Add(this.CancelPushButton);
            this.Controls.Add(this.OKPushButton);
            this.Controls.Add(this.TestSutieUriTextBox);
            this.Controls.Add(this.SuiteUriLabel);
            this.Controls.Add(this.PersonalAccessTokenTextBox);
            this.Controls.Add(this.PersonalAccessTokenLabel);
            this.Controls.Add(this.TaskNameTextBox);
            this.Controls.Add(this.TaskNameLabel);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TaskSettingsDialog";
            this.Text = "Config Existing Task Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskSettingsDialog_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.SchedualIntervalNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TaskNameLabel;
        private System.Windows.Forms.TextBox TaskNameTextBox;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.CheckBox ActivaedCheckBox;
        private System.Windows.Forms.Label PersonalAccessTokenLabel;
        private System.Windows.Forms.TextBox PersonalAccessTokenTextBox;
        private System.Windows.Forms.Label SuiteUriLabel;
        private System.Windows.Forms.TextBox TestSutieUriTextBox;
        private System.Windows.Forms.Button OKPushButton;
        private System.Windows.Forms.Button CancelPushButton;
        private System.Windows.Forms.Label AdditionalCommentLabel;
        private System.Windows.Forms.TextBox AdditionalCommentTextBox;
        private System.Windows.Forms.TextBox NotifiedMailSubjectTextBox;
        private System.Windows.Forms.Label NotifiedMailSubjectLabel;
        private System.Windows.Forms.Label SchedualLabel;
        private System.Windows.Forms.NumericUpDown SchedualIntervalNumericUpDown;
        private System.Windows.Forms.CheckBox LogResolvedcheckBox;
    }
}