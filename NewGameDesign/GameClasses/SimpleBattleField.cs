using BattleField7Namespace.NewGameDesign.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace.NewGameDesign.GameClasses
{
    /// <summary>
    /// The battle field contains all the Fields of the game as well as some logic about them.
    /// </summary>
    public class SimpleBattleField : IBattleField
    {
        /// <summary>
        /// The fields of the battle field.
        /// </summary>
        private IField[,] fields;

        /// <summary>
        /// The bombs frequency.
        /// </summary>
        private double bombsFrequency = 0.15;

        /// <summary>
        /// The basic field. Used only for cloning.
        /// </summary>
        private IField basicField;

        /// <summary>
        /// The explosion strategy.
        /// </summary>
        private IExplosionStrategy explosionStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleBattleField"/> class.
        /// </summary>
        /// <param name="basicField">The basic field.</param>
        /// <param name="explosionStrategy">The explosion strategy.</param>
        public SimpleBattleField(IField basicField, IExplosionStrategy explosionStrategy)
        {
            this.BasicField = basicField;
            this.ExplosionStrategy = explosionStrategy;
        }

        /// <summary>
        /// Gets or sets the bombs frequency.
        /// </summary>
        /// <value>
        /// The bombs frequency.
        /// </value>
        public double BombsFrequency
        {
            get
            {
                return this.bombsFrequency;
            }
            set
            {
                if (0 < value || value <= 1)
                {
                    this.bombsFrequency = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the basic field.
        /// </summary>
        /// <value>
        /// The basic field.
        /// </value>
        /// <remarks>Huge security leak! Permits the basic field to have Condition.Bomb</remarks>
        public IField BasicField
        {
            get 
            {
                return (IField)this.basicField.Clone();
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Basic Field of SimpleBattleField can not be null.");
                }
                if (value.GetCondition() == Condition.Bomb)
                {
                    throw new ArgumentException("Basic Field of SimpleBattleField can not be a bomb.");
                }
                if (value.GetExplosivePower() > 0)
                {
                    throw new ArgumentException("Basic Field of SimpleBattleField can not have an explosive charge.");
                }

                this.basicField = value;
            }
        }

        /// <summary>
        /// Gets or sets the explosion strategy.
        /// </summary>
        /// <value>
        /// The explosion strategy.
        /// </value>
        public IExplosionStrategy ExplosionStrategy
        {
            get
            {
                // TODO - Should return a clone
                return this.explosionStrategy;
            }
            set
            {
                if (value != null)
                {
                    this.explosionStrategy = value;
                }
            }
        }

        /// <summary>
        /// Initializes the battle field.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>The count of the bombs.</returns>
        public int InitializeBattleField(int size)
        {
            this.fields = new IField[size, size];

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    this.fields[r, c] = (IField)this.basicField.Clone();
                }
            }

            int bombCount = (int)(this.BombsFrequency * size * size);

            for (int b = 0; b < bombCount; b++)
            {
                while (true)
                {
                    int[] coords = this.GetRandomPosition();
                    if (this.fields[coords[0], coords[1]].GetCondition() != Condition.Bomb) //Never satisfied if the object is contructed with a basicField with condition.Bomb; infinite loop!
                    {
                        this.ConvertFieldToBomb(coords[0], coords[1]);
                        break;
                    }
                }
            }

            return bombCount;
        }

        /// <summary>
        /// Stringifies the battle field.
        /// </summary>
        /// <returns>
        /// The Stringified battle field.
        /// </returns>
        public char[,] StringifyBattleField()
        { 
            int size = this.fields.GetLength(0);
            char[,] stringifyedBattleField = new char[size, size];
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    stringifyedBattleField[r, c] = this.fields[r, c].ToChar();
                }
            }
            return stringifyedBattleField;
        }

        /// <summary>
        /// Detonates the field at position.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <returns>
        /// Count of detonated bombs.
        /// </returns>
        public int DetonateFieldAtPosition(int row, int col)
        {
            int detonatedBombs = 0;

            if (this.fields[row, col].GetCondition() == Condition.Bomb)
            {
                detonatedBombs++;
            }

            int explosivePower = this.fields[row, col].IntentionalDetonate();
            IList<int[]> coordsToDetonate = this.ExplosionStrategy.GetCoordsToDetonateByTheBlast(row, col, explosivePower);
            foreach (int[] coord in coordsToDetonate)
            {
                if (this.CoordinatesAreValid(coord[0], coord[1]))
                {
                    if (this.fields[coord[0], coord[1]].GetCondition() == Condition.Bomb)
                    {
                        detonatedBombs++;
                    }
                    this.fields[coord[0], coord[1]].DetonateByChainReaction();
                }
            }
            return detonatedBombs;
        }

        /// <summary>
        /// Checks if the given coordinateses are valid.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <returns>True if valid, False if not.</returns>
        public bool CoordinatesAreValid(int row, int col)
        {
            return (0 <= row && row < this.fields.GetLength(0)) && 
                (0 <= col && col < this.fields.GetLength(1));
        }

        private int[] GetRandomPosition()
        {
            Random rnd = new Random();
            int row = rnd.Next(0, this.fields.GetLength(0));
            int col = rnd.Next(0, this.fields.GetLength(1));
            return new int[] { row, col };
        }

        private void ConvertFieldToBomb(int row, int col)
        {
            Random rnd = new Random();
            this.fields[row, col].SetCondition(Condition.Bomb);
            this.fields[row, col].SetExplosivePower(rnd.Next(1, 6));
        }
    }
}
