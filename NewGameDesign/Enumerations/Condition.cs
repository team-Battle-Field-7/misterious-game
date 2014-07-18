using System;
using System.Linq;

namespace BattleField7Namespace.NewGameDesign.Enumerations
{
    /// <summary>
    /// Condition types for Field
    /// </summary>
    public enum Condition
    {
        /// <summary>
        /// When a field is neghter a bomb, nor blown up.
        /// </summary>
        Empty,

        /// <summary>
        /// When a field has a bomb
        /// </summary>
        Bomb,

        /// <summary>
        /// When a Field has been blown up by a bomb.
        /// </summary>
        BlownUp
    }
}
