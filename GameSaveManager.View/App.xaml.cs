using GameSaveManager.Core.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Globalization;
using System.Windows;

namespace GameSaveManager.Windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfigurationRoot Configuration { get; set; }

        public App()
        {
            var devEnvironmentVariable = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            var isDevelopment = string.IsNullOrEmpty(devEnvironmentVariable) ||
                                devEnvironmentVariable.ToLower(culture: CultureInfo.CurrentCulture) == "development";

            var builder = new ConfigurationBuilder();

            if (isDevelopment)
            {
                builder.AddUserSecrets<App>();
            }

            Configuration = builder.Build();

            Current.Properties["secrets"] = new Secrets
            {
                AppKey = Configuration.GetSection("AppKey").Value,
                AppSecret = Configuration.GetSection("AppSecret").Value,
            };
        }
    }
}
