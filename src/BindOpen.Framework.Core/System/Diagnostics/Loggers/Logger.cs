﻿using System;
using System.Collections.Generic;
using System.IO;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Configuration.Tasks;

namespace BindOpen.Framework.Core.System.Diagnostics.Loggers
{
    /// <summary>
    /// This class represents a logger.
    /// </summary>
    /// <remarks>The output format is YAML.</remarks>
    public abstract class Logger : NamedDataItem, ILogger
    {
        // ------------------------------------------------------
        // CONSTANTS
        // ------------------------------------------------------

        #region Constants

        /// <summary>
        /// The default name to consider.
        /// </summary>
        public const String __DefaultName = "standard";

        #endregion

        // ------------------------------------------------------
        // VARIABLES
        // ------------------------------------------------------

        #region Variables

        private String _FileName = null;
        private String _FolderPath = null;
        private Log _Log = null;

        #endregion

        // ------------------------------------------------------
        // PROPERTIES
        // ------------------------------------------------------

        #region Properties

        /// <summary>
        /// File path of this instance.
        /// </summary>
        public String Filepath
        {
            get
            {
                return (!string.IsNullOrEmpty(this._FolderPath) & !String.IsNullOrEmpty(this._FileName) ?
                    this.FolderPath + this._FileName : "").ToPath();
            }
        }

        /// <summary>
        /// Folder path of this instance.
        /// </summary>
        public String FolderPath
        {
            get
            {
                return this._FolderPath.GetEndedString(@"\").ToPath();
            }
        }

        /// <summary>
        /// The mode of this instance.
        /// </summary>
        public LoggerMode Mode { get; set; } = LoggerMode.Manual;

        /// <summary>
        /// The output kind of this instance.
        /// </summary>
        public DataSourceKind OutputKind { get; set; } = DataSourceKind.Repository;

        /// <summary>
        /// The format of this instance.
        /// </summary>
        public LogFormat Format { get; set; } = LogFormat.None;

        /// <summary>
        /// Indicates whether this instance is verbose.
        /// </summary>
        public Boolean IsVerbose { get; set; } = false;

        /// <summary>
        /// The UI culture of this instance.
        /// </summary>
        public String UICulture { get; set; } = null;

        /// <summary>
        /// The log of this instance.
        /// </summary>
        public Log Log
        {
            get
            {
                return this._Log;
            }
        }

        /// <summary>
        /// Function that filters event.
        /// </summary>
        public Predicate<LogEvent> EventFinder
        {
            get;
            set;
        }

        #endregion

        // ------------------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Logger class.
        /// </summary>
        public Logger()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Logger class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="logFormat">The log format to consider.</param>
        /// <param name="mode">The mode to consider.</param>
        /// <param name="outputKind">The output kind to consider.</param>
        /// <param name="isVerbose">Indicates whether .</param>
        /// <param name="uiCulture">The folder path to consider.</param>
        /// <param name="folderPath">The folder path to consider.</param>
        /// <param name="fileName">The file name to consider.</param>
        /// <param name="eventFinder">The function that filters event.</param>
        /// <param name="expirationDayNumber">The number of expiration days to consider.</param>
        /// <remarks>With expiration day number equaling to -1, no files expires. Equaling to 0, all files except the current one expires.</remarks>
        protected Logger(
            String name,
            LogFormat logFormat,
            LoggerMode mode,
            DataSourceKind outputKind,
            Boolean isVerbose =false,
            String uiCulture= null,
            String folderPath = null,
            String fileName = null,
            Predicate<LogEvent> eventFinder = null,
            int expirationDayNumber = -1) : this()
        {
            this.Name = name;
            this.Format = logFormat;
            this.Mode = mode;
            this.OutputKind = outputKind;
            this._FolderPath = folderPath;
            this.SetFileName(fileName);
            this.IsVerbose = isVerbose;
            this.UICulture = uiCulture;
            this.EventFinder = eventFinder;
            this.DeleteExpiredLogs(expirationDayNumber);
        }

        #endregion

        // ------------------------------------------------------
        // WRITING
        // ------------------------------------------------------

        #region Writing

        /// <summary>
        /// Logs the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        public virtual Boolean WriteLog(
            Log log)
        {
            return false;
        }

