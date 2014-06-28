using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace
{
    /// <summary>
    /// A single field of the Battle Field 7 game.
    /// Can be either an empty field or a field with a bomb.
    /// </summary>
    class Field
    {
        private Condition condition;
	    private int explosivePower;

        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class.
        /// </summary>
        /// <param name="condition">The condition of the field.</param>
        /// <param name="explosivePower">The explosive power.</param>
        public Field(Condition condition, int explosivePower)
        {
            this.explosivePower = explosivePower;
            this.Condition = condition;
        }

        /// <summary>
        /// Gets the condition.
        /// </summary>
        /// <value>
        /// The condition.
        /// </value>
        public Condition Condition
        {
            get 
            {
                return this.condition;
            }
            private set
            {
                this.condition = value;
                if (this.Condition == Condition.Empty ||
                    this.Condition == Condition.BlownUp)
                {
                    this.ExplosivePower = 0;
                }
            }
        }

        /// <summary>
        /// Gets the explosive power.
        /// </summary>
        /// <value>
        /// The explosive power.
        /// </value>
        /// <exception cref="System.ArgumentOutOfRangeException">Explosive power of field must be between 0 an 5.</exception>
        public int ExplosivePower
        {
            get
            {
                return this.explosivePower;
            }
            private set
            {
                if (0 <= value && value <= 5)
                {
                    this.explosivePower = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Explosive power of field must be between 0 an 5.");
                }
            }
        }

        /// <summary>
        /// Intentionaly detonates the field and throws an event about that
        /// </summary>
        /// <returns>Fields explosive power</returns>
        public int IntentionalDetonate()
        {
            if (this.Condition == Condition.Bomb)
            {
                this.Condition = Condition.BlownUp;
                // TODO - throw an event that a bomb has been blown up
                // I'm not sure how to do that... sorry
            }
            return this.explosivePower;
        }

        /// <summary>
        /// Detonates the field by chain reaction and throws an event about that
        /// </summary>
        public void DetonateByChainReaction()
        {
            if (this.Condition == Condition.Bomb)
            {
                this.Condition = Condition.BlownUp;
                // TODO - throw an event that a bomb has been blown up
                // I'm not sure how to do that... sorry
            }
        }
    }
}
