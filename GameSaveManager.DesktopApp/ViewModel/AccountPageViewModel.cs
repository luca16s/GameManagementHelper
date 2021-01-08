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

    public class AccountPageViewModel : ViewModelBase
    {
        private readonly IConnection DropboxConnection;
        private readonly Secrets Secrets;

        private ICommand _ConnectCommand;

        public ICommand ConnectCommand => _ConnectCommand ??= new RelayCommand<object>(async _ =>
                                                        {
                                                            await ConnectAsync().ConfigureAwait(true);
                                                            await SetUserInformation().ConfigureAwait(true);
                                                        });

        public static string Connect => "M19,3H5C3.89,3 3,3.89 3,5V9H5V5H19V19H5V15H3V19A2,2 0 0,0 5,21H19A2,2 0 0,0 21,19V5C21,3.89 20.1,3 19,3M10.08,15.58L11.5,17L16.5,12L11.5,7L10.08,8.41L12.67,11H3V13H12.67L10.08,15.58Z";

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