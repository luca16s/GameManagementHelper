namespace iso.gmh.desktop.ViewModel;

using System.Threading.Tasks;
using System.Windows.Input;

using Dropbox.Api;

using iso.gmh.Core.Interfaces;
using iso.gmh.Core.Models;
using iso.gmh.desktop.Commands;
using iso.gmh.desktop.Properties;

using Microsoft.Extensions.Options;

public partial class AccountViewModel(
    IOptions<Secrets> Options,
    IConnection<DropboxClient> Connection
) : BaseViewModel
{
    private ICommand _ConnectCommand;

    public ICommand ConnectCommand => _ConnectCommand ??= new RelayCommand<object>(async _ =>
    {
        await ConnectAsync();
        await SetUserInformation();
    });

    private async Task ConnectAsync() => await Connection.ConnectAsync(Options.Value);

    private async Task SetUserInformation()
    {
        User userInformation = await Connection.GetUserInformationAsync();

        Settings.Default.Name = userInformation.UserName;
        Settings.Default.Email = userInformation.Email;
    }
}