using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Logging;

namespace TourPlanner.DataAccessLayer
{
    public class Configuration
    {
        private static IConfiguration? instance;

        public static IConfiguration GetInstance()
        {
            if (instance == null)
            {

                if(!File.Exists("../../../../config/appsettings.json"))
                {
                    LoggerFactory.GetLogger().Fatal("Error while reading configuration file information - Configuration file is missing!");
                    MessageBox.Show("Configuration file is missing!", "Missing config", MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(1);
                }
                instance = new ConfigurationBuilder()
                    .AddJsonFile(Path.GetFullPath("../../../../config/appsettings.json"), false, true)
                    .Build();
            }
            return instance;
        }
    }
}
