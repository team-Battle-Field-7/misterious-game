using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace
{
    class Engine
    {
        readonly double bombsFrequency = 0.15;
        int bombsCount;
        int sizeX;
        int sizeY;
        int steps = 0;

        List<List<int[]>> explosionRangePositionsGroupedByPower;

        void Run(Drawer drawer)
        {
            // TODO - attach Engine.onBombBlownUpEvent() to the event thrown by the exploded Fields;
            // not sure how though...

            InitializeExplosionRanges();

            drawer.ShowMessage("input size of of game field:");
            int size;
            bool stringInputIsInt = int.TryParse(drawer.AskForImput(), out size);
            if (!stringInputIsInt 
                || size < 0 
                || size > 10)
            {
                throw new ArgumentException(
                    "Invalid size input", 
                    "drawer.AskForImput();"
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
                string input = drawer.AskForImput();

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

                int explosionPower = selectedField.DetonateIntentional();
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

            coordX = x;
            coordY = y;

            return true;
        }

        void onBombBlownUpEvent()
        {
            bombsCount--;
        }

        #region MethodsForInitializingBattleField7Game

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

        // hard coded, but easy to understand and change
        void InitializeExplosionRanges()
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
