using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// TODO - remove the line below when all testing is compleated
using BattleField7Namespace.InitialGameClass;

namespace BattleField7Namespace
{
    class BattleFieldGame
    {
        /// <summary>
        /// Simply calls the engine and runs it.
        /// </summary>
        static void Main()
        {
            // this is the new version.

            ConsoleDrawer consoleDrawer = new ConsoleDrawer();
            Engine engine = new Engine();
            engine.RunGame(consoleDrawer);

            // this is the old version. It's kept here for testing

            //BattleGame battleFieldGameOld = new BattleGame();
            //battleFieldGameOld.Start();
        }
    }
}
