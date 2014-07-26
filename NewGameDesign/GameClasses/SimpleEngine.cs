using System;
using System.Linq;

using BattleField7Namespace.NewGameDesign.Interfaces;

namespace BattleField7Namespace.NewGameDesign.GameClasses
{
    /// <summary>
    /// A Simple version of the engine
    /// </summary>
    public class SimpleEngine : IEngine
    {
        /// <summary>
        /// The user interface
        /// </summary>
        private IUserInterface userInterface;

        /// <summary>
        /// The battle field
        /// </summary>
        private IBattleField battleField;

        /// <summary>
        /// The turns counter.
        /// </summary>
        private int turnsCount;

        /// <summary>
        /// True when the game is over
        /// </summary>
        private bool gameIsOver = false;

        /// <summary>
        /// The logger
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// Gets or sets the logger by reference.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        internal ILogger Logger
        {
            get
            {
                return this.logger;
            }
            set
            {
                this.logger = value;
            }
        }

        /// <summary>
        /// Gets a copy of the battle field.
        /// </summary>
        /// <returns>A deep copy of the battle field</returns>
        public IBattleField GetBattleField()
        {
            return this.battleField.Clone();
        }


        /// <summary>
        /// Gets a copy of the user interface.
        /// </summary>
        /// <returns>A deep copy of the user interface</returns>
        public IUserInterface GetUserInterface()
        {
            return this.userInterface.Clone();
        }

        /// <summary>
        /// Observes the specified notifyer.
        /// </summary>
        /// <param name="notifyer">The notifyer.</param>
        public void Observe(ICountNotifier notifyer)
        {
            notifyer.AttachObserver(this);
        }

        /// <summary>
        /// Updates the count.
        /// </summary>
        /// <param name="bombsCount">The bombs count.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void UpdateCount(int bombsCount)
        {
            this.UserInterface.ShowBombsCount(bombsCount);
            if (bombsCount <= 0)
            {
                this.gameIsOver = true;
            }
        }

        ///// <summary>
        ///// Runs the game via Poor Man's Dependency Injevtion. Highly NOT recomended.
        ///// </summary>
        ///// <param name="userInterface">The user interface.</param>
        ///// 
        //public void RunGame(IUserInterface userInterface)
        //{
        //    if (this.Logger != null)
        //    {
        //        this.Logger.LogWarning("Usage of Poor Man's Dependency Injevtion");
        //    }

        //    this.RunGame(userInterface,
        //        new SimpleBattleField(
        //            new SimpleField(Condition.Empty, 0),
        //            new MyExplosionStrategy()
        //            )
        //        );
        //}

        /// <summary>
        /// Runs the game.
        /// </summary>
        /// <param name="userInterface">The user interface.</param>
        /// <param name="battleField">The battle field.</param>
        public void RunGame(IUserInterface userInterface, IBattleField battleField)
        {
            if (this.Logger != null)
            {
                this.Logger.LogStartUp();
            }

            this.UserInterface = userInterface;
            this.BattleField = battleField;

            InitializeBattleField();
            this.UserInterface.DrawGame(this.BattleField.StringifyBattleField());
            this.UserInterface.ShowTurnsCount(turnsCount);

            while (!this.gameIsOver)
            {
                this.turnsCount++;
                Tuple<int, int> position = GetPositionInput();
                int detonatedBombs = this.BattleField.DetonateFieldAtPosition(position);
                if (detonatedBombs > 0)
                {
                    this.UserInterface.ShowNote("A bomb was hit.");
                    for (int i = 1; i < detonatedBombs; i++)
                    {
                        this.UserInterface.ShowNote("A bomb was detonated by chain reaction");
                    }
                }
                this.UserInterface.DrawGame(this.BattleField.StringifyBattleField());
                this.UserInterface.ShowTurnsCount(turnsCount);
            }

            this.UserInterface.ShowCongratulations("You beat the game in " + this.turnsCount + " turns. Congrats!");
        }

        /// <summary>
        /// Gets or sets the battle field.
        /// </summary>
        /// <value>
        /// The battle field.
        /// </value>
        /// <remarks>The getter returns a reference instead of a clone.
        /// The purpose of this is to avoid the two-way dependency loophole with <c>SimpleBattleField</c>.
        /// </remarks>
        internal IBattleField BattleField
        {
            get
            {
                // TODO - Should rerturn a clone.
                return this.battleField;
            }
            set
            {
                // TODO - Should validate
                this.battleField = value;
                this.Observe(this.battleField);
            }
        }

        /// <summary>
        /// Gets or sets the user interface.
        /// </summary>
        /// <value>
        /// The user interface.
        /// </value>
        /// <remarks>The getter returns a reference instead of a clone.
        /// The purpose of this is to avoid the two-way dependency loophole with <c>SimpleBattleField</c>.
        /// </remarks>
        internal IUserInterface UserInterface
        {
            get
            {
                // TODO - Should rerturn a clone.
                return this.userInterface;
            }
            set
            {
                // TODO - Should validate
                this.userInterface = value;
            }
        }

        /// <summary>
        /// Initializes the battle field.
        /// </summary>
        private void InitializeBattleField()
        {
            int size = -1;
            bool valid = false;

            while (!valid)
	        {
                valid = int.TryParse(this.UserInterface.AskForSizeInput("Input size of battle field (1-10): "), out size);
                if (!valid || 0 > size || size > 10)
                {
                    valid = false;
                    this.UserInterface.ShowNote("Invalid size input. Try Again. ");
                }
	        }

            this.BattleField.InitializeBattleField(size);
        }

        /// <summary>
        /// Gets the position input.
        /// </summary>
        /// <returns></returns>
        private Tuple<int, int> GetPositionInput()
        {
            int row = -1;
            int col = -1;
            string[] input;
            bool valid = false;

            while (!valid)
            {
                input = this.UserInterface.AskForPositionInput("Input position to detonate (row col): ").Split(' ');
                if (input.Length != 2)
	            {
		            continue;
                }
                valid = int.TryParse(input[0], out row);
                valid = int.TryParse(input[1], out col) && valid;

                if (!valid || !this.BattleField.CoordinatesAreValid(row, col))
                {
                    valid = false;
                    this.UserInterface.ShowNote("Invalid position input. Try Again. ");
                }
            }
            return Tuple.Create<int, int>(row, col);
        }
    }
}