        /// <summary>
        /// Logs the specified task.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="task">The task to log.</param>
        public virtual Boolean WriteTask(
            Log log, TaskConfiguration task)
        {
            return false;
        }

        /// <summary>
        /// Logs the specified event.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="logEvent">The log event to consider.</param>
        public virtual Boolean WriteEvent(
            LogEvent logEvent)
        {
            return false;
        }

        /// <summary>
        /// Logs the specified element.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="elementName">The element name to consider.</param>
        /// <param name="elementValue">The element value to consider.</param>
        public virtual Boolean WriteDetailElement(
            Log log,
            String elementName,
            Object elementValue)
        {
            return false;
        }

        /// <summary>
        /// Logs the specified child log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="childLog">The child log to log.</param>
        public virtual Boolean WriteChildLog(
            Log log,
            Log childLog)
        {
            return false;
        }

        // Write to output data sources

        /// <summary>
        /// Writes the specified text to the output kind of this instance.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <returns>Returns true whether the text has been written.</returns>
        public Boolean Write(String text)
        {
            switch(this.OutputKind)
            {
                case DataSourceKind.Console:
                    return this.WriteToConsole(text);
                case DataSourceKind.Repository:
                    return this.WriteToFile(text);
            }

            return false;
        }

        /// <summary>
        /// Writes the specified text to the console of this instance.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <returns>Returns true whether the text has been written.</returns>
        private Boolean WriteToConsole(String text)
        {
            Boolean isLogged = false;

            if (!String.IsNullOrEmpty(text))
            {
                Console.Write(text);
                isLogged = true;
            }

            return isLogged;
        }

        /// <summary>
        /// Writes the specified text to the file of this instance.
        /// </summary>
        /// <param name="text">The text to write.</param>
        /// <returns>Returns true whether the text has been written.</returns>
        private Boolean WriteToFile(String text)
        {
            Boolean isLogged = false;

            if ((!String.IsNullOrEmpty(this.Filepath)) && (!String.IsNullOrEmpty(text)))
            {
                try
                {
                    if (!Directory.Exists(this.FolderPath))
                        Directory.CreateDirectory(this.FolderPath);

                    using (StreamWriter streamWriter = new global::System.IO.StreamWriter(this.Filepath, true))
                        streamWriter.Write(text);

                    isLogged = true;
                }
                catch
                {
                }
            }

            return isLogged;
        }

        #endregion

        // ------------------------------------------------------
        // ACCESSORS
        // ------------------------------------------------------

        #region Accessors

        /// <summary>
        /// Indicates whether this instance requires all the event history to be maintained.
        /// </summary>
        /// <returns>Returns True if this instance requires all the event history.</returns>
        public virtual Boolean IsHistoryRequired()
        {
            return false;
        }
        
        #endregion

        // ------------------------------------------------------
        // MANAGEMENT
        // ------------------------------------------------------

        #region Management

        /// <summary>
        /// Sets the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        public void SetLog(Log log)
        {
            this._Log = log;
        }

