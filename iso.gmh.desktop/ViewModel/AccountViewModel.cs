namespace iso.gmh.desktop.ViewModel;

using System.Threading.Tasks;
using System.Windows.Input;

using Dropbox.Api;

using iso.gmh.Core.Interfaces;
using iso.gmh.Core.Models;
using iso.gmh.desktop.Commands;
using iso.gmh.desktop.Properties;

using Microsoft.Extensions.Options;

public partial class AccountViewModel : BaseViewModel
{
    private readonly Secrets Secrets;
    private readonly IConnection<DropboxClient> Connection;

    private ICommand _ConnectCommand;

    public ICommand ConnectCommand => _ConnectCommand ??= new RelayCommand<object>(async _ =>
    {
        await ConnectAsync();
        await SetUserInformation();
    });

    public AccountViewModel(
        IConnection<DropboxClient> connection,
        IOptions<Secrets> options
    )
    {
        if (options is null) return;

        Connection = connection;
        Secrets = options.Value;
    }

    private async Task ConnectAsync()
    {
        App.Connection = Connection;

        if (App.Connection is not null)
            App.Connection.Client = await App.Connection.ConnectAsync(Secrets);
    }

    private static async Task SetUserInformation()
    {
        User userInformation = await App.Connection.GetUserInformation();

        Settings.Default.Name = userInformation.UserName;
        Settings.Default.Email = userInformation.Email;
    }
}