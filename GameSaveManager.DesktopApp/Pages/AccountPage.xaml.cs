namespace GameSaveManager.DesktopApp.Pages
{
    using System.Windows.Controls;

    using GameSaveManager.Core.Models;
    using GameSaveManager.DesktopApp.ViewModel;

    using Microsoft.Extensions.Options;

    public partial class AccountPage : Page
    {
        private readonly AccountPageViewModel AccountPageViewModel;

        public AccountPage(IOptions<Secrets> options)
        {
            InitializeComponent();
            AccountPageViewModel = new AccountPageViewModel(options);
            DataContext = AccountPageViewModel;
        }
    }
}