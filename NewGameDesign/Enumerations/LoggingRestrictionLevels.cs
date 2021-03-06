﻿using System;
using System.Linq;

namespace BattleField7Namespace.NewGameDesign.Enumerations
{
    /// <summary>
    /// Enumeration of the possible restriction levels for the Logger.
    /// </summary>
    public enum LoggingRestrictionLevels
    {
        /// <summary>
        /// The lowest restriction level - everything is to be logged.
        /// </summary>
        All = 1,

        /// <summary>
        /// Event logs are to be ignored. Only StartUp, Warning and Erorr logs will be logged.
        /// </summary>
        StartUps = 2,

        /// <summary>
        /// Event and StartUp logs are to be ignored. Only Warning and Error logs will be logged.
        /// </summary>
        Warnings = 3,

        /// <summary>
        /// Event, StartUp and Warning logs are to be ignored. Only Error logs will be logged.
        /// </summary>
        Errors = 4
    }
}
