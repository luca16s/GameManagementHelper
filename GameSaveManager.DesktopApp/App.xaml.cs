using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.Core.Services;
using GameSaveManager.DesktopApp.Pages;
using GameSaveManager.DesktopApp.ViewModel;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
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
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("gamelist.json", false, true);

            builder.AddUserSecrets<App>();

            Configuration = builder.Build();

            var servicesCollection = new ServiceCollection();

            ConfigureServices(servicesCollection, isFastConnectionEnable);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ServiceProvider = servicesCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(ServiceCollection servicesCollection, bool isFastConnectionEnable)
        {
            servicesCollection.AddTransient(typeof(GamesPage));
            servicesCollection.AddTransient(typeof(MainWindow));
            servicesCollection.AddTransient(typeof(AccountPage));
            servicesCollection.AddTransient(typeof(SettingsPage));
            servicesCollection.AddTransient(typeof(GamesPageViewModel));
            servicesCollection.AddTransient(typeof(AccountPageViewModel));
            servicesCollection.AddTransient(typeof(SettingsPageViewModel));
            servicesCollection.AddTransient<IFactory<IBackupStrategy>, BackupFactory>();

            servicesCollection.Configure<Secrets>(secret =>
            {
                secret.AppKey = Configuration.GetSection(key: nameof(Secrets.AppKey)).Value;
                secret.AppSecret = Configuration.GetSection(key: nameof(Secrets.AppSecret)).Value;
                secret.AppToken = (Debugger.IsAttached && isFastConnectionEnable)
                           ? Configuration.GetSection(key: nameof(Secrets.AppToken)).Value
                           : string.Empty;
            });

            servicesCollection.Configure<List<GameInformationModel>>(gameInformation => Configuration
                .GetSection(key: nameof(GameInformationModel)).Bind(gameInformation));
        }
    }
}