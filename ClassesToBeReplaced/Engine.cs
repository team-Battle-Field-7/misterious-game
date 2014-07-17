using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BattleField7Namespace.NewGameDesign.Interfaces;

namespace BattleField7Namespace
{
    /// <summary>
    /// Engine that runs the Battle Field 7 game
    /// </summary>
    [Obsolete]
    internal class Engine
    {
        /// <summary>
        /// The bombs frequency is the percentage of the cells that will become bombs.
        /// </summary>
        private readonly double bombsFrequency = 0.15;

        /// <summary>
        /// Keeps track of the bombs left to blow up.
        /// </summary>
        private int bombsCount;

        /// <summary>
        /// Keeps track of the turns.
        /// </summary>
        private int turnsCount;

        /// <summary>
        /// The columns of the game grid.
        /// </summary>
        private int cols;

        /// <summary>
        /// The rows of the game grid.
        /// </summary>
        private int rows;

        /// <summary>
        /// The userInterface witch displays the game.
        /// </summary>
        private IUserInterface userInterface;

        /// <summary>
        /// The game field.
        /// </summary>
        private Field[,] gameField;

        /// <summary>
        /// The explosion range positions grouped by power.
        /// The outer List has 5 elements (lists), representing the 5 possible explosive powers.
        /// The middle List has all the relative cell positions for a specific explosive power (only the unique positions)
        /// A relative position is represented by an int array of 2 elements - row and column,
        /// witch are to be added to the detonation position in order to get the coordinates of the specified position.
        /// </summary>
        private List<List<int[]>> explosionRangePositionsGroupedByPower;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        /// <param name="drawer">The drawer.</param>
        public Engine(IUserInterface drawer)
        {
            this.userInterface = drawer;
        }

        /// <summary>
        /// Gets or sets the turns count.
        /// </summary>
        /// <value>
        /// The turns count.
        /// </value>
        private int TurnsCount 
        {
            get 
            {
                return this.turnsCount;
            }
            set 
            {
                this.turnsCount = value;
                userInterface.ShowTurnsCount(this.turnsCount);
            }
        }

        /// <summary>
        /// Gets or sets the bombs count.
        /// </summary>
        /// <value>
        /// The bombs count.
        /// </value>
        private int BombsCount
        {
            get
            {
                return this.bombsCount;
            }
            set
            {
                this.bombsCount = value;
                userInterface.ShowBombsCount(this.BombsCount);
            }
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Runs the game.
        /// </summary>
        /// <exception cref="System.ArgumentException">Size input must be a number between 1 and 10;size</exception>
        public void RunGame()
        {
            if (this.Logger != null)
            {
                this.Logger.LogStartUp();
            }

            InitializeExplosionRanges();
            SetSizeOfGameField();
            gameField = InitializeGameField(cols, rows);
            StartGame();

            // TODO - write a propper message for that case //This seems proper enough.
            userInterface.ShowCongratulations("You beat the game in " + turnsCount + " turns. Congrats!");
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        private void StartGame()
        {
            userInterface.DrawGame(StringifyGameField());
            TurnsCount = 0;
            while (BombsCount > 0)
            {
                // TODO - write a propper message for that case //Done
                string input = userInterface.AskForPositionInput("Enter target coordinates <row column>: ");

                int row;
                int col;
                bool inputValid = TryGetCoords(input, rows, cols, out row, out col);

                if (!inputValid)
                {
                    if (this.Logger != null)
                    {
                        this.Logger.LogEvent("Invalid cell input attempt");
                    }
                    // TODO - write a propper message for that case //Done
                    userInterface.ShowNote("Invalid input. Try again <row column>:");
                    continue;
                }

                TurnsCount++;

                Field selectedField = gameField[row, col];

                int explosionPower = selectedField.IntentionalDetonate();
                if (explosionPower > 0)
                {
                    OnBombBlownUpEvent();
                    DetonateNearbyFields(gameField, row, col, explosionPower);
                    userInterface.ShowNote("You hit a bomb with power level of " + explosionPower);
                }
                userInterface.DrawGame(StringifyGameField());
            }
        }

        /// <summary>
        /// Detonates the nearby fields after explosion.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        /// <param name="positionCol">The position x.</param>
        /// <param name="positionRow">The position y.</param>
        /// <param name="explosionPower">The explosion power.</param>
        private void DetonateNearbyFields(Field[,] gameField, int positionRow, int positionCol, int explosionPower)
        {
            int cols = gameField.GetLength(0);
            int rows = gameField.GetLength(1);

            for (int power = 0; power < explosionPower; power++)
            {
                foreach (int[] relativePosition in explosionRangePositionsGroupedByPower[power])
                {
                    int col = positionCol + relativePosition[0];
                    int row = positionRow + relativePosition[1];

                    if (0 > col || col >= cols || 
                        0 > row || row >= rows)
                    {
                        continue;
                    }

                    Field fieldToDetonate = gameField[row, col];
                    if (fieldToDetonate.Condition == Condition.Bomb)
                    {
                        userInterface.ShowNote("A bomb was detonated by chain reaction.");
                        OnBombBlownUpEvent();
                    }
                    fieldToDetonate.DetonateByChainReaction();
                }

            }
        }

        /// <summary>
        /// Tries the get coordinates.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="cols">The size x.</param>
        /// <param name="rows">The size y.</param>
        /// <param name="col">The coord x.</param>
        /// <param name="row">The coord y.</param>
        /// <returns></returns>
        private bool TryGetCoords(string input, int rows, int cols, out int row, out int col)
        {
            col = 0;
            row = 0;

            if (input.IndexOf(" ") == -1)
            {
                return false;
            }

            string inputRow = input.Substring(0, input.IndexOf(" "));
            string inputCol = input.Substring(input.IndexOf(" ") + 1);

            int c = 0;
            int r = 0;

            if (!int.TryParse(inputCol, out c) ||
                !int.TryParse(inputRow, out r))
            {
                return false;
            }

            if (0 > c || c >= cols ||
                0 > r || r >= rows)
            {
                return false;
            }

            col = c;
            row = r;

            return true;
        }

        private char[,] StringifyGameField()
        {
            int size = this.gameField.GetLength(0);
            char[,] stringifyedBattleField = new char[size, size];
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    char ch = ' ';
                    if (this.gameField[r, c].Condition == Condition.Empty)
	                {
		                ch = '-';
	                }
                    else if (this.gameField[r, c].Condition == Condition.BlownUp)
	                {
		                ch = 'X';
	                }
                    else
	                {
		                ch = this.gameField[r, c].ExplosivePower.ToString()[0];
	                }

                    stringifyedBattleField[r, c] = ch;
                }
            }
            return stringifyedBattleField;
        }

