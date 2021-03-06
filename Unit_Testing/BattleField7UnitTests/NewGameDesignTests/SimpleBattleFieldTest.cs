﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleField7Namespace.NewGameDesign.GameClasses;
using BattleField7Namespace.NewGameDesign.Enumerations;

namespace BattleField7UnitTests.NewGameDesignTests
{
    [TestClass]
    public class SimpleBattleFieldTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBadBasicFieldConstruct()
        {
            var badBasicField = new SimpleField(Condition.Bomb, TestUtils.ValidPower());
            var notOkBattleField = new SimpleBattleField(badBasicField, new SimpleExplosionStrategy());
            Assert.Fail("Construction of a SimpleBattleField with an explosive basic field shouldn't be allowed. That can brake the initialization logic.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestValidSizeBattleFieldInitialization()
        {
            var commonBattleField = TestUtils.NormalBattleFieldGen();
            commonBattleField.InitializeBattleField(-1);
            var itsBombCount = commonBattleField.BombsCount;
            Assert.AreEqual(itsBombCount, 0, "A BattleField of negative size shouldn't have any bombs.");
        }

        [TestMethod]
        public void TestMultipleBattleFieldInitializations()
        {
            var testField = TestUtils.NormalBattleFieldGen();
            var size = TestUtils.ValidSize();
            testField.InitializeBattleField(size);
            int bombCount1 = TestUtils.BombCount(testField);
            testField.InitializeBattleField(size);
            int bombCount2 = TestUtils.BombCount(testField);
            Assert.AreEqual(bombCount1, bombCount2, "Initializing a battlefield a second time shouldn't affect the bomb count. Size: " + size);
        }

        [TestMethod]
        public void TestStringifyBattleField()
        {
            var battleField = TestUtils.NormalBattleFieldGen();
            battleField.InitializeBattleField(1);

            var actual = battleField.StringifyBattleField();
            char[,] expected = { { '-' } };
            Assert.IsTrue(actual.ContentEquals(expected), "We can't even represent a one-field battleField!");
        }

        /// <summary>
        /// Not so much to mess up with returning an int, just to make sure no ArgumentOutOfRangeException-s are thrown
        /// </summary>
        [TestMethod]
        public void TestDetonateFieldAtPosition()
        {
            var testBattleField = TestUtils.NormalBattleFieldGen();
            var size = TestUtils.ValidSize(4); //If the size is less than 4, we may have no bombs put in the field.
            testBattleField.InitializeBattleField(size);

            Tuple<int, int> foundBombCoords;
            bool bombFound = TestUtils.TryFindEdgeBomb(testBattleField, out foundBombCoords);
            while (!bombFound)
            {
                testBattleField.InitializeBattleField(size); //It's not important whether InitializeBattleField adds more bombs, as long as it puts at least one of them on an edge.
                bombFound = TestUtils.TryFindEdgeBomb(testBattleField, out foundBombCoords);
            }

            Assert.IsTrue(testBattleField.DetonateFieldAtPosition(foundBombCoords.Item1, foundBombCoords.Item2) > 0, "Error with size " + size);
        }

        [TestMethod]
        public void TestCoordinatesAreValid()
        {
            var testBattleField = TestUtils.NormalBattleFieldGen();
            testBattleField.InitializeBattleField(1);
            Assert.IsTrue(testBattleField.CoordinatesAreValid(0, 0), "CoordinatesAreValid gives false-negative results");
            Assert.IsFalse(testBattleField.CoordinatesAreValid(0, 1), "CoordinatesAreValid gives false-positive results (max columns + 1)");
            Assert.IsFalse(testBattleField.CoordinatesAreValid(0, -1), "CoordinatesAreValid gives false-positive results (columns = -1)");
            Assert.IsFalse(testBattleField.CoordinatesAreValid(1, 0), "CoordinatesAreValid gives false-positive results (max rows + 1)");
            Assert.IsFalse(testBattleField.CoordinatesAreValid(-1, 0), "CoordinatesAreValid gives false-positive results (rows = -1)");
        }
    }
}
