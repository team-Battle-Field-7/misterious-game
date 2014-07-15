using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace.NewGameDesign.Interfaces
{
    interface IField : ICloneable
    {
        /// <summary>
        /// Intentionaly detonates the field
        /// </summary>
        /// <returns>Field's explosive power</returns>
        int IntentionalDetonate();

        /// <summary>
        /// Detonates the field by chain reaction
        /// </summary>
        void DetonateByChainReaction();

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        object Clone();
    }
}
