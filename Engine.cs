using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleFieldNamespace
{
    class Engine {
    readonly double bombsFrequency = 0.15;
	int bombsCount;
	int sizeX;
	int sizeY;
    int steps = 0;

	void Run(Drawer drawer)
	{
        // TODO - attach Engine.onBombBlownUpEvent() to the event thrown by the exploded Fields;
        // not sure how though...

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
		while(bombsCount > 0)
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
            Field selectedField = new Field(Condition.Empty,0);

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
        // TODO - Implement Engine.DetonateNearbyFields();
        throw new NotImplementedException("Engine.DetonateNearbyFields() is not implemented");

        //foreach (field in reach of the explosion)
        //{
        //    field.DetonateByChainReaction();
        //}
	}

	void onBombBlownUpEvent()
    {
		bombsCount--;
	}

    object InitializeGameField(int bombsCount, int sizeX, int sizeY)
    {
        // TODO - Implement Engine.InitializeGameField();
        throw new NotImplementedException("Engine.InitializeGameField() is not implemented");
    }

    bool TryGetCoords(string inpit, int sizeX, int sizeY, out int coordX, out int coordY)
    {
        // TODO - Implement Engine.TryGetCoords();
        throw new NotImplementedException("Engine.TryGetCoords() is not implemented");
    }
 }
}
