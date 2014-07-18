using System;
using System.Linq;
using System.IO;
using BattleField7Namespace.NewGameDesign.Interfaces;
using BattleField7Namespace.NewGameDesign.Enumerations;

namespace BattleField7Namespace.NewGameDesign.UIClasses
{
    /// <summary>
    /// Class used for logging to a text file.
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// The restriction level of the logger.
        /// </summary>
        private LoggingRestrictionLevels restrictionLevel = LoggingRestrictionLevels.All;

        /// <summary>
        /// The file path of the logger. Only *.txt paths are acceptable.
        /// </summary>
        private string filePath = "loggs.txt"; //just "logs.txt" maybe?

        /// <summary>
        /// Gets or sets the restriction level.
        /// </summary>
        /// <value>
        /// The restriction level.
        /// </value>
        public LoggingRestrictionLevels RestrictionLevel
        {
            get 
            {
                return this.restrictionLevel;
            }
            set 
            {
                this.restrictionLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        /// <exception cref="System.ArgumentException">Logger.FilePath must be a path to a .txt file.  + value + is not a valid FilePath.</exception>
        public string FilePath
        {
            get
            {
                return this.filePath;
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
                this.filePath = value;
            }
        }


        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Log(string message)
        {
            using (FileStream fs = new FileStream(this.FilePath, FileMode.Append, FileAccess.Write))
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
        public void LogError(string error)
        {
            if (RestrictionLevel <= LoggingRestrictionLevels.Errors)
            {
                this.Log("[Error] " + error);
            }
        }

        /// <summary>
        /// Logs the warning.
        /// </summary>
        /// <param name="warning">The warning.</param>
        public void LogWarning(string warning)
        {
            if (RestrictionLevel <= LoggingRestrictionLevels.Warnings)
            {
                this.Log("[Warning] " + warning);
            }
        }

        /// <summary>
        /// Logs the start up event.
        /// </summary>
        public void LogStartUp()
        {
            if (RestrictionLevel <= LoggingRestrictionLevels.StartUps)
            {
                this.Log("[Program Started]");
            }
        }

        /// <summary>
        /// Logs the event.
        /// </summary>
        /// <param name="eventMsg">The event message.</param>
        public void LogEvent(string eventMsg)
        {
            if (RestrictionLevel <= LoggingRestrictionLevels.All)
            {
                this.Log("[Event] " + eventMsg);
            }
        }
    }
}
