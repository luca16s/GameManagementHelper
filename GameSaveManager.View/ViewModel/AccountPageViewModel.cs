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
        private readonly DropboxConnection DropboxConnection;

        private ICommand _ConnectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                if (_ConnectCommand == null) _ConnectCommand = new RelayCommand<object>(async p =>
                {
                    await ConnectAsync().ConfigureAwait(true);
                    await SetUserInformation().ConfigureAwait(true);
                });
                return _ConnectCommand;
            }
        }

        public AccountPageViewModel()
        {
            Application.Current.Properties["DropboxConnection"] = new DropboxConnection();
            DropboxConnection = (DropboxConnection)Application.Current.Properties["DropboxConnection"];
        }

        private async Task ConnectAsync()
        {
            await DropboxConnection.ConnectAsync(appKey:"", appSecret:"").ConfigureAwait(true);
        }

        private async Task SetUserInformation()
        {
            var user = await DropboxConnection.Client.Users.GetCurrentAccountAsync().ConfigureAwait(true);

            Settings.Default.Name = user.Name.DisplayName;
            Settings.Default.Email = user.Email;
        }
    }
}
