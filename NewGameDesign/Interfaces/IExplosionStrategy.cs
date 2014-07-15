using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace.NewGameDesign.Interfaces
{
    interface IExplosionStrategy
    {
        IList<int[]> GetCoordsToDetonateByTheBlast(int row, int col, int explosivePower);
    }
}
