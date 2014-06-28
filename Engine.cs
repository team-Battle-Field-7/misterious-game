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
        int sizeX;
        int sizeY;
        int steps = 0;

        List<List<int[]>> explosionRangePositionsGroupedByPower;

        /// <summary>
        /// Runs the game.
        /// </summary>
        /// <param name="drawer">The drawer.</param>
        /// <exception cref="System.ArgumentException">Size input must be a number between 1 and 10;size</exception>
        public void RunGame(Drawer drawer)
        {
            // TODO - attach Engine.onBombBlownUpEvent() to the event thrown by the exploded Fields;
            // not sure how though...

            InitializeExplosionRanges();

            drawer.ShowMessage("input size of of game field:");
            int size;
            bool stringInputIsInt = int.TryParse(drawer.AskForInput(), out size);
            if (!stringInputIsInt 
                || size < 0 
                || size > 10)
            {
                throw new ArgumentException(
                    "Size input must be a number between 1 and 10", 
                    "size"
                    );
            }

            sizeX = size;
            sizeY = size;
            bombsCount = (int)(size * size * bombsFrequency);

            Field[,] gameField = InitializeGameField(bombsCount, sizeX, sizeY);

            drawer.DrawGame(gameField);

            int turnsCount = 0;
            while (bombsCount > 0)
            {
                // TODO - write a propper message for that case
                drawer.ShowMessage("Give me INPUT!!!");
                string input = drawer.AskForInput();

                int coordX;
                int coordY;
                bool inputValid = TryGetCoords(input, sizeX, sizeY, out coordX, out coordY);

                if (!inputValid)
                {
                    // TODO - write a propper message for that case
                    drawer.ShowMessage("The input sucks! Give me another!!!");
                    continue;
                }

                steps++;

                Field selectedField = gameField[coordX, coordY];

                int explosionPower = selectedField.IntentionalDetonate();
                if (explosionPower > 0)
                {
                    DetonateNearbyFields(gameField, coordX, coordY, explosionPower);

                    // TODO - write a propper message for that case
                    drawer.ShowNote("Gues I should tell you that a bomb has been blown up, shouldn't I?");
                }
                drawer.DrawGame(gameField);
            }
            // TODO - write a propper message for that case
            drawer.ShowCongratulations("You beat the game in " + turnsCount + " turns. Congrats!");
            
            // not sure if the game should restart now.
            // TODO - Decide weather to reset the game or not.
        }

        /// <summary>
        /// Detonates the nearby fields after explosion.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        /// <param name="positionX">The position x.</param>
        /// <param name="positionY">The position y.</param>
        /// <param name="explosionPower">The explosion power.</param>
        void DetonateNearbyFields(Field[,] gameField, int positionX, int positionY, int explosionPower)
        {
            int sizeX = gameField.GetLength(0);
            int sizeY = gameField.GetLength(1);

            for (int power = 0; power < explosionPower; power++)
            {
                foreach (int[] relativePosition in explosionRangePositionsGroupedByPower[power])
                {
                    int coordX = positionX + relativePosition[0];
                    int coordY = positionX + relativePosition[1];

                    if (0 > coordX || coordX >= sizeX || 
                        0 > coordY || coordY >= sizeY)
                    {
                        continue;
                    }

                    Field fieldToDetonate = gameField[coordX, coordY];
                    fieldToDetonate.DetonateByChainReaction();
                }

            }
        }

        /// <summary>
        /// Tries the get coordinates.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="sizeX">The size x.</param>
        /// <param name="sizeY">The size y.</param>
        /// <param name="coordX">The coord x.</param>
        /// <param name="coordY">The coord y.</param>
        /// <returns></returns>
        bool TryGetCoords(string input, int sizeX, int sizeY, out int coordX, out int coordY)
        {
            coordX = 0;
            coordY = 0;

            if (input.IndexOf(" ") == -1)
            {
                return false;
            }

            string inputX = input.Substring(0, input.IndexOf(" "));
            string inputY = input.Substring(input.IndexOf(" ") + 1);

            int x = 0;
            int y = 0;

            if (!int.TryParse(inputX, out x) ||
                !int.TryParse(inputY, out y))
            {
                return false;
            }

            if (0 < x || x >= sizeX ||
                0 < y || y >= sizeY)
            {
                return false;
            }

            coordX = x;
            coordY = y;

            return true;
        }

        /// <summary>
        /// Activates on the BombBlownUp event. Counts the detonated bombs.
        /// </summary>
        void onBombBlownUpEvent()
        {
            bombsCount--;
        }

        #region MethodsForInitializingBattleField7Game

        /// <summary>
        /// Initializes the game field.
        /// </summary>
        /// <param name="bombsCount">The bombs count.</param>
        /// <param name="sizeX">The size x.</param>
        /// <param name="sizeY">The size y.</param>
        /// <returns></returns>
        Field[,] InitializeGameField(int bombsCount, int sizeX, int sizeY)
        {
            Random rnd = new Random();

            int maxSize = sizeX * sizeY;
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

            Field[,] gameField = new Field[sizeX, sizeY];
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    if (!bombPositions.Contains(x * sizeY + y))
                    {
                        gameField[x, y] = new Field(Condition.Empty, 0);
                    }
                    else
                    {
                        gameField[x, y] = new Field(Condition.Bomb, rnd.Next(1, 6));
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
