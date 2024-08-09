namespace iso.gmh.desktop.ViewModel;

using System.Threading.Tasks;
using System.Windows.Input;

using iso.gmh.Core.Enums;
using iso.gmh.Core.Interfaces;
using iso.gmh.Core.Models;
using iso.gmh.desktop.Commands;
using iso.gmh.desktop.Properties;

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