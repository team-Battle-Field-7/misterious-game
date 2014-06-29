﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace
{
    /// <summary>
    /// Engine that runs the Battle Field 7 game
    /// </summary>
    class Engine
    {
        private readonly double bombsFrequency = 0.15;
        private int bombsCount;
        private int turnsCount;
        private int cols;
        private int rows;
        private IDrawer drawer;
        private Field[,] gameField;

        private List<List<int[]>> explosionRangePositionsGroupedByPower;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        /// <param name="drawer">The drawer.</param>
        public Engine(IDrawer drawer)
        {
            this.drawer = drawer;
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
                drawer.ShowTurnsCount(this.turnsCount);
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
                drawer.ShowBombsCount(this.BombsCount);
            }
        }
        /// <summary>
        /// Runs the game.
        /// </summary>
        /// <param name="drawer">The drawer.</param>
        /// <exception cref="System.ArgumentException">Size input must be a number between 1 and 10;size</exception>
        public void RunGame()
        {
            InitializeExplosionRanges();
            SetSizeOfGameField();
            gameField = InitializeGameField(cols, rows);
            StartGame();

            // TODO - write a propper message for that case
            drawer.ShowCongratulations("You beat the game in " + turnsCount + " turns. Congrats!");
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        private void StartGame()
        {
            drawer.DrawGame(gameField);
            TurnsCount = 0;
            while (BombsCount > 0)
            {
                // TODO - write a propper message for that case
                drawer.ShowAskInput("Give me INPUT!!! ( in format 'row col' ): ");
                string input = drawer.AskForPositionInput();

                int row;
                int col;
                bool inputValid = TryGetCoords(input, rows, cols, out row, out col);

                if (!inputValid)
                {
                    // TODO - write a propper message for that case
                    drawer.ShowNote("The input sucks! Give me another!!!");
                    continue;
                }

                TurnsCount++;

                Field selectedField = gameField[row, col];

                int explosionPower = selectedField.IntentionalDetonate();
                if (explosionPower > 0)
                {
                    OnBombBlownUpEvent();
                    DetonateNearbyFields(gameField, row, col, explosionPower);
                    drawer.ShowNote("You hit a bomb with power level of " + explosionPower);
                }
                drawer.DrawGame(gameField);
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
                        drawer.ShowNote("A bomb was detonated by chain reaction.");
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
        /// <param name="bombsCount">The bombs count.</param>
        /// <param name="col">The size x.</param>
        /// <param name="row">The size y.</param>
        /// <returns></returns>
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
            drawer.ShowAskInput("Input game field size (1-10): ");
            int size;
            bool stringInputIsInt = int.TryParse(drawer.AskForSizeInput(), out size);
            while (true)
            {
                if (!stringInputIsInt
                || size < 0
                || size > 10)
                {
                    drawer.ShowNote("Bad input - try again.");
                    drawer.ShowAskInput("Input game field size (1-10): ");
                    stringInputIsInt = int.TryParse(drawer.AskForSizeInput(), out size);
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
        private void InitializeExplosionRanges()// hard coded, but easy to understand and change
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
