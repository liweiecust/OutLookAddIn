using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspenTech.MES.LogMvtResults.Domain
{
    public class LogMvtResultsTask : ConfigurationElement
    {
        public LogMvtResultsTask()
        {
        }

        public LogMvtResultsTask(string taskName, string fullTestSuiteUrl, string personalAccessToken, string additionalComment = null, bool activated = false, int id = -1)
        {
            Id = id;
            TaskName = taskName;
            FullTestSuiteUri = fullTestSuiteUrl;
            PersonalAccessToken = personalAccessToken;
            this.Activated = activated;
            this.AdditionalComment = additionalComment;
        }

        [ConfigurationProperty(nameof(Id), IsRequired = true, IsKey = true)]
        public int Id
        {
            get
            {
                return (int)this[nameof(Id)];
            }
            set
            {
                this[nameof(Id)] = value;
            }
        }

        [ConfigurationProperty(nameof(TaskName), IsRequired = true)]
        public string TaskName
        {
            get
            {
                return (string)this[nameof(TaskName)];
            }
            set
            {
                this[nameof(TaskName)] = value;
            }
        }


        [ConfigurationProperty(nameof(NotifiedMailSubject), IsRequired = true, IsKey = true)]
        public string NotifiedMailSubject
        {
            get
            {
                return (string)this[nameof(NotifiedMailSubject)];
            }
            set
            {
                this[nameof(NotifiedMailSubject)] = value;
            }
        }

        [ConfigurationProperty(nameof(FullTestSuiteUri), IsRequired = true)]
        //[RegexStringValidator(@"\w+:\/\/[\w.]+\S*")]
        public string FullTestSuiteUri
        {
            get
            {
                return (string)this[nameof(FullTestSuiteUri)];
            }
            set
            {
                this[nameof(FullTestSuiteUri)] = value;
            }
        }

        public bool IsValidTestSuiteUri()
        {
            try
            {
                var orgUri = this.OrganizationUri;
                var projectName = this.ProjectName;
                var planId = this.PlanId;
                var suiteId = this.SuiteId;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [ConfigurationProperty(nameof(PersonalAccessToken), IsRequired = true)]
        public string PersonalAccessToken
        {
            get
            {
                return (string)this[nameof(PersonalAccessToken)];
            }
            set
            {
                this[nameof(PersonalAccessToken)] = value;
            }
        }

        [ConfigurationProperty(nameof(Activated), IsRequired = true)]
        public bool Activated
        {
            get
            {
                return (bool)this[nameof(Activated)];
            }
            set
            {
                this[nameof(Activated)] = value;
            }
        }

        [ConfigurationProperty(nameof(AdditionalComment), IsRequired = true)]
        public string AdditionalComment
        {
            get
            {
                return (string)this[nameof(AdditionalComment)];
            }
            set
            {
                this[nameof(AdditionalComment)] = value;
            }
        }

        [ConfigurationProperty(nameof(LastLogDate), IsRequired = true, DefaultValue = "2020-01-01 00:00:00")]
        public string LastLogDate
        {
            get
            {
                return (string)this[nameof(LastLogDate)];
            }
            set
            {
                this[nameof(LastLogDate)] = value;
            }
        }

        [ConfigurationProperty(nameof(SchedualInterval), IsRequired = true, DefaultValue = 7)]
        public int SchedualInterval
        {
            get
            {
                return (int)this[nameof(SchedualInterval)];
            }
            set
            {
                this[nameof(SchedualInterval)] = value;
            }
        }

        [ConfigurationProperty(nameof(LogPassOrKnownIssue), IsRequired = true, DefaultValue = true)]
        public bool LogPassOrKnownIssue
        {
            get
            {
                return (bool)this[nameof(LogPassOrKnownIssue)];
            }
            set
            {
                this[nameof(LogPassOrKnownIssue)] = value;
            }
        }

        public string ResultFilePath { get; set; }

        public string OrganizationUri
        {
            get
            {
                var OrganizationUrlRegex = new Regex(@"(?<OrganizationUrl>https://\S+\.visualstudio\.com)/|(?<OrganizationUrl>https://dev.azure.com/\S+?)/", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                var value = OrganizationUrlRegex.Match(FullTestSuiteUri).Groups["OrganizationUrl"].Value;
                if (string.IsNullOrEmpty(value)) throw new ConfigurationErrorsException("Test suite Uri is not correct, Organization Uri is not found.");
                return value;
            }
        }

        public string ProjectName
        {
            get
            {
                var uriSubOrganizationUrl = FullTestSuiteUri.Replace(OrganizationUri, string.Empty).Replace("%20", " ");
                var ProjectNameRegex = new Regex(@"/(?<ProjectName>.+?)/", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                var value = ProjectNameRegex.Match(uriSubOrganizationUrl).Groups["ProjectName"].Value;
                if (string.IsNullOrEmpty(value)) throw new ConfigurationErrorsException("Test suite Uri is not correct, Project Name is not found.");
                return value;
            }
        }

        public int PlanId
        {
            get
            {
                var uri = new Uri(FullTestSuiteUri);
                var planIdRegex = new Regex(@"&planId=(?<planId>\d+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                return Convert.ToInt32(planIdRegex.Match(uri.Query).Groups["planId"].Value);
            }
        }

        public int SuiteId
        {
            get
            {
                var uri = new Uri(FullTestSuiteUri);
                var suiteIdRegex = new Regex(@"&suiteId=(?<suiteId>\d+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                return Convert.ToInt32(suiteIdRegex.Match(uri.Query).Groups["suiteId"].Value);
            }
        }
    }
}
