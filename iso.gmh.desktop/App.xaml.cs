namespace iso.gmh.desktop;

using System;
using System.Collections.ObjectModel;
using System.Windows;

using Dropbox.Api;

using iso.gmh.core.Services;
using iso.gmh.Core.Enums;
using iso.gmh.Core.Interfaces;
using iso.gmh.Core.Models;
using iso.gmh.desktop.Pages;
using iso.gmh.desktop.ViewModel;
using iso.gmh.dropbox;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public partial class App : Application
{
    private IServiceCollection ConfigureServices(
        ServiceCollection services,
        IConfigurationRoot configuration
    ) => services
            .AddTransient<AboutPage>()
            .AddTransient<GamesPage>()
            .AddTransient<SteamPage>()
            .AddTransient<MainWindow>()
            .AddTransient<AccountPage>()
            .AddTransient<SettingsPage>()
            .AddTransient<DropboxClient>()
            .AddTransient<AboutViewModel>()
            .AddTransient<GamesPageViewModel>()
            .AddTransient<AccountPageViewModel>()
            .AddTransient<SettingsPageViewModel>()
            .Configure<Secrets>(secret => {
                secret.AppToken = string.Empty;
                secret.AppKey = configuration.GetSection(key: nameof(Secrets.AppKey)).Value;
                secret.AppSecret = configuration.GetSection(key: nameof(Secrets.AppSecret)).Value;
            })
            .Configure<ObservableCollection<Game>>(
                gameInformation => configuration.GetSection(key: nameof(Game)).Bind(gameInformation)
            )
            .AddTransient<IConnection<DropboxClient>, DropboxConnection>()
            .AddTransient<IFactory<ESaveType, IBackupStrategy>, BackupFactory>()
        ;

    public static EDrive DriveService { get; set; }
    public static ESaveType BackupType { get; set; }
    public static IConnection<DropboxClient> Connection { get; set; }

    protected override void OnStartup(
        StartupEventArgs e
    )
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("gamelist.json", false, true)
            .AddUserSecrets<App>()
            .Build();

        ConfigureServices(
            new ServiceCollection(),
            configuration
        ).BuildServiceProvider()
        .GetRequiredService<MainWindow>()
        .Show();
    }
}