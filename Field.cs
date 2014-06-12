using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleFieldNamespace
{
    class Field
    {
        private Condition condition;
	    private int explosivePower;

        public Field(Condition condition, int explosivePower)
        {
            this.explosivePower = explosivePower;
            this.Condition = condition;
        }

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

        public int DetonateIntentional()
        {
            if (this.Condition == Condition.Bomb)
            {
                this.Condition = Condition.BlownUp;
                // TODO - throw an event that a bomb has been blown up
                // I'm not sure how to do that... sorry
            }
            return this.explosivePower;
        }

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
