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
        IOptions<Secrets> options,
        IConnection<DropboxClient> connection
    )
    {
        InitializeComponent();

        DataContext = new AccountViewModel(
            options,
            connection
        );
    }
}