namespace GameSaveManager.DesktopApp.ViewModel
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
        private readonly IFactory<EDriveServices, IConnection> Connection;
        private readonly Secrets Secrets;

        private ICommand _ConnectCommand;

        public ICommand ConnectCommand => _ConnectCommand ??= new RelayCommand<object>(async _ =>
                                                        {
                                                            await ConnectAsync().ConfigureAwait(true);
                                                            await SetUserInformation().ConfigureAwait(true);
                                                        });

        public AccountPageViewModel(IFactory<EDriveServices, IConnection> connection, IOptions<Secrets> options)
        {
            if (options == null)
                return;

            Connection = connection;
            Secrets = options.Value;
        }

        private async Task ConnectAsync()
            => App.Client = await Connection
            .Create(App.DriveService)
            .ConnectAsync(Secrets)
            .ConfigureAwait(true);

        private static async Task SetUserInformation()
        {
            Dropbox.Api.Users.FullAccount user = await App.Client
                .Users
                .GetCurrentAccountAsync()
                .ConfigureAwait(true);

            Settings.Default.Name = user.Name.DisplayName;
            Settings.Default.Email = user.Email;
        }
    }
}