        /// <summary>
        /// Delete the logs older than the specified day number.
        /// </summary>
        /// <param name="expirationDayNumber">The number of expiration days to consider.</param>
        /// <remarks>With expiration day number equaling to -1, no files expires. Equaling to 0, all files except the current one expires.</remarks>
        public void DeleteExpiredLogs(int expirationDayNumber)
        {
            if (expirationDayNumber>-1 && Directory.Exists(this._FolderPath))
            {
                String[] files = Directory.GetFiles(this._FolderPath);

                String logFilePath = this.Filepath;

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.LastAccessTime < DateTime.Now.AddDays(-expirationDayNumber))
                    {
                        if (!string.Equals(fileInfo.FullName, logFilePath, StringComparison.OrdinalIgnoreCase))
                        {
                            try
                            {
                                fileInfo.Delete();
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sets the name of the file of this instance.
        /// </summary>
        /// <param name="fileName">The name of the file to consider.</param>
        public void SetFileName(String fileName)
        {
            this._FileName = (String.IsNullOrEmpty(fileName) ? "log_{{currentTimeStamp}}.log" : fileName)
                .Replace("{{currentTimeStamp}}", Guid.NewGuid().ToString(), false);
        }

        /// <summary>
        /// Sets the log file location.
        /// </summary>
        /// <param name="newFolderPath">The new folder path to consider.</param>
        /// <param name="isFileToBeMoved">Indicates whether the file must be moved.</param>
        /// <param name="newFileName">The new file name to consider.</param>
        public void SetFilePath(String newFolderPath, Boolean isFileToBeMoved, String newFileName = null)
        {
            if (String.IsNullOrEmpty(newFolderPath)) return;

            String oldFilePath = this.Filepath;
            newFileName = (String.IsNullOrEmpty(newFileName) ? (oldFilePath==null ? null : Path.GetFileName(oldFilePath)) : newFileName);

            if (isFileToBeMoved && File.Exists(oldFilePath) && !String.IsNullOrEmpty(oldFilePath))
            {
                // we move the old file to the new folder
                String newFilePath = newFolderPath.ToLower().GetEndedString(@"\").ToPath() + newFileName;

                try
                {
                    if (!Directory.Exists(newFolderPath))
                        Directory.CreateDirectory(newFolderPath);
                    File.Move(oldFilePath, newFilePath);
                }
                catch
                {
                }
            }

            this._FolderPath = newFolderPath;
            this._FileName = newFileName;
        }

        #endregion

        // ------------------------------------------
        // SERIALIZATION / UNSERIALIZATION
        // ------------------------------------------

        #region

        // Unserialization ---------------------------------

        /// <summary>
        /// Instantiates a new instance of Log class from a xml file.
        /// </summary>
        /// <param name="filePath">The path of the Xml file to load.</param>
        /// <param name="loadLog">The output log of the load task.</param>
          /// <param name="appScope">The application scope to consider.</param>
        /// <param name="mustFileExist">Indicates whether the file must exist.</param>
        /// <returns>The load log.</returns>
        public virtual Log LoadLog(
            String filePath,
            Log loadLog = null,
            IAppScope appScope = null,
            Boolean mustFileExist = true)
        {
            return new Log();
        }

        /// <summary>
        /// Instantiates a new instance of Log class from a xml string.
        /// </summary>
        /// <param name="xmlString">The Xml string to load.</param>
        /// <param name="loadLog">The output log of the load task.</param>
          /// <param name="appScope">The application scope to consider.</param>
        /// <returns>The log defined in the Xml file.</returns>
        public virtual Log LoadLogFromString(
            String xmlString,
            Log loadLog = null,
            AppScope appScope = null)
        {
            return new Log();
        }

        // Serialization ---------------------------------

        /// <summary>
        /// Saves this instance in the specified log file.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="logFilePath">The path of the log file to save.</param>
        /// <param name="isAppended">Indicates whether the new content is appended if one alreay exists.</param>
        /// <returns>Returns the saving log.</returns>
        public virtual Boolean Save(Log log, String logFilePath, Boolean isAppended = false)
        {
            return false;
        }

        /// <summary>
        /// Saves this instance in the specified log file.
        /// </summary>
        /// <param name="isAppended">Indicates whether the new content is appended if one alreay exists.</param>
        /// <returns>Returns the saving log.</returns>
        public Boolean Save(Boolean isAppended = false)
        {
            return this.Save(this._Log, this.Filepath, isAppended);
        }

        /// <summary>
        /// Gets the string representing to the specified log.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="attributeNames">The attribute names to consider.</param>
        /// <returns>The string representing to the specified log.</returns>
        public virtual String ToString(
            Log log,
            List<String> attributeNames = null)
        {
            return "";
        }

        /// <summary>
        /// Gets the string representing to the specified event.
        /// </summary>
        /// <param name="logEvent">The log event to consider.</param>
        /// <param name="attributeNames">The attribute names to consider.</param>
        /// <returns>The string representing to the specified event.</returns>
        public virtual String ToString(
            LogEvent logEvent,
            List<String> attributeNames = null)
        {
            return "";
        }

        #endregion
    }
    }
