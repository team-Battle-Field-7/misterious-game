using System;
using System.Linq;

namespace BattleField7Namespace.NewGameDesign.Interfaces
{
    /// <summary>
    /// Observes one or more ICountNotifier-s
    /// </summary>
    public interface ICountObserver
    {
        /// <summary>
        /// Observes the specified notifyer.
        /// </summary>
        /// <param name="notifyer">The notifyer.</param>
        void Observe(ICountNotifier notifyer);

        /// <summary>
        /// Updates the count.
        /// </summary>
        /// <param name="count">The count.</param>
        void UpdateCount(int count);
    }
}
