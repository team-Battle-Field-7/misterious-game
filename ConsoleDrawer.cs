using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace
{
    class ConsoleDrawer : Drawer
    {
        // TODO - implement ConsoleDrawer.DrawGame();
        /// <summary>
        /// Draws the game in the console.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        /// <exception cref="System.NotImplementedException">ConsoleDrawer.DrawGame() is not implemented</exception>
        public override void DrawGame(Field[,] gameField)
        {
            throw new NotImplementedException("ConsoleDrawer.DrawGame() is not implemented");
        }

        // TODO - implement ConsoleDrawer.AskForInput();
        public override string AskForInput()
        {
            throw new NotImplementedException("ConsoleDrawer.AskForInput() is not implemented");
        }

        // TODO - implement ConsoleDrawer.ShowMessage();
        public override void ShowMessage(string message)
        {
            throw new NotImplementedException("ConsoleDrawer.ShowMessage() is not implemented");
        }

        // TODO - implement ConsoleDrawer.ShowCongratulations();
        public override void ShowCongratulations(string message)
        {
            throw new NotImplementedException("ConsoleDrawer.ShowCongratulations() is not implemented");
        }

        // TODO - implement ConsoleDrawer.ShowGameOver();
        public override void ShowGameOver(string message)
        {
            throw new NotImplementedException("ConsoleDrawer.ShowGameOver() is not implemented");
        }

        // TODO - implement ConsoleDrawer.ShowNote(();
        public override void ShowNote(string message)
        {
            throw new NotImplementedException("ConsoleDrawer.ShowNote() is not implemented");
        }
    }
}
