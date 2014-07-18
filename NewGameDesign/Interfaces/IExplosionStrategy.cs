using BattleField7Namespace.NewGameDesign.Types;
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
        /// <param name="explosionCoords">The explosion coords.</param>
        /// <param name="explosivePower">The explosive power.</param>
        /// <returns>
        /// The coordinates of the fields to detonate by chain reaction.
        /// </returns>
        IList<Tuple<int, int>> GetCoordsToDetonateByTheBlast(Tuple<int, int> explosionCoords, int explosivePower);
    }
}
