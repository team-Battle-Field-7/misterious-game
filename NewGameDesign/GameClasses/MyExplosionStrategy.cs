using BattleField7Namespace.NewGameDesign.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace.NewGameDesign.GameClasses
{
    /// <summary>
    /// /// Logic for getting the coordinates of a blast after detonation of a bomb
    /// </summary>
    class MyExplosionStrategy : IExplosionStrategy
    {
        /// <summary>
        /// The explosion range positions grouped by power.
        /// The outer List has 5 elements (lists), representing the 5 possible explosive powers.
        /// The middle List has all the relative cell positions for a specific explosive power (only the unique positions)
        /// A relative position is represented by an int array of 2 elements - row and column,
        /// witch are to be added to the detonation position in order to get the coordinates of the specified position.
        /// </summary>
        private List<List<int[]>> explosionRangePositionsGroupedByPower;

        public MyExplosionStrategy()
        {
            this.InitializeExplosionRanges();
        }

        /// <summary>
        /// Gets the coords to detonate by the blast.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <param name="explosivePower">The explosive power.</param>
        /// <returns>
        /// The coordinates of the fields to detonate by chain reaction.
        /// </returns>
        public IList<int[]> GetCoordsToDetonateByTheBlast(int row, int col, int explosivePower)
        {
            List<int[]> fieldsToDetonate = new List<int[]>();
            for (int power = 0; power < explosivePower; power++)
            {
                foreach (int[] relativePosition in explosionRangePositionsGroupedByPower[power])
                {
                    int[] coords = new int[2];
                    coords[0] = row + relativePosition[0];
                    coords[1] = col + relativePosition[1];
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
            List<int[]> powerLevelOne = new List<int[]>();
            powerLevelOne.Add(new int[] { 1, 1 });
            powerLevelOne.Add(new int[] { -1, 1 });
            powerLevelOne.Add(new int[] { 1, -1 });
            powerLevelOne.Add(new int[] { -1, -1 });

            List<int[]> powerLevelTwo = new List<int[]>();
            powerLevelTwo.Add(new int[] { 1, 0 });
            powerLevelTwo.Add(new int[] { 0, 1 });
            powerLevelTwo.Add(new int[] { -1, 0 });
            powerLevelTwo.Add(new int[] { 0, -1 });

            List<int[]> powerLevelThree = new List<int[]>();
            powerLevelThree.Add(new int[] { 2, 0 });
            powerLevelThree.Add(new int[] { 0, 2 });
            powerLevelThree.Add(new int[] { -2, 0 });
            powerLevelThree.Add(new int[] { 0, -2 });

            List<int[]> powerLevelFour = new List<int[]>();
            powerLevelFour.Add(new int[] { 2, 1 });
            powerLevelFour.Add(new int[] { -2, 1 });
            powerLevelFour.Add(new int[] { 2, -1 });
            powerLevelFour.Add(new int[] { -2, -1 });
            powerLevelFour.Add(new int[] { 1, 2 });
            powerLevelFour.Add(new int[] { -1, 2 });
            powerLevelFour.Add(new int[] { 1, -2 });
            powerLevelFour.Add(new int[] { -1, -2 });

            List<int[]> powerLevelFive = new List<int[]>();
            powerLevelFive.Add(new int[] { 2, 2 });
            powerLevelFive.Add(new int[] { -2, 2 });
            powerLevelFive.Add(new int[] { 2, -2 });
            powerLevelFive.Add(new int[] { -2, -2 });

            this.explosionRangePositionsGroupedByPower = new List<List<int[]>>();
            this.explosionRangePositionsGroupedByPower.Add(powerLevelOne);
            this.explosionRangePositionsGroupedByPower.Add(powerLevelTwo);
            this.explosionRangePositionsGroupedByPower.Add(powerLevelThree);
            this.explosionRangePositionsGroupedByPower.Add(powerLevelFour);
            this.explosionRangePositionsGroupedByPower.Add(powerLevelFive);
        }
    }
}
