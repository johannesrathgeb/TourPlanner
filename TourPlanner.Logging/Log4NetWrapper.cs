using System;
using System.IO;

namespace TourPlanner.Logging
{
    public class Log4NetWrapper : ILoggerWrapper
    {
        private log4net.ILog logger;

        public static Log4NetWrapper CreateLogger(string configPath)
        {
            if (!File.Exists(configPath))
            {
                throw new ArgumentException("Does not exist.", nameof(configPath));
            }

            log4net.Config.XmlConfigurator.Configure(new FileInfo(configPath));
            var logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            return new Log4NetWrapper(logger);
        }

        private Log4NetWrapper(log4net.ILog logger)
        {
            this.logger = logger;
        }

        public void Debug(string message)
        {
            this.logger.Debug(message);
        }
        public void Warn(string message)
        {
            this.logger.Warn(message);
        }

        public void Error(string message)
        {
            this.logger.Error(message);
        }

        public void Fatal(string message)
        {
            this.logger.Fatal(message);
        }
    }
}
