using AspenTech.MES.LogMvtResults;
using AspenTech.MES.LogMvtResults.Domain;
using Microsoft.VisualBasic.FileIO;
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
    public class MiscTests
    {
        [Test]
        public void Misc_OfType_ReturnNotNull()
        {
            var t = new ArrayList();
            t.Add(1);
            t.Add("1");
            Assert.IsNotNull(t.OfType<LogMvtResultsTask>());
        }

        [Test]
        public void Misc_OneWeek_Test()
        {
            var previous = DateTime.Parse("2020-04-27 00:00:00");
            var last = DateTime.Parse("2020-05-04 00:00:00");
            var t = last - previous;
            Assert.AreEqual(7, t.Days);
        }

        [Test]
        public void Misc_FileExists_Test()
        {
            var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ExecutionResult.csv");
            using (TextFieldParser csvReader = new TextFieldParser(filePath))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;
                csvReader.ReadFields();
                csvReader.ReadFields();
                Assert.IsTrue(File.Exists(filePath));
            }

        }

        ///// <summary>
        ///// Mark the test run as completed 
        ///// </summary>
        //public async Task EndTestRunAsync(TestRunData testRunData, int testRunId, bool publishAttachmentsAsArchive = false, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    Trace.Entering();
        //    RunUpdateModel updateModel = new RunUpdateModel(
        //        completedDate: testRunData.CompleteDate,
        //        state: TestRunState.Completed.ToString()
        //        );
        //    TestRun testRun = await _testResultsServer.UpdateTestRunAsync(_projectName, testRunId, updateModel, cancellationToken);

        //    // Uploading run level attachments, only after run is marked completed;
        //    // so as to make sure that any server jobs that acts on the uploaded data (like CoverAn job does for Coverage files)  
        //    // have a fully published test run results, in case it wants to iterate over results 

        //    if (publishAttachmentsAsArchive)
        //    {
        //        await UploadTestRunAttachmentsAsArchiveAsync(testRunId, testRunData.Attachments, cancellationToken);
        //    }
        //    else
        //    {
        //        await UploadTestRunAttachmentsIndividualAsync(testRunId, testRunData.Attachments, cancellationToken);
        //    }

        //    _executionContext.Output(string.Format(CultureInfo.CurrentCulture, "Published Test Run : {0}", testRun.WebAccessUrl));
        //}



        //public TestRunPublisherTests()
        //{
        //    _attachmentFilePath = "attachment.txt";

        //    File.WriteAllText(_attachmentFilePath, "asdf");
        //    _testRunContext = new TestRunContext("owner", "platform", "config", 1, "builduri", "releaseuri", "releaseenvuri");

        //    _reader = new Mock<IResultReader>();
        //    _reader.Setup(x => x.ReadResults(It.IsAny<IExecutionContext>(), It.IsAny<string>(), It.IsAny<TestRunContext>()))
        //                .Callback<IExecutionContext, string, TestRunContext>
        //                ((executionContext, filePath, runContext) =>
        //                {
        //                    _runContext = runContext;
        //                    _resultsFilepath = filePath;
        //                })
        //                .Returns((IExecutionContext executionContext, string filePath, TestRunContext runContext) =>
        //                {
        //                    TestRunData trd = new TestRunData(
        //                        name: "xyz",
        //                        buildId: runContext.BuildId,
        //                        completedDate: "",
        //                        state: "InProgress",
        //                        isAutomated: true,
        //                        dueDate: "",
        //                        type: "",
        //                        buildFlavor: runContext.Configuration,
        //                        buildPlatform: runContext.Platform,
        //                        releaseUri: runContext.ReleaseUri,
        //                        releaseEnvironmentUri: runContext.ReleaseEnvironmentUri
        //                    );
        //                    trd.Attachments = new string[] { "attachment.txt" };
        //                    return trd;
        //                });

        //    _testResultServer = new Mock<ITestResultsServer>();
        //    _testResultServer.Setup(x => x.InitializeServer(It.IsAny<Client.VssConnection>()));
        //    _testResultServer.Setup(x => x.AddTestResultsToTestRunAsync(It.IsAny<TestResultCreateModel[]>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
        //                .Callback<TestResultCreateModel[], string, int, CancellationToken>
        //                ((currentBatch, projectName, testRunId, cancellationToken) =>
        //                {
        //                    _batchSizes.Add(currentBatch.Length);
        //                    _resultCreateModels = currentBatch;
        //                })
        //                .Returns(() =>
        //                {
        //                    List<TestCaseResult> resultsList = new List<TestCaseResult>();
        //                    int i = 0;
        //                    foreach (TestResultCreateModel resultCreateModel in _resultCreateModels)
        //                    {
        //                        resultsList.Add(new TestCaseResult() { Id = ++i });
        //                    }
        //                    return Task.FromResult(resultsList);
        //                });

        //    _testResultServer.Setup(x => x.CreateTestRunAsync(It.IsAny<string>(), It.IsAny<RunCreateModel>(), It.IsAny<CancellationToken>()))
        //                .Callback<string, RunCreateModel, CancellationToken>
        //                ((projectName, testRunData, cancellationToken) =>
        //                {
        //                    _projectId = projectName;
        //                    _testRun = (TestRunData)testRunData;
        //                })
        //                .Returns(Task.FromResult(new TestRun() { Name = "TestRun", Id = 1 }));

        //    _testResultServer.Setup(x => x.UpdateTestRunAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<RunUpdateModel>(), It.IsAny<CancellationToken>()))
        //                .Callback<string, int, RunUpdateModel, CancellationToken>
        //                ((projectName, testRunId, updateModel, cancellationToken) =>
        //                {
        //                    _runId = testRunId;
        //                    _projectId = projectName;
        //                    _updateProperties = updateModel;
        //                })
        //                .Returns(Task.FromResult(new TestRun() { Name = "TestRun", Id = 1 }));

        //    _testResultServer.Setup(x => x.CreateTestRunAttachmentAsync(
        //                It.IsAny<TestAttachmentRequestModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
        //                .Callback<TestAttachmentRequestModel, string, int, CancellationToken>
        //                ((reqModel, projectName, testRunId, cancellationToken) =>
        //                {
        //                    _attachmentRequestModel = reqModel;
        //                    _projectId = projectName;
        //                    _runId = testRunId;
        //                })
        //                .Returns(Task.FromResult(new TestAttachmentReference()));

        //    _testResultServer.Setup(x => x.CreateTestResultAttachmentAsync(It.IsAny<TestAttachmentRequestModel>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
        //                .Callback<TestAttachmentRequestModel, string, int, int, CancellationToken>
        //                ((reqModel, projectName, testRunId, testCaseResultId, cancellationToken) =>
        //                {
        //                    if (_resultsLevelAttachments.ContainsKey(testCaseResultId))
        //                    {
        //                        _resultsLevelAttachments[testCaseResultId].Add(reqModel);
        //                    }
        //                    else
        //                    {
        //                        _resultsLevelAttachments.Add(testCaseResultId, new List<TestAttachmentRequestModel>() { reqModel });
        //                    }
        //                })
        //                .Returns(Task.FromResult(new TestAttachmentReference()));
        //}
    }
}
