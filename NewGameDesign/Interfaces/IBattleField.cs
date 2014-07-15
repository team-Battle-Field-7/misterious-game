using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace.NewGameDesign.Interfaces
{
    interface IBattleField
    {
        /// <summary>
        /// Stringifies the battle field.
        /// </summary>
        /// <returns>
        /// The Stringified battle field.
        /// </returns>
        string[,] StringifyBattleField();

        /// <summary>
        /// Detonates the field at position.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <returns>
        /// Count of detonated bombs.
        /// </returns>
        int DetonateFieldAtPosition(int row, int col);
    }
}
