using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BattleField7Namespace.NewGameDesign.Interfaces
{
    /// <summary>
    /// Interface used for logging.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Log(string message);

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="error">The error.</param>
        void LogError(string error);

        /// <summary>
        /// Logs the warning.
        /// </summary>
        /// <param name="warning">The warning.</param>
        void LogWarning(string warning);


        /// <summary>
        /// Logs the start up event.
        /// </summary>
        void LogStartUp();

        /// <summary>
        /// Logs the event.
        /// </summary>
        /// <param name="eventMsg">The event message.</param>
        void LogEvent(string eventMsg);
    }
}
