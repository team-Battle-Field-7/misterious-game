using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleField7Namespace.NewGameDesign.UIClasses;

namespace BattleField7UnitTests
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TestWrongFileLog()
        {
            var logger = new Logger();
            logger.FilePath = "log.html";
        }

        [TestMethod]
        public void TestLogFileExists()
        {
            var logger = new Logger();
            logger.Log("testing!");
            Assert.IsTrue(System.IO.File.Exists(logger.FilePath), "No log file created!");
            Assert.IsTrue(System.IO.File.ReadAllText(logger.FilePath).Length > 0, "Log file was created (" + logger.FilePath + ") but nothing was written inside.");
        }
    }
}
