using AspenTech.MES.LogMvtResults;
using AspenTech.MES.LogMvtResults.Domain;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspenTech.MES.LogMvtResultsTests.Unit
{
    [TestFixture]
    public class LogMvtResultsTaskSectionTest
    {

        [Test]
        public void IsTaskNameExists_CheckSameIdTask_ReturnsFalse()
        {
            var section = new LogMvtResultsTaskSection();
            var task = new LogMvtResultsTask("task1", "", "", null, false, 1); //tasks contains same id, so regards as update instead of add.
            section.TaskCollection.AddOrUpdate(task);

            Assert.IsFalse(section.IsTaskNameExists(task));
        }

        [Test]
        public void LogMvtResultsTaskSectionTest_IsTaskNameExists_AddCheckTrue()
        {
            var section = new LogMvtResultsTaskSection();
            var task = new LogMvtResultsTask("task1", "", "", null, false, 1);
            var task1 = new LogMvtResultsTask("task1", "", "", null, false, 2);
            section.TaskCollection.AddOrUpdate(task);
            Assert.IsTrue(section.IsTaskNameExists(task1));
        }

        [Test]
        public void LogMvtResultsTaskSectionTest_IsNotifiedMailSubjectExists_UpdateCheckFalse()
        {
            var section = new LogMvtResultsTaskSection();
            var task = new LogMvtResultsTask("task1", "", "", null, false, 1); //tasks contains same id, so regards as update instead of add.
            section.TaskCollection.AddOrUpdate(task);
            Assert.IsFalse(section.IsNotifiedMailSubjectExists(task));
        }

        [Test]
        public void LogMvtResultsTaskSectionTest_IsNotifiedMailSubjectExists_AddCheckTrue()
        {
            var section = new LogMvtResultsTaskSection();
            var task = new LogMvtResultsTask("task1", "", "", null, false, 1);
            var task1 = new LogMvtResultsTask("task1", "", "", null, false, 2);
            section.TaskCollection.AddOrUpdate(task);
            Assert.IsTrue(section.IsNotifiedMailSubjectExists(task1));
        }
    }
}
