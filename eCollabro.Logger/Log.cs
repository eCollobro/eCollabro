// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.IO;
using System;
using System.Configuration;

#endregion

namespace eCollabro.Logger
{
    /// <summary>
    /// Log : To write log 
    /// </summary>
    public class Log
    {
        #region Data Memebers

        private static string exceptionlogFileName;
        private static string debuglogFileName;
        private static Log log = new Log();
        private bool _isLogEnabled;

        #endregion

        #region Methods

        /// <summary>
        /// GetInstance
        /// </summary>
        /// <returns></returns>
        public static Log GetInstance()
        {
            log.SetLogFiles();
            return log;
        }


        /// <summary>
        /// SetLogFiles
        /// </summary>
        private void SetLogFiles()
        {
            LoggerConfigurationSection config = (LoggerConfigurationSection)ConfigurationManager.GetSection("loggerConfiguration");
            string logFilePath = config.LogFilePath;
            _isLogEnabled = config.DebugMode;
            if (Path.GetPathRoot(logFilePath).StartsWith("\\") || Path.GetPathRoot(logFilePath).StartsWith("/"))
            {
                logFilePath = AppDomain.CurrentDomain.BaseDirectory + "App_Data" + logFilePath;
                if (!Directory.Exists(logFilePath))
                    Directory.CreateDirectory(logFilePath);
            }
            exceptionlogFileName = Path.Combine(logFilePath, "eCollabro_Exceptionlog_" + DateTime.Now.ToString("MMddyyyyHH00") + ".txt");
            debuglogFileName = Path.Combine(logFilePath, "eCollabro_Debuglog_" + DateTime.Now.ToString("MMddyyyyHH00") + ".txt");
        }

        /// <summary>
        /// WriteExceptionLog
        /// </summary>
        /// <param name="line"></param>
        public void WriteExceptionLog(String line)
        {
            WriteLine(exceptionlogFileName, line);
        }

        /// <summary>
        /// WriteDebugLog
        /// </summary>
        /// <param name="line"></param>
        public void WriteDebugLog(string line)
        {
            WriteLine(debuglogFileName, line);
        }

        /// <summary>
        /// DebugEnabled
        /// </summary>
        public bool DebugEnabled
        {
            get
            {
                return _isLogEnabled;
            }
        }

        /// <summary>
        /// WriteLine
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="line"></param>
        private void WriteLine(string fileName, string line)
        {
            //IS : Lock Added
            lock (this)
            {
                try
                {
                    TextWriter tw = new StreamWriter(fileName, true);
                    tw.WriteLine(DateTime.Now + ": " + line);
                    tw.Close();
                }
                catch (Exception)
                {
                    throw new Exception("Error creating log file as " + fileName);
                }
            }
        }

        #endregion
    }
}
