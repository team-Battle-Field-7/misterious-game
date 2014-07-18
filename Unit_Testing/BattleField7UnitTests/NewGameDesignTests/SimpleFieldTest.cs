using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleField7Namespace.NewGameDesign.GameClasses;
using BattleField7Namespace.NewGameDesign.Enumerations;

namespace BattleField7UnitTests.NewGameDesignTests
{
    [TestClass]
    public class SimpleFieldTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestBadPowerConstruct1()
        {
            var field = new SimpleField(Condition.Bomb, 6);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestBadPowerConstruct2()
        {
            var field = new SimpleField(Condition.Bomb, -1);
        }

        [TestMethod]
        public void TestConditionRepairingConstruct()
        {
            var field = new SimpleField(Condition.Empty, ValidPower());
            Assert.AreEqual(field.ExplosivePower, 0);
        }

        [TestMethod]
        public void TestDetonateSafeFields()
        {
            var emptyField = new SimpleField(Condition.Empty, ValidPower());
            var blownField = new SimpleField(Condition.BlownUp, ValidPower());
            Assert.IsTrue(emptyField.IntentionalDetonate() == 0 && blownField.IntentionalDetonate() == 0);
        }

        [TestMethod]
        public void TestDetonateBombSimpleField()
        {
            var bombField = new SimpleField(Condition.Bomb, ValidPower());
            var i = bombField.IntentionalDetonate();
            Assert.AreEqual(bombField.ExplosivePower, 0);
        }

        [TestMethod]
        public void TestChainDetonateSafeFields()
        {
            var emptyField = new SimpleField(Condition.Empty, 0);
            var blownField = new SimpleField(Condition.BlownUp, ValidPower());
            emptyField.DetonateByChainReaction();
            blownField.DetonateByChainReaction();
            Assert.IsTrue(emptyField.ExplosivePower == 0 && blownField.ExplosivePower == 0, "Explosive powers of DetonatedByChainReaction empty and blown fields are not 0");
            Assert.IsTrue(emptyField.Condition == Condition.BlownUp && blownField.Condition == Condition.BlownUp, "Empty and Blown Fields don't have the same condition after DetonatedByChainReaction().");
        }

        [TestMethod]
        public void TestChainDetonateBombSimpleField()
        {
            var bombField = new SimpleField(Condition.Bomb, ValidPower());
            bombField.DetonateByChainReaction();
            Assert.IsTrue(bombField.Condition == Condition.BlownUp, "BombField is not blown up after DetonateByChainReaction.");
            Assert.IsTrue(bombField.ExplosivePower == 0, "BombfField has asplosive power different than 0 after DetonateByChainReaction.");
        }

        


        /// <summary>
        /// Provides random nonzero explosive power for a field
        /// </summary>
        /// <returns>An int between 1 and 5</returns>
        public static int ValidPower()
        {
            var random = new Random();
            return random.Next(1, 5);
        }
    }
}
