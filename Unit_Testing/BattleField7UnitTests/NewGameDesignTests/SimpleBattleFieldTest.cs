using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleField7Namespace;
using BattleField7Namespace.NewGameDesign.GameClasses;

namespace BattleField7UnitTests.NewGameDesignTests
{
    [TestClass]
    public class SimpleBattleFieldTest
    {
        [TestMethod]
        public void TestBadBasicFieldConstruct()
        {
            var badBasicField = new SimpleField(Condition.Bomb, Utils.ValidPower());
            var notOkBattleField = new SimpleBattleField(badBasicField, new MyExplosionStrategy());
            Assert.Fail("Construction of a SimpleBattleField with an explosive basic field shouldn't be allowed. That can brake the initialization logic.");
        }

        [TestMethod]
        public void TestValidSizeBattleFieldInitialization()
        {
            var commonBattleField = Utils.NormalBattleFieldGen();
            var itsBombCount = commonBattleField.InitializeBattleField(-1);
            Assert.AreEqual(itsBombCount, 0, "A BattleField of negative size shouldn't have any bombs.");
        }

        [TestMethod]
        public void TestStringifyBattleField()
        {
            var battleField = Utils.NormalBattleFieldGen();
            battleField.InitializeBattleField(1);

            var actual = battleField.StringifyBattleField();
            char[,] expected = { { '-' } };
            Assert.IsTrue(actual.ContentEquals(expected));
        }

        [TestMethod]
        public void TestDetonateFieldAtPosition()
        {
            //To be continued (After I add some more Utils to make precise-case battlefields.)
        }
    }
}
