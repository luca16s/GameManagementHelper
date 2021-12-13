namespace iso.gmh.desktop
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;
    using System.Windows;

    using iso.gmh.desktop.Pages;
    using iso.gmh.Core.Enums;
    using iso.gmh.Core.Interfaces;
    using iso.gmh.Core.Models;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using iso.gmh.desktop.ViewModel;
    using iso.gmh.services.Drive;
    using iso.gmh.services.Backup;

    public partial class App : Application
    {
        public static IConnection Client { get; set; }
        public static EDriveServices DriveService { get; set; }
        public static EBackupSaveType BackupType { get; set; }

        public IConfigurationRoot Configuration { get; set; }
        public IServiceProvider ServiceProvider { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            string connectionType = Environment.GetEnvironmentVariable("CONNECTION_TYPE");
            bool isFastConnectionEnable = string.IsNullOrEmpty(connectionType) ||
                                connectionType.ToLower(culture: CultureInfo.CurrentCulture) == "fast";

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("gamelist.json", false, true);

            _ = builder.AddUserSecrets<App>();

            Configuration = builder.Build();

            var servicesCollection = new ServiceCollection();

            ConfigureServices(servicesCollection, isFastConnectionEnable);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ServiceProvider = servicesCollection.BuildServiceProvider();

            MainWindow mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(ServiceCollection servicesCollection, bool isFastConnectionEnable)
        {
            _ = servicesCollection.AddTransient(typeof(AboutPage));
            _ = servicesCollection.AddTransient(typeof(GamesPage));
            _ = servicesCollection.AddTransient(typeof(MainWindow));
            _ = servicesCollection.AddTransient(typeof(AccountPage));
            _ = servicesCollection.AddTransient(typeof(SettingsPage));
            _ = servicesCollection.AddTransient(typeof(AboutViewModel));
            _ = servicesCollection.AddTransient(typeof(GamesPageViewModel));
            _ = servicesCollection.AddTransient(typeof(AccountPageViewModel));
            _ = servicesCollection.AddTransient(typeof(SettingsPageViewModel));
            _ = servicesCollection.AddTransient<IFactory<EDriveServices, IConnection>, ConnectionFactory>();
            _ = servicesCollection.AddTransient<IFactory<EBackupSaveType, IBackupStrategy>, BackupFactory>();

            _ = servicesCollection.Configure<Secrets>(secret =>
            {
                secret.AppKey = Configuration.GetSection(key: nameof(Secrets.AppKey)).Value;
                secret.AppSecret = Configuration.GetSection(key: nameof(Secrets.AppSecret)).Value;
                secret.AppToken = Debugger.IsAttached && isFastConnectionEnable
                           ? Configuration.GetSection(key: nameof(Secrets.AppToken)).Value
                           : string.Empty;
            });

            _ = servicesCollection
                .Configure<ObservableCollection<GameInformationModel>>(gameInformation => Configuration
                .GetSection(key: nameof(GameInformationModel))
                .Bind(gameInformation));
        }
    }
}