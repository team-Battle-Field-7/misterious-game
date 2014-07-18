using BattleField7Namespace.NewGameDesign.Enumerations;
using System;
using System.Linq;

namespace BattleField7Namespace.NewGameDesign.Interfaces
{
    /// <summary>
    /// A single field of the Battle Field 7 game.
    /// Can be either an empty field or a field with a bomb.
    /// </summary>
    public interface IField : ICloneable
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
        /// Gets the character representation of the field.
        /// </summary>
        /// <returns></returns>
        char ToChar();

        
        /// <summary>
        /// Gets the condition.
        /// </summary>
        /// <returns></returns>
        Condition GetCondition();

        /// <summary>
        /// Sets the condition of the field.
        /// </summary>
        void SetCondition(Condition condition);

        /// <summary>
        /// Gets the explosive power.
        /// </summary>
        /// <returns></returns>
        int GetExplosivePower();

        /// <summary>
        /// Sets the explosive power.
        /// </summary>
        void SetExplosivePower(int power);
    }
}
