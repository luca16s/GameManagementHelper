namespace iso.gmh.desktop.Pages;

using System.Windows.Controls;

using iso.gmh.desktop.ViewModel;
using iso.gmh.Core.Enums;
using iso.gmh.Core.Interfaces;
using iso.gmh.Core.Models;

using Microsoft.Extensions.Options;

public partial class AccountPage : Page
{
    public AccountPage(IFactory<EDriveServices, IConnection> connection,
        IOptions<Secrets> options)
    {
        InitializeComponent();
        DataContext = new AccountPageViewModel(connection, options);
    }
}