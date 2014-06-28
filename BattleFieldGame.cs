using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BattleField7Namespace
{
    class BattleFieldGame
    {
        /// <summary>
        /// Simply calls the engine and runs it.
        /// </summary>
        static void Main()
        {
            ConsoleDrawer consoleDrawer = new ConsoleDrawer();
            Engine engine = new Engine();
            engine.RunGame(consoleDrawer);

            // TODO - When all the classes are compleated, remove this and replace it with Engine initialization and Engine.Run()
            //BattleGame battleFieldGameOld = new BattleGame();
            //battleFieldGameOld.Start();
        }
    }
}
