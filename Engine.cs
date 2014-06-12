using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleFieldNamespace
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

            initializeExplosionRanges();

            // TODO - implement a proper validation of the input of size
            drawer.ShowMessage("input size of of game field:");
            int size = int.Parse(drawer.AskForImput());

            sizeX = size;
            sizeY = size;
            bombsCount = (int)(size * size * bombsFrequency);

            // TODO - Decide the Data Type of gameField
            object gameField = new Object();
            gameField = InitializeGameField(bombsCount, sizeX, sizeY);

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
                    drawer.ShowMessage("That sucks!");
                    continue;
                }

                steps++;

                // TODO - Select the field by it's coordinates (coordX and coordY)
                Field selectedField = new Field(Condition.Empty, 0);

                int explosionPower = selectedField.DetonateIntentional();
                if (explosionPower > 0)
                {
                    DetonateNearbyFields(gameField, coordX, coordY, explosionPower);

                    // TODO - write a propper message for that case
                    drawer.ShowMessage("Gues I should tell you that a bomb has been blown up, shouldn't I?");
                }
                drawer.DrawGame(gameField);
            }
            // TODO - write a propper message for that case
            drawer.ShowMessage("You beat the game in " + turnsCount + " turns. Congrats!");
            // not sure if the game should restart now.
        }

        void DetonateNearbyFields(object gameField, int positionX, int positionY, int explosionPower)
        {
            // TODO - initialize these size variables, using gameField
            throw new NotImplementedException("In Engine.DetonateNearbyFields() - sizes of gameField are not initialized");
            int sizeX = 0;
            int sizeY = 0;

            for (int power = 0; power < explosionPower; power++)
            {
                foreach (int[] relativePosition in explosionRangePositionsGroupedByPower[power])
                {
                    int coordX = positionX + relativePosition[0];
                    int coordY = positionX + relativePosition[1];

                    if (0 > coordX || coordX >= sizeX)
                    {
                        continue;
                    }
                    if (0 > coordY || coordY >= sizeY)
                    {
                        continue;
                    }

                    Field fieldToDetonate;

                    // TODO - Select The Field by the calculated coordinates
                    throw new NotImplementedException("In Engine.DetonateNearbyFields() - field to Detonate is not selected");

                    fieldToDetonate.DetonateByChainReaction();
                }

            }
        }

        bool TryGetCoords(string inpit, int sizeX, int sizeY, out int coordX, out int coordY)
        {
            // TODO - Implement Engine.TryGetCoords();
            throw new NotImplementedException("Engine.TryGetCoords() is not implemented");
        }

        void onBombBlownUpEvent()
        {
            bombsCount--;
        }

        #region InitializeBattleField7Game
        
        object InitializeGameField(int bombsCount, int sizeX, int sizeY)
        {
            // TODO - Implement Engine.InitializeGameField();
            throw new NotImplementedException("Engine.InitializeGameField() is not implemented");
        }

        void initializeExplosionRanges() // hard coded, but easy to change
        {
            List<int[]> powerLevelOne = new List<int[]>();
            powerLevelOne.Add(new int[] { 1, 1 });

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
