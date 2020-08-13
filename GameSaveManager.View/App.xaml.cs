using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.DropboxIntegration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Windows;

namespace GameSaveManager.Windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _Host;

        public App()
        {
            _Host = Host
                .CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(context.Configuration, services);
                })
                .ConfigureLogging(logging =>
                {
                })
                .Build();
        }

        private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));

            services
                .AddScoped<ICloudOperations>(x =>
                ActivatorUtilities.CreateInstance<DropboxOperations>(x,
                new DropboxConnection().Client));
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _Host.StartAsync().ConfigureAwait(true);

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_Host)
            {
                await _Host.StopAsync(TimeSpan.FromSeconds(5)).ConfigureAwait(true);
            }

            base.OnExit(e);
        }
    }
}
