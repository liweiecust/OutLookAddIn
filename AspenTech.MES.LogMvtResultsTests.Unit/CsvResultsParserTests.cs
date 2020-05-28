using System;
using System.IO;
using System.Reflection;
using AspenTech.MES.LogMvtResults.Domain;
using NUnit.Framework;

namespace AspenTech.MES.LogMvtResultsTests.Unit
{
    [TestFixture]
    public class CsvResultsParserTests
    {
        [TestCase]
        public void Parse_CsvContains3ResultItems_ReturnsCount3()
        {
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(exePath, "ExecutionResult.csv");
            var parser = new CsvResultsParser(filePath);
            var results = parser.Parse();
            Assert.AreEqual(3, results.Count);
        }

        [TestCase]
        public void Parse_ParseSingleItem_ReturnsCorrectItem()
        {
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(exePath, "ExecutionResult.csv");
            var parser = new CsvResultsParser(filePath);
            var results = parser.Parse();
            var expect = new CsvResultItem { Id = "VSTS423", Description = "test", Result = "PASS", Note = "Aspen-V12-MSC-Media319.iso" };
            Assert.AreEqual(expect.Id, results[0].Id);
            Assert.AreEqual(expect.Description, results[0].Description);
            Assert.AreEqual(expect.Result, results[0].Result);
            Assert.AreEqual(expect.Note, results[0].Note);
        }
    }
}
