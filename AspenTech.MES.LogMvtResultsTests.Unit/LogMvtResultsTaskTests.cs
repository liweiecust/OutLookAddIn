using AspenTech.MES.LogMvtResults;
using AspenTech.MES.LogMvtResults.Domain;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AspenTech.MES.LogMvtResultsTests.Unit
{
    [TestFixture]
    public class LogMvtResultsTaskTests
    {
        [TestCase("https://aspentech-alm.visualstudio.com/AspenTech/_testManagement?_a=tests&Length=2&planId=415999&suiteId=515999")]
        public void OrganizationUrl_ValidVSStyleSuiteUri_ReturnsOrganizationUri(string suiteUri)
        {
            var task = new LogMvtResultsTask("task1", suiteUri, "");
            Assert.AreEqual("https://aspentech-alm.visualstudio.com", task.OrganizationUri);
        }

        [TestCase("https://dev.azure.com/aspentech-alm/AspenTech/_testManagement?_a=tests&Length=2&planId=415999&suiteId=515999")]
        public void OrganizationUrl_ValidAzureStyleSuiteUri_ReturnsOrganizationUri(string suiteUri)
        {
            var task = new LogMvtResultsTask("task1", suiteUri, "");
            Assert.AreEqual("https://dev.azure.com/aspentech-alm", task.OrganizationUri);
        }

        [TestCase("https://dev.azure.com/aspentech-alm/AspenTech/_testManagement?_a=tests&Length=2&planId=415999&suiteId=515999")]
        [TestCase("https://aspentech-alm.visualstudio.com/AspenTech/_testManagement?_a=tests&Length=2&planId=415999&suiteId=515999")]
        public void ProjectName_ValidTestSuiteUriWithoutEmbeddedWhiteSpace_ReturnsProjectName(string suiteUri)
        {
            var task = new LogMvtResultsTask("task1", suiteUri, "");
            Assert.AreEqual("AspenTech", task.ProjectName);
        }
        [TestCase("https://dev.azure.com/aspentech-alm/AspenTech%20Sandbox/_testManagement?_a=tests&Length=2&planId=415999&suiteId=515999")]
        [TestCase("https://dev.azure.com/aspentech-alm/AspenTech Sandbox/_testManagement?_a=tests&Length=2&planId=415999&suiteId=515999")]
        public void ProjectName_ValidTestSuiteUriWithEmbeddedRawOrEscapedWhiteSpace_ReturnsProjectName(string suiteUri)
        {
            var task = new LogMvtResultsTask("task1", suiteUri, "");
            Assert.AreEqual("AspenTech Sandbox", task.ProjectName);
        }

        [TestCase("https://dev.azure.com/aspentech-alm/AspenTech/_testManagement?_a=tests&Length=2&planId=415999&suiteId=515999")]
        public void PlanId_ValidTestSuiteUriWithoutWhiteSpace_ReturnsPlanId(string suiteUri)
        {
            var task = new LogMvtResultsTask("task1", suiteUri, "");
            Assert.AreEqual(415999, task.PlanId);
        }

        [TestCase("https://dev.azure.com/aspentech-alm/AspenTech%20Sandbox/_testManagement?_a=tests&Length=2&planId=415999&suiteId=515999")]
        [TestCase("https://dev.azure.com/aspentech-alm/AspenTech Sandbox/_testManagement?_a=tests&Length=2&planId=415999&suiteId=515999")]
        public void PlanId_ValidTestSuiteUriWithEmbeddedRawOrEscapedWhiteSpace_ReturnsPlanId(string suiteUri)
        {
            var task = new LogMvtResultsTask("task1", suiteUri, "");
            Assert.AreEqual(415999, task.PlanId);
        }

        [TestCase("https://dev.azure.com/aspentech-alm/AspenTech/_testManagement?_a=tests&Length=2&planId=415999&suiteId=515999")]
        public void SuiteId_ValidTestSuiteUriWithoutWhiteSpace_ReturnsSuiteId(string suiteUri)
        {
            var task = new LogMvtResultsTask("task1", suiteUri, "");
            Assert.AreEqual(515999, task.SuiteId);
        }

        [TestCase("https://dev.azure.com/aspentech-alm/AspenTech%20Sandbox/_testManagement?_a=tests&Length=2&planId=415999&suiteId=515999")]
        [TestCase("https://dev.azure.com/aspentech-alm/AspenTech Sandbox/_testManagement?_a=tests&Length=2&planId=415999&suiteId=515999")]
        public void SuiteId_ValidTestSuiteUriWithEmbeddedRawOrEscapedWhiteSpace_ReturnsSuiteId(string suiteUri)
        {
            var task = new LogMvtResultsTask("task1", suiteUri, "");
            Assert.AreEqual(515999, task.SuiteId);
        }

        [TestCase("https://dev.azure.com/")]
        public void SuiteId_InvalideSuiteUri_ThrowsFormatException(string suiteUri)
        {
            var task = new LogMvtResultsTask("task1", suiteUri, "");
            Assert.Catch<FormatException>(()=> { var i = task.SuiteId; });
        }

        [TestCase("https://dev.azure.com/")]
        public void PlanId_InvalideSuiteUri_ThrowsFormatException(string suiteUri)
        {
            var task = new LogMvtResultsTask("task1", suiteUri, "");
            Assert.Catch<FormatException>(() => { var i = task.PlanId; });
        }

        [TestCase("https://dev.azure.com/aspentech-alm/")]
        public void ProjectName_InvalideSuiteUri_ThrowsConfigurationErrorsException(string suiteUri)
        {
            var task = new LogMvtResultsTask("task1", suiteUri, "");
            var ex = Assert.Catch<ConfigurationErrorsException>(() => { var i = task.ProjectName; });
            Assert.That(ex.Message == "Test suite Uri is not correct, Project Name is not found.");
        }

        [TestCase("https://dev.azure.com/")]
        public void OrganizationUri_InvalideSuiteUri_ThrowsConfigurationErrorsException(string suiteUri)
        {
            var task = new LogMvtResultsTask("task1", suiteUri, "");
            var ex = Assert.Catch<ConfigurationErrorsException>(() => { var i = task.OrganizationUri; });
            Assert.That(ex.Message == "Test suite Uri is not correct, Organization Uri is not found.");
        }

        [TestCase("https://dev.azure.com/")]
        [TestCase("https://dev.azure.com/aspentech-alm/")]
        [TestCase("https://dev.azure.com/aspentech-alm/AspenTech")]
        [TestCase("https://dev.azure.com/aspentech-alm/AspenTech/_testManagement?_a=tests&Length=2&planId=415999")]
        public void IsValidTestSuiteUri_InvalidSuiteUri_ReturnsFalse(string suiteUri)
        {
            var task = new LogMvtResultsTask("task1", suiteUri, "");
            Assert.IsFalse(task.IsValidTestSuiteUri());
        }

        [TestCase("https://dev.azure.com/aspentech-alm/AspenTech/_testManagement?_a=tests&Length=2&planId=415999&suiteId=515999")]
        public void LIsValidTestSuiteUri_ValidSuiteUri_ReturnsTrue(string suiteUri)
        {
            var task = new LogMvtResultsTask("task1", suiteUri, "");
            Assert.IsTrue(task.IsValidTestSuiteUri());
        }
    }
}
