using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.DropboxIntegration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Configuration;
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
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            if (isDevelopment)
            {
                builder.AddUserSecrets<App>();
            }

            Configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();

            services
                .AddOptions()
                .AddLogging()
                .BuildServiceProvider();

            var serviceProvider = services.BuildServiceProvider();

            Application.Current.Properties["secrets"] = new Secrets
            {
                AppKey = Configuration.GetSection("AppKey").Value,
                AppSecret = Configuration.GetSection("AppSecret").Value,
            };
        }
    }
}
