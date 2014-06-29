using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;

namespace BattleField7Namespace
{
    class BattleFieldGame
    {
        private delegate void runFormCallback();

        /// <summary>
        /// Simply calls the engine and runs it.
        /// </summary>
        static void Main()
        {
            //// this is the new console version.
            //ConsoleDrawer consoleDrawer = new ConsoleDrawer();
            //Engine engine = new Engine();
            //engine.RunGame(consoleDrawer);

            // this is the newest windows forms version
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            WinFormsDrawer form = new WinFormsDrawer();
            Thread formThread = new Thread(
                delegate()
                {
                    Engine engine = new Engine(form);
                    engine.RunGame();
                }
            );
            formThread.Start();
            Application.Run(form);

            

            //// this is the old version. It's kept here for testing
            //BattleGame battleFieldGameOld = new BattleGame();
            //battleFieldGameOld.Start();
        }
    }
}
