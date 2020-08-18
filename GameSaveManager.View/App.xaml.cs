using GameSaveManager.Core.Models;

using Microsoft.Extensions.Configuration;

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

            if (isDevelopment) builder.AddUserSecrets<App>();

            Configuration = builder.Build();

            var connectionType = Environment.GetEnvironmentVariable("DROPBOX_CONNECTION_TYPE");
            var isFastConnectionEnable = string.IsNullOrEmpty(connectionType) ||
                                connectionType.ToLower(culture: CultureInfo.CurrentCulture) == "fast";

            Current.Properties["SECRETS"] = new Secrets
            {
                AppKey = Configuration.GetSection(nameof(Secrets.AppKey)).Value,
                AppSecret = Configuration.GetSection(nameof(Secrets.AppSecret)).Value,
                AppToken = isFastConnectionEnable ? Configuration.GetSection(nameof(Secrets.AppToken)).Value : string.Empty,
            };
        }
    }
}