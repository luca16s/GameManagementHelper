namespace GameSaveManager.DesktopApp.Pages
{
    using System.Windows.Controls;

    using GameSaveManager.Core.Enums;
    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;
    using GameSaveManager.DesktopApp.ViewModel;

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
}