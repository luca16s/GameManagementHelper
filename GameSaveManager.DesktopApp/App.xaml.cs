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

            _ = builder.AddUserSecrets<App>();

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
            _ = servicesCollection.AddTransient(typeof(AboutPage));
            _ = servicesCollection.AddTransient(typeof(GamesPage));
            _ = servicesCollection.AddTransient(typeof(MainWindow));
            _ = servicesCollection.AddTransient(typeof(AccountPage));
            _ = servicesCollection.AddTransient(typeof(SettingsPage));
            _ = servicesCollection.AddTransient(typeof(GamesPageViewModel));
            _ = servicesCollection.AddTransient(typeof(AccountPageViewModel));
            _ = servicesCollection.AddTransient(typeof(SettingsPageViewModel));
            _ = servicesCollection.AddTransient<IFactory<IBackupStrategy>, BackupFactory>();

            _ = servicesCollection.Configure<Secrets>(secret =>
              {
                  secret.AppKey = Configuration.GetSection(key: nameof(Secrets.AppKey)).Value;
                  secret.AppSecret = Configuration.GetSection(key: nameof(Secrets.AppSecret)).Value;
                  secret.AppToken = (Debugger.IsAttached && isFastConnectionEnable)
                             ? Configuration.GetSection(key: nameof(Secrets.AppToken)).Value
                             : string.Empty;
              });

            _ = servicesCollection.Configure<List<GameInformationModel>>(gameInformation => Configuration
                  .GetSection(key: nameof(GameInformationModel)).Bind(gameInformation));
        }
    }
}