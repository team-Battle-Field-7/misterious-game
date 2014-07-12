using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BattleField7Namespace
{
    /// <summary>
    /// Static class used for logging to a text file.
    /// </summary>
    internal static class Logger
    {
        /// <summary>
        /// The restriction level of the logger.
        /// </summary>
        private static LoggingRestrictionLevels restrictionLevel = LoggingRestrictionLevels.All;

        /// <summary>
        /// The file path of the logger. Only *.txt paths are acceptable.
        /// </summary>
        private static string filePath = "loggs.txt";

        /// <summary>
        /// Gets or sets the restriction level.
        /// </summary>
        /// <value>
        /// The restriction level.
        /// </value>
        internal static LoggingRestrictionLevels RestrictionLevel
        {
            get 
            {
                return Logger.restrictionLevel;
            }
            set 
            {
                Logger.restrictionLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        /// <exception cref="System.ArgumentException">Logger.FilePath must be a path to a .txt file.  + value + is not a valid FilePath.</exception>
        internal static string FilePath
        {
            get
            {
                return Logger.filePath;
            }
            set
            {
                if (value.Substring(value.LastIndexOf(".")) != ".txt")
                {
                    throw new ArgumentException("Logger.FilePath must be a path to a .txt file. " + value + "is not a valid FilePath.");
                }
                if (!File.Exists(value))
                {
                    File.Create(value);
                }
                Logger.filePath = value;
            }
        }


        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        internal static void Log(string message)
        {
            using (FileStream fs = new FileStream(Logger.FilePath, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                string logTime = "[" + DateTime.Now.ToString("MM/dd/yy HH:mm:ss") + "]";
                sw.WriteLine(logTime + " " + message);
            }
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="error">The error.</param>
        internal static void LogError(string error)
        {
            if (RestrictionLevel <= LoggingRestrictionLevels.Errors)
            {
                Logger.Log("[Error] " + error);
            }
        }

        /// <summary>
        /// Logs the warning.
        /// </summary>
        /// <param name="warning">The warning.</param>
        internal static void LogWarning(string warning)
        {
            if (RestrictionLevel <= LoggingRestrictionLevels.Warnings)
            {
                Logger.Log("[Warning] " + warning);
            }
        }

        /// <summary>
        /// Logs the start up event.
        /// </summary>
        internal static void LogStartUp()
        {
            if (RestrictionLevel <= LoggingRestrictionLevels.StartUps)
            {
                Logger.Log("[Program Started]");
            }
        }

        /// <summary>
        /// Logs the event.
        /// </summary>
        /// <param name="eventMsg">The event message.</param>
        internal static void LogEvent(string eventMsg)
        {
            if (RestrictionLevel <= LoggingRestrictionLevels.All)
            {
                Logger.Log("[Event] " + eventMsg);
            }
        }
    }
}
