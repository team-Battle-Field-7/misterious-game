using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace.NewGameDesign.Interfaces
{
    /// <summary>
    /// Logic for getting the coordinates of a blast after detonation of a bomb
    /// </summary>
    public interface IExplosionStrategy
    {
        /// <summary>
        /// Gets the coords to detonate by the blast.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <param name="explosivePower">The explosive power.</param>
        /// <returns>The coordinates of the fields to detonate by chain reaction.</returns>
        IList<int[]> GetCoordsToDetonateByTheBlast(int row, int col, int explosivePower);
    }
}
