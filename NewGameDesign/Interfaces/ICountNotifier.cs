using System;
using System.Linq;

namespace BattleField7Namespace.NewGameDesign.Interfaces
{
    /// <summary>
    /// Notifies one or more attached ICountObserver-s
    /// </summary>
    public interface ICountNotifier
    {
        /// <summary>
        /// Attaches the observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        void AttachObserver(ICountObserver observer);

        /// <summary>
        /// Notifies the observer.
        /// </summary>
        void NotifyObserver();
    }
}
