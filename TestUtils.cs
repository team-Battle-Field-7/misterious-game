using System;
using System.Linq;
using BattleField7Namespace.NewGameDesign.GameClasses;
using BattleField7Namespace.NewGameDesign.Enumerations;
using BattleField7Namespace.NewGameDesign.UIClasses;

namespace BattleField7UnitTests
{
    public static class TestUtils
    {
        /// <summary>
        /// Provides random nonzero explosive power for a field
        /// </summary>
        /// <returns>An int between 1 and 5</returns>
        public static int ValidPower()
        {
            var random = new Random();
            return random.Next(1, 5);
        }

        /// <summary>
        /// Provides a random nonzero battlefield size.
        /// </summary>
        /// <param name="lowerBound">The minimal size of the test battlefield needed</param>
        /// <returns>An int between <paramref name="lowerBound"/> and 10</returns>
        public static int ValidSize(int lowerBound = 1)
        {
            var random = new Random();
            return random.Next(lowerBound, 10);
        }

        /// <summary>
        /// Provides a regular <c>SimpleBattleField</c> object, the way it would be usually created.
        /// </summary>
        /// <returns>A <c>SimpleBattleField</c> with an <c>basicField</c> in <c>Condition.Empty</c> and explosive power 0</returns>
        public static SimpleBattleField NormalBattleFieldGen()
        {
            var dummyEngine = new SimpleEngine();
            dummyEngine.UserInterface = new ConsoleUI();
            var testBattleField = new SimpleBattleField(new SimpleField(Condition.Empty, 0), new SimpleExplosionStrategy());
            dummyEngine.BattleField = testBattleField;
            return (SimpleBattleField)dummyEngine.BattleField;
        }

        /// <summary>
        /// Provides an odd <c>SimpleBattleField</c> object, the way it would never be created but would be accepted by the assembly
        /// </summary>
        /// <returns>A <c>SimpleBattleField</c> with an <c>basicField</c> in <c>Condition.Bomb</c> and a valid explosive power. </returns>
        public static SimpleBattleField BrokenBattleFieldGen()
        {
            var dummyEngine = new SimpleEngine();
            dummyEngine.UserInterface = new ConsoleUI();
            var testBattleField = new SimpleBattleField(new SimpleField(Condition.Bomb, TestUtils.ValidPower()), new SimpleExplosionStrategy());
            dummyEngine.BattleField = testBattleField;
            return (SimpleBattleField)dummyEngine.BattleField;
        }

        /// <summary>
        /// Compares 2D arrays element by element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns><c>false</c> if any two elements at the same coorfinates don't match</returns>
        public static bool ContentEquals<T>(this T[,] x, T[,] y) where T : IComparable
        {
            if (x.GetLength(0) != y.GetLength(0) || x.GetLength(1) != y.GetLength(1))
            {
                return false;
            }

            for (int i = 0; i < x.GetLength(0); i++)
            {
                for (int j = 0; j < x.GetLength(1); j++)
                {
                    if (x[i,j].CompareTo(y[i,j]) != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Searches for and identifies of a bomb that's at an edge of a battlefield
        /// for test purposes.
        /// </summary>
        /// <param name="battleField">A PRE-INITIALIZED SimpleBattleField object</param>
        /// <returns>True or false depending on whether such a bomb was found</returns>
        /// <remarks>Optimized for rectangular battlefields as well, not just square ones.</remarks>
        /// <note>Uses <c>StringifyBattleField</c> to find bomb's coordinates</note>
        public static bool TryFindEdgeBomb(SimpleBattleField battleField, out Tuple<int, int> resultCoords)
        {
            char[,] fieldView = battleField.StringifyBattleField();
            for (int i = 0; i < fieldView.GetLength(0); i++)
            {
                if (Char.IsDigit(fieldView[i,0]))
                {
                    resultCoords = new Tuple<int, int>(i, 0);
                    return true;
                }

                if (Char.IsDigit(fieldView[i, fieldView.GetLength(1) - 1]))
                {
                    resultCoords = new Tuple<int, int>(i, fieldView.GetLength(1) - 1);
                    return true;
                }
            }

            for (int i = 0; i < fieldView.GetLength(1); i++)
            {
                if (Char.IsDigit(fieldView[0, i]))
                {
                    resultCoords = new Tuple<int, int>(0, i);
                    return true;
                }

                if (Char.IsDigit(fieldView[fieldView.GetLength(0) - 1, i]))
                {
                    resultCoords = new Tuple<int, int>(fieldView.GetLength(0), i);
                    return true;
                }
            }

            resultCoords = null;
            return false;
        }

        /// <summary>
        /// Finds the number of bombs in a battlefield
        /// </summary>
        /// <param name="testField">A PRE-INITIALIZED SimpleBattleField object</param>
        /// <returns>The count of all digit characters returned by <c>StringifyBattleField</c></returns>
        public static int BombCount(SimpleBattleField testField)
        {
            int result = 0;
            char[,] fieldView = testField.StringifyBattleField();

            for (int i = 0; i < fieldView.GetLength(0); i++)
            {
                for (int j = 0; j < fieldView.GetLength(1); j++)
                {
                    if (Char.IsDigit(fieldView[i,j]))
                    {
                        result++;
                    }
                }
            }

            return result;
        }
    }
}
