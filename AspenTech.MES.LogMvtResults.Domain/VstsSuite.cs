using Microsoft.TeamFoundation.TestManagement.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspenTech.MES.LogMvtResults.Domain
{
    public class VstsSuite
    {
        //const String azureDevOpsOrganizationUrl = "https://dev.azure.com/aspentech-alm";
        //const String projectName = "AspenTech Sandbox";
        //const string personalaccesstoken = ""; //Peter
        //const int planId = 4308;
        //const int suiteId = 4354;


        public VstsSuite(string OrganizationUrl = "https://dev.azure.com/aspentech-alm", string projectName = "AspenTech Sandbox", int planId = 4308, int suiteId = 4354, string personalAccessToken= "")
        {
            this.OrganizationUrl = OrganizationUrl;
            this.ProjectName = projectName;
            this.PlanId = planId;
            this.SuiteId = suiteId;
            this.Personalaccesstoken = personalAccessToken;
        }

        public VstsSuite(LogMvtResultsTask task)
            : this(task.OrganizationUri, task.ProjectName, task.PlanId, task.SuiteId, task.PersonalAccessToken)
        { }

        public int GenerateTestResultID()
        {
            return _startTestCaseResultID++;
        }

        public bool IsSingleConfiguration()
        {
            return _testCases.All(p => p.PointAssignments.Count == 1);
        }

        public async Task PopulateTestCases()
        {
            var orgUri = new Uri(OrganizationUrl);

            VssCredentials credentials = new VssCredentials(new Microsoft.VisualStudio.Services.Common.VssBasicCredential(string.Empty, Personalaccesstoken));
            using (var connection = new VssConnection(orgUri, credentials))
            {

                var testClient = connection.GetClient<TestManagementHttpClient>();
                _testCases = await testClient.GetTestCasesAsync(ProjectName, PlanId, SuiteId);
                var testPoints = await testClient.GetPointsAsync(ProjectName, PlanId, SuiteId);

                //_testCases = testClient.GetTestCasesAsync(ProjectName, PlanId, SuiteId).Result;
                //var testPoints = testClient.GetPointsAsync(ProjectName, PlanId, SuiteId).Result;

                //var t1 = testClient.GetTestCasesAsync(ProjectName, PlanId, SuiteId);
                //var t2 = testClient.GetPointsAsync(ProjectName, PlanId, SuiteId);
                //await Task.WhenAll(t1, t2);
                //_testCases = t1.Result;
                //var testPoints = t2.Result;

                for (int i = 0; i < testPoints.Count; i++)
                {
                    _testCaseToPointMap.Add(testPoints[i].TestCase.Id, testPoints[i].Id);
                }

                for (int i = 0; i < testPoints.Count; i++)
                {
                    _testPointToCaseMap.Add(testPoints[i].Id, testPoints[i].TestCase.Id);
                }
            }
        }

        public async Task<List<string>> LogResults(List<VstsTestCaseResult> results)
        {
            var orgUri = new Uri(OrganizationUrl);

            VssCredentials credentials = new VssCredentials(new Microsoft.VisualStudio.Services.Common.VssBasicCredential(string.Empty, Personalaccesstoken));
            var connection = new VssConnection(orgUri, credentials);

            using (var testClient = connection.GetClient<TestManagementHttpClient>())
            {
                
                var resultPointIds = results.Where(p=>_testCaseToPointMap.Keys.Contains(p.Id)).Select(p => GetPointIdByCaseId(p.Id)).ToArray();

                List<TestCaseResult> testcaseResults = new List<TestCaseResult>();
                foreach (var pointId in resultPointIds)
                {
                    TestCaseResult temp = new TestCaseResult() { State = "Completed" };
                    temp.Id = GenerateTestResultID();
                    temp.TestPoint = new ShallowReference(pointId.ToString());
                    var current = results.Where(p => p.Id == GetCaseIdByPointId(pointId)).First();
                    temp.Outcome = current.Outcome;
                    if (current.AssociatedBugs != null)
                    {

                        var tempDefects = new List<ShallowReference>();
                        foreach (var defect in current.AssociatedBugs)
                        {
                            tempDefects.Add(new ShallowReference(defect));
                        }
                        temp.AssociatedBugs = tempDefects;
                    }
                    temp.Comment = current.Comment;
                    testcaseResults.Add(temp);
                }

                RunCreateModel run = new RunCreateModel(name: "Mvt", plan: new ShallowReference(PlanId.ToString()), pointIds: resultPointIds);
                TestRun testrun = await testClient.CreateTestRunAsync(run, ProjectName);
                var testResults = await testClient.UpdateTestResultsAsync(testcaseResults.ToArray(), ProjectName, testrun.Id);
                RunUpdateModel runmodel = new RunUpdateModel(state: "Completed", deleteUnexecutedResults: true);//, deleteUnexecutedResults: true);
                TestRun testRunResult = await testClient.UpdateTestRunAsync(runmodel, ProjectName, testrun.Id);//, runmodel);
                var loggedCases =  await testClient.GetTestResultsAsync(ProjectName, testRunResult.Id);
                return loggedCases.Select(p => p.TestCase.Id).ToList();
                //TestRunState.Completed;
                //TestRunOutcome.Passed.ToString();
            }
        }

        public int GetPointIdByCaseId(string testCaseId)
        {
            return _testCaseToPointMap[testCaseId];
        }

        public string GetCaseIdByPointId(int testPointId)
        {
            return _testPointToCaseMap[testPointId];
        }

        public List<string> TestCasesIds => _testCases.Select(p => p.Workitem.Id).ToList();

        private List<SuiteTestCase> _testCases;

        private Dictionary<string, int> _testCaseToPointMap = new Dictionary<string, int>();

        private Dictionary<int, string> _testPointToCaseMap = new Dictionary<int, string>();

        private int _startTestCaseResultID = 100000;

        public string OrganizationUrl { get; private set; }

        public string ProjectName { get; private set; }

        public int PlanId { get; private set; }

        public int SuiteId { get; private set; }

        public string Personalaccesstoken { get; private set; }
    }
}
