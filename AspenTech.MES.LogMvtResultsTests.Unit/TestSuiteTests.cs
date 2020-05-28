using AspenTech.MES.LogMvtResults;
using AspenTech.MES.LogMvtResults.Domain;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspenTech.MES.LogMvtResultsTests.Unit
{
    [TestFixture]
    public class TestSuiteTests
    {
        //https://aspentech-alm.visualstudio.com/AspenTech%20Sandbox/_testManagement?_a=tests&Length=2&planId=4308&suiteId=4354
        const String azureDevOpsOrganizationUrl = "https://dev.azure.com/aspentech-alm";
        const String projectName = "AspenTech Sandbox";
        const string personalaccesstoken = ""; //Peter
        const int planId = 4308;
        const int suiteId = 4354;// 4354;

        [Test]
        public void TestSuite_GetAllTest_Correct()
        {
            var orgUri = new Uri(azureDevOpsOrganizationUrl);

            VssCredentials credentials = new VssCredentials(new Microsoft.VisualStudio.Services.Common.VssBasicCredential(string.Empty, personalaccesstoken));
            var connection = new VssConnection(orgUri, credentials);


            var testClient = connection.GetClient<TestManagementHttpClient>();
            List<SuiteTestCase> testCases = testClient.GetTestCasesAsync(projectName, planId, suiteId).Result;
            Assert.AreEqual(15, testCases.Count);

            //int testpointid = 682;
            //var testResults = await testClient.GetTestResultsAsync(teamProject, 1169628);// & resultId = 100000 //4308, 4354, 423);
            //foreach (var item in testResults)
            //{
            //    Console.WriteLine(item.TestCase.Id +" " + item.TestPoint.Id +" " + item.Outcome);
            //}
            //var runGet = await testClient.GetTestRunByIdAsync("AspenTech Sandbox", 1169710);
           
        }

        [Test]
        [Ignore("Will update SandBox project")]
        public void TestSuite_LogResult_OK()
        {
            VstsSuite suite = new VstsSuite(personalAccessToken: personalaccesstoken);
            suite.PopulateTestCases().Wait();
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(exePath, "ExecutionResult.csv");
            var parser = new CsvResultsParser(filePath);
            var results = parser.Parse();
            var tran = new CsvToVstsTransformer();
            var res = tran.CsvListToVstsList(results, string.Empty);
            var loggedCases = suite.LogResults(res).Result;
            var ExpectedCases = new List<string>
            {
                "423",
                "2069",
                "2188"
            };
            CollectionAssert.AreEquivalent(ExpectedCases, loggedCases);
        }

        [Test]
        [Ignore("Will update SandBox project")]
        public void TestSuite_PartiallyLogResult_OK()
        {
            VstsSuite suite = new VstsSuite(personalAccessToken: personalaccesstoken); ;
            suite.PopulateTestCases().Wait();
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(exePath, "ExecutionResult_PartiallyLog.csv");
            var parser = new CsvResultsParser(filePath);
            var results = parser.Parse();
            var tran = new CsvToVstsTransformer();
            var res = tran.CsvListToVstsList(results, string.Empty, true);
            var loggedCases = suite.LogResults(res).Result;
            var ExpectedCases = new List<string>
            {
                "423",
                "2188"
            };
            CollectionAssert.AreEquivalent(ExpectedCases, loggedCases);
        }

        [Test]
        //[Ignore("debug")]
        public void GenerateNotLoggedHtmlContent_PartiallyLogResult_OK()
        {
            VstsSuite suite = new VstsSuite(personalAccessToken: personalaccesstoken); ;
            suite.PopulateTestCases().Wait();
            string exePath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
            string filePath = Path.Combine(exePath, "ExecutionResult.csv");
            var parser = new CsvResultsParser(filePath);
            var results = parser.Parse();
            var tran = new CsvToVstsTransformer();
            var res = tran.CsvListToVstsList(results, string.Empty, true);
            var NotLogged = new List<CsvResultItem>();
            var logged = new List<string>
            {
                "423",
                "2188"
            };
            foreach (var item in results)
            {
                if (!logged.Any(p => item.Id.EndsWith(p)))
                    NotLogged.Add(item);
            }
            string contents = LogResultsTaskController.GenerateNotLoggedHtmlContent(NotLogged, results.Count, "32bit_IP21Server");
            //var loggedCases = suite.LogResults(res).Result;
            File.WriteAllText(Path.Combine(exePath, "NotLogged.html"), contents);
            
        }
    }
}
