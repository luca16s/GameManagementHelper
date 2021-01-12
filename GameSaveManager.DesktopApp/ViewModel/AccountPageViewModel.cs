namespace GameSaveManager.DesktopApp.ViewModel
{
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    using Dropbox.Api;

    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;
    using GameSaveManager.DesktopApp.Commands;
    using GameSaveManager.DesktopApp.Properties;
    using GameSaveManager.DropboxApi;

    using Microsoft.Extensions.Options;

    public class AccountPageViewModel : BaseViewModel
    {
        private readonly IConnection DropboxConnection;
        private readonly Secrets Secrets;

        private ICommand _ConnectCommand;

        public ICommand ConnectCommand => _ConnectCommand ??= new RelayCommand<object>(async _ =>
                                                        {
                                                            await ConnectAsync().ConfigureAwait(true);
                                                            await SetUserInformation().ConfigureAwait(true);
                                                        });

        private DropboxClient DropboxClient { get; set; }

        public AccountPageViewModel(IOptions<Secrets> options)
        {
            if (options == null)
                return;

            DropboxConnection = new DropboxConnection();
            Secrets = options.Value;
        }

        private async Task ConnectAsync()
        {
            Application
                .Current
                .Properties["CLIENT"] = DropboxClient = (DropboxClient)await DropboxConnection
                .ConnectAsync(Secrets)
                .ConfigureAwait(true);
        }

        private async Task SetUserInformation()
        {
            Dropbox.Api.Users.FullAccount user = await DropboxClient
                .Users
                .GetCurrentAccountAsync()
                .ConfigureAwait(true);

            Settings.Default.Name = user.Name.DisplayName;
            Settings.Default.Email = user.Email;
        }
    }
}