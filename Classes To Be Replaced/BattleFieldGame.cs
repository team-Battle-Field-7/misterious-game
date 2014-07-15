using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace BattleField7Namespace
{
    public class BattleFieldGame
    {
        /// <summary>
        /// The logger
        /// </summary>
        private ILogger logger;

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public ILogger Logger
        {
            get
            {
                return this.logger;
            }
            set
            {
                this.logger = value;
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
