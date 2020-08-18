using Dropbox.Api;

using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.DropboxIntegration;
using GameSaveManager.View.Commands;
using GameSaveManager.View.Properties;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GameSaveManager.View.ViewModel
{
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

        public AccountPageViewModel()
        {
            DropboxConnection = new DropboxConnection();
            Secrets = (Secrets)Application.Current.Properties["SECRETS"];
        }

        private async Task ConnectAsync()
        {
            Application.Current.Properties["CLIENT"] = await DropboxConnection.ConnectAsync(Secrets).ConfigureAwait(true) as DropboxClient;
        }

        private static async Task SetUserInformation()
        {
            var user = await ((DropboxClient)Application.Current.Properties["CLIENT"])
                .Users
                .GetCurrentAccountAsync()
                .ConfigureAwait(true);

            Settings.Default.Name = user.Name.DisplayName;
            Settings.Default.Email = user.Email;
        }
    }
}