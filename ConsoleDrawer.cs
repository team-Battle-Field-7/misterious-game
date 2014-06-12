using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleFieldNamespace
{
    class ConsoleDrawer : Drawer
    {
        // I'm still not sure what the gameField will be, so I simply wrote 'object'.
        // TODO - implement ConsoleDrawer.DrawGame();
        public override void DrawGame(object gameField)
        {
            throw new NotImplementedException("ConsoleDrawer.DrawGame() is not implemented");
        }

        // TODO - implement ConsoleDrawer.AskForImput();
        public override string AskForImput()
        {
            throw new NotImplementedException("ConsoleDrawer.AskForImput() is not implemented");
        }

        // TODO - implement ConsoleDrawer.ShowMessage();
        public override void ShowMessage(string message)
        {
            throw new NotImplementedException("ConsoleDrawer.ShowMessage() is not implemented");
        }
    }
}
