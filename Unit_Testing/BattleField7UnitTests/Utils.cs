using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleField7Namespace;
using BattleField7Namespace.NewGameDesign.GameClasses;

namespace BattleField7UnitTests
{
    public static class Utils
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
        /// <returns>An int between 1 and 10</returns>
        public static int ValidSize()
        {
            var random = new Random();
            return random.Next(1, 10);
        }

        /// <summary>
        /// Provides a regular <c>SimpleBattleField</c> object, the way it would be usually created.
        /// </summary>
        /// <returns>A <c>SimpleBattleField</c> with an <c>basicField</c> in <c>Condition.Empty</c> and explosive power 0</returns>
        public static SimpleBattleField NormalBattleFieldGen()
        {
            return new SimpleBattleField(new SimpleField(Condition.Empty, 0), new MyExplosionStrategy());
        }

        /// <summary>
        /// Provides an odd <c>SimpleBattleField</c> object, the way it would never be created but would be accepted by the assembly
        /// </summary>
        /// <returns>A <c>SimpleBattleField</c> with an <c>basicField</c> in <c>Condition.Bomb</c> and a valid explosive power. </returns>
        public static SimpleBattleField BrokenBattleFieldGen()
        {
            return new SimpleBattleField(new SimpleField(Condition.Bomb, Utils.ValidPower()), new MyExplosionStrategy());
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
    }
}
