using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.Core.Services;
using GameSaveManager.View.Pages;
using GameSaveManager.View.ViewModel;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;

namespace GameSaveManager.Windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IConfigurationRoot Configuration { get; set; }
        public IServiceProvider ServiceProvider { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var connectionType = Environment.GetEnvironmentVariable("DROPBOX_CONNECTION_TYPE");
            var isFastConnectionEnable = string.IsNullOrEmpty(connectionType) ||
                                connectionType.ToLower(culture: CultureInfo.CurrentCulture) == "fast";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            builder.AddUserSecrets<App>();

            Configuration = builder.Build();

            Current.Properties["SECRETS"] = new Secrets
            {
                AppKey = Configuration.GetSection(nameof(Secrets.AppKey)).Value,
                AppSecret = Configuration.GetSection(nameof(Secrets.AppSecret)).Value,
                AppToken = (Debugger.IsAttached && isFastConnectionEnable)
                           ? Configuration.GetSection(nameof(Secrets.AppToken)).Value
                           : string.Empty,
            };

            var servicesCollection = new ServiceCollection();
            ConfigureServices(servicesCollection);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ServiceProvider = servicesCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void ConfigureServices(ServiceCollection servicesCollection)
        {
            servicesCollection.AddTransient(typeof(GamesPage));
            servicesCollection.AddTransient(typeof(MainWindow));
            servicesCollection.AddTransient(typeof(AccountPage));
            servicesCollection.AddTransient(typeof(SettingsPage));
            servicesCollection.AddTransient(typeof(GamesPageViewModel));
            servicesCollection.AddTransient(typeof(AccountPageViewModel));
            servicesCollection.AddTransient(typeof(SettingsPageViewModel));
            servicesCollection.AddTransient<IFactory<IBackupStrategy>, BackupFactory>();
        }
    }
}