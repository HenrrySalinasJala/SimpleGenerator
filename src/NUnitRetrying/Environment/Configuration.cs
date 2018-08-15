namespace NUnitRetrying.Environment
{
    using log4net;
    using NUnit.Framework;
    using System;
    using System.Configuration;

    /// <summary>
    /// Environment Configuration
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// The logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The retry times number.
        /// </summary>
        public static int RetryTimes = GetInt("retryTimes", 1);

        /// <summary>
        /// Gets an int value from the configuration or the NUnit parameter.
        /// </summary>
        /// <param name="configurationPropertyName">The configuration name.</param>
        /// <param name="defaultValue">The default value if the configuration is not found.</param>
        /// <returns>returns .</returns>
        private static int GetInt(string configurationPropertyName, int defaultValue)
        {
            if (!string.IsNullOrEmpty(TestContext.Parameters.Get(configurationPropertyName)))
            {
                return int.Parse(TestContext.Parameters.Get(configurationPropertyName));
            }
            else if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[configurationPropertyName]))
            {
                try
                {
                    return int.Parse(ConfigurationManager.AppSettings[configurationPropertyName]);
                }
                catch (Exception)
                {
                    Console.Error.WriteLine(string.Format("Invalid value for property: [{0}]", configurationPropertyName));
                    return defaultValue;
                }
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
