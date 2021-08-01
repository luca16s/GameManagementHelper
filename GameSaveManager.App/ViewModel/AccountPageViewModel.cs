﻿namespace GameSaveManager.DesktopApp.ViewModel
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using GameSaveManager.Core.Enums;
    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;
    using GameSaveManager.DesktopApp.Commands;
    using GameSaveManager.DesktopApp.Properties;
    using GameSaveManager.Windows;

    using Microsoft.Extensions.Options;

    public class AccountPageViewModel : BaseViewModel
    {
        private readonly Secrets Secrets;
        private readonly IFactory<EDriveServices, IConnection> Connection;

        private ICommand _ConnectCommand;

        public ICommand ConnectCommand => _ConnectCommand ??= new RelayCommand<object>(async _ =>
        {
            await ConnectAsync().ConfigureAwait(true);
            await SetUserInformation().ConfigureAwait(true);
        });

        public AccountPageViewModel(IFactory<EDriveServices, IConnection> connection,
            IOptions<Secrets> options)
        {
            if (options == null)
                return;

            Connection = connection;
            Secrets = options.Value;
        }

        private async Task ConnectAsync()
        {
            App.Client = Connection.Create(App.DriveService);

            if (App.Client != null)
                await App.Client
                    .ConnectAsync(Secrets)
                    .ConfigureAwait(true);
        }

        private static async Task SetUserInformation()
        {
            UserModel userInformation = await App.Client.GetUserInformation();

            Settings.Default.Name = userInformation.UserName;
            Settings.Default.Email = userInformation.Email;
        }
    }
}