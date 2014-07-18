using BattleField7Namespace.NewGameDesign.Interfaces;
using System;
using System.Linq;
using BattleField7Namespace.NewGameDesign.Enumerations;

namespace BattleField7Namespace.NewGameDesign.GameClasses
{
    /// <summary>
    /// A single field of the Battle Field 7 game.
    /// Can be either an empty field or a field with a bomb.
    /// </summary>
    public class SimpleField : IField
    {
        /// <summary>
        /// The condition of the field.
        /// </summary>
        private Condition condition;

        /// <summary>
        /// The explosive power of the field.
        /// It is always 0 if the condition of the field is not Condition.Bomb
        /// </summary>
	    private int explosivePower;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleField" /> class.
        /// </summary>
        /// <param name="condition">The condition of the field.</param>
        /// <param name="explosivePower">The explosive power.</param>
        public SimpleField(Condition condition, int explosivePower)
        {
            this.ExplosivePower = explosivePower;
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
        /// Intentionaly detonates the field
        /// </summary>
        /// <returns>Fields explosive power</returns>
        public int IntentionalDetonate()
        {
            int power = this.ExplosivePower;
            this.Condition = Condition.BlownUp;
            return power;
        }

        /// <summary>
        /// Detonates the field by chain reaction
        /// </summary>
        public void DetonateByChainReaction()
        {
            this.Condition = Condition.BlownUp;
        }

        /// <summary>
        /// To the character.
        /// </summary>
        /// <returns></returns>
        public char ToChar()
        {
            if (this.Condition == Condition.BlownUp)
            {
                return 'X';
            }
            else if (this.Condition == Condition.Empty)
            {
                return '-';
            }
            else
            {
                return this.ExplosivePower.ToString()[0];
            }
        }

        /// <summary>
        /// Gets the condition.
        /// </summary>
        /// <returns></returns>
        public Condition GetCondition()
        {
            return this.Condition;
        }

        /// <summary>
        /// Sets the condition of the field.
        /// </summary>
        /// <param name="condition"></param>
        public void SetCondition(Condition condition)
        {
            this.Condition = condition;
        }

        /// <summary>
        /// Gets the explosive power.
        /// </summary>
        /// <returns></returns>
        public int GetExplosivePower()
        {
            return this.ExplosivePower;
        }

        /// <summary>
        /// Sets the explosive power.
        /// </summary>
        public void SetExplosivePower(int power)
        {
            this.ExplosivePower = power;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            return new SimpleField(this.Condition, this.ExplosivePower);
        }
    }
}
