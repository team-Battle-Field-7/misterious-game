using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BattleFieldNamespace
{
    /// <summary>
    /// Ако това ще е енджин на играта, няма да я наследява, а ще извиква <c>Init()</c> и <c>Start()</c> от нея.
    /// Методите, които са private в класа Battlegame са само такива, използвани помощно от дрги методи //vnci    /// 
    /// </summary>
    class BattleField : BattleGame
    {
        
        static void Main(string[] args)
        {
            BattleField BF = new BattleField();

            BF.Start();
        }
    }
}
