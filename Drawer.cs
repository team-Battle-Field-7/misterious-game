using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleFieldNamespace
{
    abstract class Drawer
    {
        public abstract void DrawGame(object gameField);
        // I'm still not sure what the gameField will be, so I simply wrote 'object'.

        public abstract string AskForImput();

        public abstract void ShowMessage(string message);
    }
}
