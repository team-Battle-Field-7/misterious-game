using BattleField7Namespace.NewGameDesign.Interfaces;
using BattleField7Namespace.NewGameDesign.Types;
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
        /// Initializes a new instance of the <see cref="SimpleBattleField" /> class.
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
        /// <exception cref="System.ArgumentException">
        /// Basic Field of SimpleBattleField can not be null.
        /// or
        /// Basic Field of SimpleBattleField can not be a bomb.
        /// or
        /// Basic Field of SimpleBattleField can not have an explosive charge.
        /// </exception>
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
        /// <returns>
        /// The count of the bombs.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Size of BattleField can not be 0 or negative</exception>
        public int InitializeBattleField(int size)
        {
            if (0 >= size)
            {
                throw new ArgumentOutOfRangeException("Size of BattleField can not be 0 or negative");
            }

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
                    Coord2D coords = this.GetRandomPosition();
                    if (this.fields[coords.Row, coords.Column].GetCondition() != Condition.Bomb) //Never satisfied if the object is contructed with a basicField with condition.Bomb; infinite loop!
                    {
                        this.ConvertFieldToBomb(coords.Row, coords.Column);
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
        /// <param name="position">The position</param>
        /// <returns>
        /// Count of detonated bombs.
        /// </returns>
        public int DetonateFieldAtPosition(int row, int column)
        {
            return this.DetonateFieldAtPosition(new Coord2D(row, column));
        }

        /// <summary>
        /// Detonates the field at position.
        /// </summary>
        /// <param name="position">The position</param>
        /// <returns>
        /// Count of detonated bombs.
        /// </returns>
        public int DetonateFieldAtPosition(Coord2D position)
        {
            int detonatedBombs = 0;

            if (this.fields[position.Row, position.Column].GetCondition() == Condition.Bomb)
            {
                detonatedBombs++;
            }

            int explosivePower = this.fields[position.Row, position.Column].IntentionalDetonate();
            IList<Coord2D> coordsToDetonate = this.ExplosionStrategy.GetCoordsToDetonateByTheBlast(position, explosivePower);
            foreach (Coord2D coord in coordsToDetonate)
            {
                if (this.CoordinatesAreValid(coord.Row, coord.Column))
                {
                    if (this.fields[coord.Row, coord.Column].GetCondition() == Condition.Bomb)
                    {
                        detonatedBombs++;
                    }
                    this.fields[coord.Row, coord.Column].DetonateByChainReaction();
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

        private Coord2D GetRandomPosition()
        {
            Random rnd = new Random();
            int row = rnd.Next(0, this.fields.GetLength(0));
            int col = rnd.Next(0, this.fields.GetLength(1));
            return new Coord2D(row, col);
        }

        private void ConvertFieldToBomb(int row, int col)
        {
            Random rnd = new Random();
            this.fields[row, col].SetCondition(Condition.Bomb);
            this.fields[row, col].SetExplosivePower(rnd.Next(1, 6));
        }
    }
}
