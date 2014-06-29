using System;
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
        readonly double bombsFrequency = 0.15;
        int bombsCount;
        int cols;
        int rows;

        List<List<int[]>> explosionRangePositionsGroupedByPower;

        /// <summary>
        /// Runs the game.
        /// </summary>
        /// <param name="drawer">The drawer.</param>
        /// <exception cref="System.ArgumentException">Size input must be a number between 1 and 10;size</exception>
        public void RunGame(Drawer drawer)
        {
            InitializeExplosionRanges();

            drawer.ShowMessage("Input game field size (1-10): ");
            int size;
            bool stringInputIsInt = int.TryParse(drawer.AskForInput(), out size);
            while (true)
            {
                if (!stringInputIsInt
                || size < 0
                || size > 10)
                {
                    drawer.ShowMessage("bad input - try again. Input game field size (1-10): ");
                    stringInputIsInt = int.TryParse(drawer.AskForInput(), out size);
                    continue;
                }
                break;
            }
            

            cols = size;
            rows = size;
            bombsCount = (int)(size * size * bombsFrequency);

            Field[,] gameField = InitializeGameField(bombsCount, cols, rows);

            drawer.DrawGame(gameField);

            int turnsCount = 0;
            while (bombsCount > 0)
            {
                // TODO - write a propper message for that case
                drawer.ShowMessage("Give me INPUT!!! ( in format 'row col' ): ");
                string input = drawer.AskForInput();

                int row;
                int col;
                bool inputValid = TryGetCoords(input, rows, cols, out row, out col);

                if (!inputValid)
                {
                    // TODO - write a propper message for that case
                    drawer.ShowMessage("The input sucks! Give me another!!!");
                    continue;
                }

                turnsCount++;

                Field selectedField = gameField[row, col];

                int explosionPower = selectedField.IntentionalDetonate();
                if (explosionPower > 0)
                {
                    OnBombBlownUpEvent();
                    DetonateNearbyFields(gameField, row, col, explosionPower);
                }
                drawer.DrawGame(gameField);
            }
            // TODO - write a propper message for that case
            drawer.ShowCongratulations("You beat the game in " + turnsCount + " turns. Congrats!");
            
            // not sure if the game should restart now.
            // TODO - Decide weather to reset the game or not.
            Console.WriteLine("press enter to go on");
            Console.ReadLine();
        }

        /// <summary>
        /// Detonates the nearby fields after explosion.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        /// <param name="positionCol">The position x.</param>
        /// <param name="positionRow">The position y.</param>
        /// <param name="explosionPower">The explosion power.</param>
        void DetonateNearbyFields(Field[,] gameField, int positionRow, int positionCol, int explosionPower)
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
        bool TryGetCoords(string input, int rows, int cols, out int row, out int col)
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
        void OnBombBlownUpEvent()
        {
            bombsCount--;
        }

        #region MethodsForInitializingBattleField7Game

        /// <summary>
        /// Initializes the game field.
        /// </summary>
        /// <param name="bombsCount">The bombs count.</param>
        /// <param name="col">The size x.</param>
        /// <param name="row">The size y.</param>
        /// <returns></returns>
        Field[,] InitializeGameField(int bombsCount, int col, int row)
        {
            Random rnd = new Random();

            int maxSize = col * row;
            List<int> bombPositions = new List<int>();
            for (int i = 0; i < bombsCount; i++)
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
        /// Initializes the explosion ranges.
        /// ExplosionRanges holds the reletive positions to detonate for each explosion power
        /// </summary>
        void InitializeExplosionRanges()// hard coded, but easy to understand and change
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
