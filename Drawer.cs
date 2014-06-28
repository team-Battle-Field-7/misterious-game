using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace
{
    abstract class Drawer
    {
        public abstract void DrawGame(Field[,] gameField);

        public abstract string AskForImput();

        public abstract void ShowMessage(string message);
        public abstract void ShowCongratulations(string message);
        public abstract void ShowGameOver(string message);
        public abstract void ShowNote(string message);
    }
}
