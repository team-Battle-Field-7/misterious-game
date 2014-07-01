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
            BattleFieldGame game = new BattleFieldGame();
            //game.RunOldVersion();
            //game.RunNewConsoleVersion();
            game.RunNewWinFormsVersion();
        }

        /// <summary>
        /// Runs the old version of the game.
        /// </summary>
        public void RunOldVersion()
        {
            InitialGameClass.BattleGame battleFieldGameOld = new InitialGameClass.BattleGame();
            battleFieldGameOld.Start();
        }

        /// <summary>
        /// Runs the new console version of the game.
        /// </summary>
        public void RunNewConsoleVersion()
        {
            ConsoleDrawer consoleDrawer = new ConsoleDrawer();
            Engine engine = new Engine(consoleDrawer);
            engine.RunGame();
        }

        /// <summary>
        /// Runs the new windows forms version of the game.
        /// </summary>
        public void RunNewWinFormsVersion()
        {
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
        }
    }
}
