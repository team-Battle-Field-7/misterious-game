using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleField7Namespace.NewGameDesign.GameClasses;

namespace BattleField7UnitTests.NewGameDesignTests
{
    [TestClass]
    public class SimpleExplosionStrategyTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGetCoordsToDetonateByTheBlastBadPower1()
        {
            var es = new SimpleExplosionStrategy();
            es.GetCoordsToDetonateByTheBlast(2, 2, 6);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGetCoordsToDetonateByTheBlastBadPower2()
        {
            var es = new SimpleExplosionStrategy();
            es.GetCoordsToDetonateByTheBlast(2, 2, -1);
        }

        [TestMethod]
        public void TestGetCoordsToDetonateByTheBlastBadPower0()
        {
            var es = new SimpleExplosionStrategy();
            var suspiciousResult = es.GetCoordsToDetonateByTheBlast(2, 2, 0);
            Assert.IsTrue(suspiciousResult.Count == 0);
        }
    }
}
