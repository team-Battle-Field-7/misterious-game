using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using BattleField7Namespace.NewGameDesign.Interfaces;
using BattleField7Namespace.NewGameDesign.GameClasses;

namespace BattleField7Namespace
{
    /// <summary>
    /// Knows how to initialize every version of the BattleField7 Game.
    /// </summary>
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

        /// <summary>
        /// Runs the new design version of the came in the console.
        /// </summary>
        public void RunTheNewDesignVersionInTheConsole()
        {
            ConsoleUI consoleUI = new ConsoleUI();
            SimpleBattleField battleField = new SimpleBattleField(
                                                new SimpleField(Condition.Empty, 0),
                                                new MyExplosionStrategy());

            SimpleEngine engine = new SimpleEngine();
            engine.Logger = this.logger;
            engine.RunGame(consoleUI, battleField);
        }

        /// <summary>
        /// Runs the new design version of the came in Windows Forms.
        /// </summary>
        public void RunTheNewDesignVersionInWinForms()
        {


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            WinFormsUI form = new WinFormsUI();
            SimpleBattleField battleField = new SimpleBattleField(
                                                new SimpleField(Condition.Empty, 0),
                                                new MyExplosionStrategy());

            Thread formThread = new Thread(
                delegate()
                {
                    SimpleEngine engine = new SimpleEngine();
                    engine.Logger = this.logger;
                    engine.RunGame(form, battleField);
                }
            );
            formThread.Start();
            Application.Run(form);
        }
    }
}
