// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.Configuration;

#endregion 

namespace eCollabro.Logger
{
    public class LoggerConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// logFilePath
        /// </summary>
        [ConfigurationProperty("logFilePath", DefaultValue = @"\Logs", IsRequired = false)]
        public string LogFilePath
        {
            get
            { 
                return (string)this["logFilePath"]; 
            }
            set
            { 
                this["logFilePath"] = value; 
            }
        }

        [ConfigurationProperty("debugMode", DefaultValue = false , IsRequired = false)]
        public bool DebugMode
        {
            get
            {
                return (bool)this["debugMode"];
            }
            set
            {
                this["debugMode"] = value;
            }
        }

    }

}

