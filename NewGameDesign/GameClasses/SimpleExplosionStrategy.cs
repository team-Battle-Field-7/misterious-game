using BattleField7Namespace.NewGameDesign.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleField7Namespace.NewGameDesign.GameClasses
{
    /// <summary>
    /// /// Logic for getting the coordinates of a blast after detonation of a bomb
    /// </summary>
    public class SimpleExplosionStrategy : IExplosionStrategy
    {
        /// <summary>
        /// The explosion range positions grouped by power.
        /// The outer List has 5 elements (lists), representing the 5 possible explosive powers.
        /// The middle List has all the relative cell positions for a specific explosive power (only the unique positions)
        /// A relative position is represented by an int array of 2 elements - row and column,
        /// witch are to be added to the detonation position in order to get the coordinates of the specified position.
        /// </summary>
        private List<List<Tuple<int, int>>> explosionRangePositionsGroupedByPower;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleExplosionStrategy"/> class.
        /// </summary>
        public SimpleExplosionStrategy()
        {
            this.InitializeExplosionRanges();
        }

        /// <summary>
        /// Gets the coords to detonate by the blast.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="explosivePower">The explosive power.</param>
        /// <returns></returns>
        public IList<Tuple<int, int>> GetCoordsToDetonateByTheBlast(int row, int column, int explosivePower)
        {
            return this.GetCoordsToDetonateByTheBlast(Tuple.Create<int, int>(row, column), explosivePower);
        }

        /// <summary>
        /// Gets the coords to detonate by the blast.
        /// </summary>
        /// <param name="explosionCoords">The explosion coordinates</param>
        /// <param name="explosivePower">The explosive power.</param>
        /// <returns>
        /// The coordinates of the fields to detonate by chain reaction.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">A negative coordinate is out of range
        /// or
        /// Negative explosive power is out of range</exception>
        public IList<Tuple<int, int>> GetCoordsToDetonateByTheBlast(Tuple<int, int> explosionCoords, int explosivePower)
        {
            if (0 > explosionCoords.Item1 ||
                0 > explosionCoords.Item2)
            {
                throw new ArgumentOutOfRangeException("An explosion coordinate can not be negative");
            }
            if (0 > explosivePower ||
                explosivePower > 5)
            {
                throw new ArgumentOutOfRangeException("The explosive power can not be negative, or bigger then 5.");
            }

            List<Tuple<int, int>> fieldsToDetonate = new List<Tuple<int, int>>();
            for (int power = 0; power < explosivePower; power++)
            {
                foreach (Tuple<int, int> relativePosition in this.explosionRangePositionsGroupedByPower[power])
                {

                    int row = explosionCoords.Item1 + relativePosition.Item1;
                    int col = explosionCoords.Item2 + relativePosition.Item2;
                    Tuple<int, int> coords = Tuple.Create<int, int>(row, col);
                    fieldsToDetonate.Add(coords);
                }
            }
            return fieldsToDetonate;
        }

        /// <summary>
        /// Initializes the explosion ranges.
        /// ExplosionRanges holds the reletive positions to detonate for each explosion power
        /// </summary>
        private void InitializeExplosionRanges() // hard coded, but easy to understand and change
        {
            List<Tuple<int, int>> powerLevelOne = new List<Tuple<int, int>>();
            powerLevelOne.Add(Tuple.Create<int, int>(1, 1));
            powerLevelOne.Add(Tuple.Create<int, int>(-1, 1));
            powerLevelOne.Add(Tuple.Create<int, int>(1, -1));
            powerLevelOne.Add(Tuple.Create<int, int>(-1, -1));

            List<Tuple<int, int>> powerLevelTwo = new List<Tuple<int, int>>();
            powerLevelTwo.Add(Tuple.Create<int, int>(1, 0));
            powerLevelTwo.Add(Tuple.Create<int, int>(0, 1));
            powerLevelTwo.Add(Tuple.Create<int, int>(-1, 0));
            powerLevelTwo.Add(Tuple.Create<int, int>(0, -1));

            List<Tuple<int, int>> powerLevelThree = new List<Tuple<int, int>>();
            powerLevelThree.Add(Tuple.Create<int, int>(2, 0));
            powerLevelThree.Add(Tuple.Create<int, int>(0, 2));
            powerLevelThree.Add(Tuple.Create<int, int>(-2, 0));
            powerLevelThree.Add(Tuple.Create<int, int>(0, -2));

            List<Tuple<int, int>> powerLevelFour = new List<Tuple<int, int>>();
            powerLevelFour.Add(Tuple.Create<int, int>(2, 1));
            powerLevelFour.Add(Tuple.Create<int, int>(-2, 1));
            powerLevelFour.Add(Tuple.Create<int, int>(2, -1));
            powerLevelFour.Add(Tuple.Create<int, int>(-2, -1));
            powerLevelFour.Add(Tuple.Create<int, int>(1, 2));
            powerLevelFour.Add(Tuple.Create<int, int>(-1, 2));
            powerLevelFour.Add(Tuple.Create<int, int>(1, -2));
            powerLevelFour.Add(Tuple.Create<int, int>(-1, -2));

            List<Tuple<int, int>> powerLevelFive = new List<Tuple<int, int>>();
            powerLevelFive.Add(Tuple.Create<int, int>(2, 2));
            powerLevelFive.Add(Tuple.Create<int, int>(-2, 2));
            powerLevelFive.Add(Tuple.Create<int, int>(2, -2));
            powerLevelFive.Add(Tuple.Create<int, int>(-2, -2));

            this.explosionRangePositionsGroupedByPower = new List<List<Tuple<int, int>>>();
            this.explosionRangePositionsGroupedByPower.Add(powerLevelOne);
            this.explosionRangePositionsGroupedByPower.Add(powerLevelTwo);
            this.explosionRangePositionsGroupedByPower.Add(powerLevelThree);
            this.explosionRangePositionsGroupedByPower.Add(powerLevelFour);
            this.explosionRangePositionsGroupedByPower.Add(powerLevelFive);
        }
    }
}