        /// <summary>
        /// Keeps count of the bombs left.
        /// </summary>
        private void OnBombBlownUpEvent()
        {
            BombsCount--;
        }

        #region MethodsForInitializingBattleField7Game

        /// <summary>
        /// Initializes the game field.
        /// </summary>
        /// <param name="col">The size x.</param>
        /// <param name="row">The size y.</param>
        /// <returns>The game field</returns>
        private Field[,] InitializeGameField(int col, int row)
        {
            Random rnd = new Random();

            int maxSize = col * row;
            List<int> bombPositions = new List<int>();
            for (int i = 0; i < BombsCount; i++)
            {
                int position = rnd.Next(0, maxSize);
                if (!bombPositions.Contains(position))
                {
                    bombPositions.Add(position);
                }
                else
                {
                    i--;
                    continue;
                }
            }

            Field[,] gameField = new Field[row, col];
            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    if (!bombPositions.Contains(c * row + r))
                    {
                        gameField[r, c] = new Field(Condition.Empty, 0);
                    }
                    else
                    {
                        gameField[r, c] = new Field(Condition.Bomb, rnd.Next(1, 6));
                    }
                }
            }
            return gameField;
        }

        /// <summary>
        /// Sets the size of game field.
        /// </summary>
        private void SetSizeOfGameField()
        {
            int size;
            bool stringInputIsInt = int.TryParse(userInterface.AskForSizeInput("Input game field size (1-10): "), out size);
            while (true)
            {
                if (!stringInputIsInt
                || size <= 0
                || size > 10)
                {
                    if (this.Logger != null)
                    {
                        this.Logger.LogEvent("Invalid size input attempt");
                    }
                    userInterface.ShowNote("Bad input - try again.");
                    stringInputIsInt = int.TryParse(userInterface.AskForSizeInput("Input game field size (1-10): "), out size);
                    continue;
                }
                break;
            }

            cols = size;
            rows = size;
            BombsCount = (int)(size * size * bombsFrequency);
        }

        /// <summary>
        /// Initializes the explosion ranges.
        /// ExplosionRanges holds the reletive positions to detonate for each explosion power
        /// </summary>
        private void InitializeExplosionRanges() // hard coded, but easy to understand and change
        {
            List<int[]> powerLevelOne = new List<int[]>();
            powerLevelOne.Add(new int[] { 1, 1 });
            powerLevelOne.Add(new int[] { -1, 1 });
            powerLevelOne.Add(new int[] { 1, -1 });
            powerLevelOne.Add(new int[] { -1, -1 });

            List<int[]> powerLevelTwo = new List<int[]>();
            powerLevelTwo.Add(new int[] { 1, 0 });
            powerLevelTwo.Add(new int[] { 0, 1 });
            powerLevelTwo.Add(new int[] { -1, 0 });
            powerLevelTwo.Add(new int[] { 0, -1 });

            List<int[]> powerLevelThree = new List<int[]>();
            powerLevelThree.Add(new int[] { 2, 0 });
            powerLevelThree.Add(new int[] { 0, 2 });
            powerLevelThree.Add(new int[] { -2, 0 });
            powerLevelThree.Add(new int[] { 0, -2 });

            List<int[]> powerLevelFour = new List<int[]>();
            powerLevelFour.Add(new int[] { 2, 1 });
            powerLevelFour.Add(new int[] { -2, 1 });
            powerLevelFour.Add(new int[] { 2, -1 });
            powerLevelFour.Add(new int[] { -2, -1 });
            powerLevelFour.Add(new int[] { 1, 2 });
            powerLevelFour.Add(new int[] { -1, 2 });
            powerLevelFour.Add(new int[] { 1, -2 });
            powerLevelFour.Add(new int[] { -1, -2 });

            List<int[]> powerLevelFive = new List<int[]>();
            powerLevelFive.Add(new int[] { 2, 2 });
            powerLevelFive.Add(new int[] { -2, 2 });
            powerLevelFive.Add(new int[] { 2, -2 });
            powerLevelFive.Add(new int[] { -2, -2 });

            explosionRangePositionsGroupedByPower = new List<List<int[]>>();
            explosionRangePositionsGroupedByPower.Add(powerLevelOne);
            explosionRangePositionsGroupedByPower.Add(powerLevelTwo);
            explosionRangePositionsGroupedByPower.Add(powerLevelThree);
            explosionRangePositionsGroupedByPower.Add(powerLevelFour);
            explosionRangePositionsGroupedByPower.Add(powerLevelFive);
        }
        
        #endregion
    }
}