using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace BattleField7Namespace
{
    public class BattleFieldGame
    {
        private ILogger logger;

        /// <summary>
        /// Simply calls the engine and runs it.
        /// </summary>
        static void Main()
        {
            BattleFieldGame game = new BattleFieldGame();
            game.logger = new Logger();

            try
            {
                //game.RunOldVersion();
                //game.RunNewConsoleVersion();
                game.RunNewWinFormsVersion();
            }
            catch (Exception ex)
            {
                if (game.logger != null)
                {
                    game.logger.LogError(ex.ToString());
                }
                throw;
            }
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
            ConsoleUI consoleDrawer = new ConsoleUI();
            Engine engine = new Engine(consoleDrawer);
            engine.Logger = this.logger;
            engine.RunGame();
        }

        /// <summary>
        /// Runs the new windows forms version of the game.
        /// </summary>
        public void RunNewWinFormsVersion()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            WinFormsUI form = new WinFormsUI();
            Thread formThread = new Thread(
                delegate()
                {
                    Engine engine = new Engine(form);
                    engine.Logger = this.logger;
                    engine.RunGame();
                }
            );
            formThread.Start();
            Application.Run(form);
        }
    }
}
