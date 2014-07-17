using BattleField7Namespace.NewGameDesign.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace.NewGameDesign.Interfaces
{
    /// <summary>
    /// The battle field is a collection of IFields with some methods
    /// </summary>
    public interface IBattleField
    {
        /// <summary>
        /// Initializes the battle field.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>The count of the bombs.</returns>
        int InitializeBattleField(int size);

        /// <summary>
        /// Stringifies the battle field.
        /// </summary>
        /// <returns>
        /// The Stringified battle field.
        /// </returns>
        char[,] StringifyBattleField();

        /// <summary>
        /// Detonates the field at position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>
        /// Count of detonated bombs.
        /// </returns>
        int DetonateFieldAtPosition(Coord2D position);

        /// <summary>
        /// Checks if the given coordinateses are valid.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <returns>True if valid, False if not.</returns>
        bool CoordinatesAreValid(int row, int col);
    }
}
