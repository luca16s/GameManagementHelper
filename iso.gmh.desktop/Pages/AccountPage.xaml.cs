namespace iso.gmh.desktop.Pages;

using System.Windows.Controls;

using Dropbox.Api;

using iso.gmh.Core.Interfaces;
using iso.gmh.Core.Models;
using iso.gmh.desktop.ViewModel;

using Microsoft.Extensions.Options;

public partial class AccountPage : Page
{
    public AccountPage(
        IConnection<DropboxClient> connection,
        IOptions<Secrets> options
    )
    {
        InitializeComponent();
        DataContext = new AccountPageViewModel(connection, options);
    }
}