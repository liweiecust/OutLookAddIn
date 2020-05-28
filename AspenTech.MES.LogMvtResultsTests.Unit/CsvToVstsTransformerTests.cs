using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using AspenTech.MES.LogMvtResults.Domain;
using NUnit.Framework;

namespace AspenTech.MES.LogMvtResultsTests.Unit
{
    [TestFixture]
    public class CsvToVstsTransformerTests
    {
        [TestCase("[Fail with defect 516000]Test Redefine Wizard on On 64-bit IP21 system whose maximum database size is one billion bytes")]
        public void FailTagRegexPattern_Match(string text)
        {
            Regex regex = CsvToVstsTransformer.FailTagRegex;
            string failId = regex.Match(text).Groups["failID"].Value;
            Assert.AreEqual("516000", failId);
        }

        [TestCase("[Perf]Check ProcessExplorer.exe process get killed after closing the application")]
        [TestCase("Check the \"Snapshots\"button in IP21 Manager")]
        public void FailTagRegexPatternRegexPattern_NotMatch(string text)
        {
            Regex regex = CsvToVstsTransformer.FailTagRegex;
            Assert.IsFalse(regex.Match(text).Success);
        }

        [TestCase("[Fail with defect 516000]Test Redefine Wizard on On 64-bit IP21 system whose maximum database size is one billion bytes")]
        [TestCase("[pass with low priority defect 516000]Test Redefine Wizard on On 64-bit IP21 system whose maximum database size is one billion bytes")]
        public void GetKnownDefect_GiveSingleDefect_RetunsDefect(string text)
        {
            var tran = new CsvToVstsTransformer();
            var failId = tran.GetKnownDefects(text);
            CollectionAssert.AreEquivalent(new List<string>{"516000"}, failId);
        }

        [TestCase("[Fail with defect 516000]Test Redefine Wizard on On 64-bit IP21 system whose maximum database size is one billion bytes")]
        [TestCase("[pass with low priority defect 516000]Test Redefine Wizard on On 64-bit IP21 system whose maximum database size is one billion bytes")]
        [TestCase("[failed with defect 516000]Test Redefine Wizard on On 64-bit IP21 system whose maximum database size is one billion bytes")]
        [TestCase("[PASSED with low priority defect 516000]Test Redefine Wizard on On 64-bit IP21 system whose maximum database size is one billion bytes")]
        public void GetKnownDefect_ByDefault_CaseInsensitive(string text)
        {
            var tran = new CsvToVstsTransformer();
            var failId = tran.GetKnownDefects(text);
            CollectionAssert.AreEquivalent(new List<string> { "516000" }, failId);
        }

        [TestCase("[Fail with defect 516000|52300]Test Redefine Wizard on On 64-bit IP21 system whose maximum database size is one billion bytes")]
        public void GetKnownDefect_Give2Defects_Returns2Defects(string text)
        {
            var tran = new CsvToVstsTransformer();
            var failId = tran.GetKnownDefects(text);
            CollectionAssert.AreEquivalent(new List<string> { "516000", "52300" }, failId);
        }

        [TestCase("[Perf]Check ProcessExplorer.exe process get killed after closing the application")]
        [TestCase("Check the \"Snapshots\"button in IP21 Manager")]
        public void GetKnownDefect_GiveNoDefect_RetunsNull(string text)
        {
            var tran = new CsvToVstsTransformer();
            var failId = tran.GetKnownDefects(text);
            Assert.IsNull(failId);
        }

        [TestCase]
        public void CsvToVsts_NoDefect_ReturnsVsts()
        {
            var raw = new CsvResultItem { Id = "VSTS468797", Description = "Check a definition record's PUBLISH_UPDATES field attribute", Result = "PASS", Note = "Aspen-V12-MSC-Media319.iso" };
            var tran = new CsvToVstsTransformer();
            var res = tran.CsvToVsts(raw);
            var expect = new VstsTestCaseResult { Id = "468797", Description = "Check a definition record's PUBLISH_UPDATES field attribute", Outcome = "passed", Comment = "Media319" };
            Assert.AreEqual(expect, res);
        }

        [TestCase]
        public void CsvToVsts_NoDefect_ReturnsPassed()
        {
            var raw = new CsvResultItem { Id = "VSTS468797", Description = "Check a definition record's PUBLISH_UPDATES field attribute", Result = "PASS", Note = "Aspen-V12-MSC-Media319.iso" };
            var tran = new CsvToVstsTransformer();
            var res = tran.CsvToVsts(raw);
            var expect = new VstsTestCaseResult { Id = "468797", Description = "Check a definition record's PUBLISH_UPDATES field attribute", Outcome = "passed", Comment = "Media319" };
            Assert.AreEqual(expect.Outcome, res.Outcome);
        }

        [TestCase("VSTS468797")]
        [TestCase("vStS468797")]
        public void CsvToVsts_NoDefect_ReturnsId(string id)
        {
            var raw = new CsvResultItem { Id = "VSTS468797", Description = "Check a definition record's PUBLISH_UPDATES field attribute", Result = "PASS", Note = "Aspen-V12-MSC-Media319.iso" };
            var tran = new CsvToVstsTransformer();
            var res = tran.CsvToVsts(raw);
            var expect = new VstsTestCaseResult { Id = "468797", Description = "Check a definition record's PUBLISH_UPDATES field attribute", Outcome = "passed", Comment = "Media319" };
            Assert.AreEqual(expect.Id, res.Id );
        }

        [TestCase]
        public void CsvToVsts_NoDefect_ReturnsRawDescription()
        {
            var raw = new CsvResultItem { Id = "VSTS468797", Description = "Check a definition record's PUBLISH_UPDATES field attribute", Result = "PASS", Note = "Aspen-V12-MSC-Media319.iso" };
            var tran = new CsvToVstsTransformer();
            var res = tran.CsvToVsts(raw);
            var expect = new VstsTestCaseResult { Id = "468797", Description = "Check a definition record's PUBLISH_UPDATES field attribute", Outcome = "passed", Comment = "Media319" };
            Assert.AreEqual(expect.Description, res.Description);
        }

        [TestCase]
        public void CsvToVsts_NoDefect_ReturnsMedia()
        {
            var raw = new CsvResultItem { Id = "VSTS468797", Description = "Check a definition record's PUBLISH_UPDATES field attribute", Result = "PASS", Note = "Aspen-V12-MSC-Media319.iso" };
            var tran = new CsvToVstsTransformer();
            var res = tran.CsvToVsts(raw);
            var expect = new VstsTestCaseResult { Id = "468797", Description = "Check a definition record's PUBLISH_UPDATES field attribute", Outcome = "passed", Comment = "Media319" };
            Assert.AreEqual(expect.Comment, res.Comment);
        }

        [TestCase]
        public void CsvToVsts_OneDefect_ReturnsFailedWithDefect()
        {
            var raw = new CsvResultItem { Id = "VSTS468797", Description = "[Fail with defect 516000|52300]Check a definition record's PUBLISH_UPDATES field attribute", Result = "TBD", Note = "Aspen-V12-MSC-Media319.iso" };
            var tran = new CsvToVstsTransformer();
            var res = tran.CsvToVsts(raw);
            var expect = new VstsTestCaseResult { Id = "468797", Description = "Check a definition record's PUBLISH_UPDATES field attribute", Outcome = "failed", Comment = "Media319" };
            Assert.AreEqual(expect.Outcome,res.Outcome );
            CollectionAssert.AreEquivalent(new List<string> { "516000", "52300" }, res.AssociatedBugs);
        }
    }
}
