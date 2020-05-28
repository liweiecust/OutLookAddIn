using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using AspenTech.MES.LogMvtResults;

namespace AspenTech.MES.LogMvtResultsTests.Unit
{
    [TestFixture]
    [Ignore("Obsolete")]
    public class MailItemProcessorTests
    {
        [TestCase("QE - V12 - IP21 Automated Test report for Mvt")]
        [TestCase("QE - V12 - IP21 32bit Automated Test report for Mvt")]
        public void MailItemProcessor_ResultsAvailableMailTitleRegexPattern_Match(string text)
        {
            //Regex regex = MailItemProcessor.ResultsAvailableMailTitleRegexPattern;
            //Assert.IsTrue(regex.Match(text).Success);
        }
    }
}
