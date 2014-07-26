using System;
using System.Linq;

namespace BattleField7Namespace.NewGameDesign.Interfaces
{
    /// <summary>
    /// The battle field is a collection of IFields with some methods
    /// </summary>
    public interface IBattleField : ICountNotifier
    {
        /// <summary>
        /// Initializes the battle field.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>The count of the bombs.</returns>
        void InitializeBattleField(int size);

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
        int DetonateFieldAtPosition(Tuple<int, int> position);

        /// <summary>
        /// Checks if the given coordinateses are valid.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <returns>True if valid, False if not.</returns>
        bool CoordinatesAreValid(int row, int col);

        /// <summary>
        /// Explicit implementation of ICloneable for IBallefield objects
        /// </summary>
        /// <returns>A deep copy ot the surrent IBattleField</returns>
        IBattleField Clone();
    }
}
