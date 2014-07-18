using BattleField7Namespace.NewGameDesign.Interfaces;
using BattleField7Namespace.NewGameDesign.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        private List<List<Coord2D>> explosionRangePositionsGroupedByPower;

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
        public IList<Coord2D> GetCoordsToDetonateByTheBlast(int row, int column, int explosivePower)
        {
            return this.GetCoordsToDetonateByTheBlast(new Coord2D(row, column), explosivePower);
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
        public IList<Coord2D> GetCoordsToDetonateByTheBlast(Coord2D explosionCoords, int explosivePower)
        {
            if (0 > explosionCoords.Row ||
                0 > explosionCoords.Column)
            {
                throw new ArgumentOutOfRangeException("An explosion coordinate can not be negative");
            }
            if (0 > explosivePower ||
                explosivePower > 5)
            {
                throw new ArgumentOutOfRangeException("The explosive power can not be negative, or bigger then 5.");
            }

            List<Coord2D> fieldsToDetonate = new List<Coord2D>();
            for (int power = 0; power < explosivePower; power++)
            {
                foreach (Coord2D relativePosition in this.explosionRangePositionsGroupedByPower[power])
                {

                    int row = explosionCoords.Row + relativePosition.Row;
                    int col = explosionCoords.Column + relativePosition.Column;
                    Coord2D coords = new Coord2D(row, col);
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
            List<Coord2D> powerLevelOne = new List<Coord2D>();
            powerLevelOne.Add(new Coord2D(1, 1));
            powerLevelOne.Add(new Coord2D(-1, 1));
            powerLevelOne.Add(new Coord2D(1, -1));
            powerLevelOne.Add(new Coord2D(-1, -1));

            List<Coord2D> powerLevelTwo = new List<Coord2D>();
            powerLevelTwo.Add(new Coord2D(1, 0));
            powerLevelTwo.Add(new Coord2D(0, 1));
            powerLevelTwo.Add(new Coord2D(-1, 0));
            powerLevelTwo.Add(new Coord2D(0, -1));

            List<Coord2D> powerLevelThree = new List<Coord2D>();
            powerLevelThree.Add(new Coord2D(2, 0));
            powerLevelThree.Add(new Coord2D(0, 2));
            powerLevelThree.Add(new Coord2D(-2, 0));
            powerLevelThree.Add(new Coord2D(0, -2));

            List<Coord2D> powerLevelFour = new List<Coord2D>();
            powerLevelFour.Add(new Coord2D(2, 1));
            powerLevelFour.Add(new Coord2D(-2, 1));
            powerLevelFour.Add(new Coord2D(2, -1));
            powerLevelFour.Add(new Coord2D(-2, -1));
            powerLevelFour.Add(new Coord2D(1, 2));
            powerLevelFour.Add(new Coord2D(-1, 2));
            powerLevelFour.Add(new Coord2D(1, -2));
            powerLevelFour.Add(new Coord2D(-1, -2));

            List<Coord2D> powerLevelFive = new List<Coord2D>();
            powerLevelFive.Add(new Coord2D(2, 2));
            powerLevelFive.Add(new Coord2D(-2, 2));
            powerLevelFive.Add(new Coord2D(2, -2));
            powerLevelFive.Add(new Coord2D(-2, -2));

            this.explosionRangePositionsGroupedByPower = new List<List<Coord2D>>();
            this.explosionRangePositionsGroupedByPower.Add(powerLevelOne);
            this.explosionRangePositionsGroupedByPower.Add(powerLevelTwo);
            this.explosionRangePositionsGroupedByPower.Add(powerLevelThree);
            this.explosionRangePositionsGroupedByPower.Add(powerLevelFour);
            this.explosionRangePositionsGroupedByPower.Add(powerLevelFive);
        }
    }
}
