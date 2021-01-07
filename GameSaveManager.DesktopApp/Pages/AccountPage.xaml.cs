namespace GameSaveManager.DesktopApp.Pages
{
    using System.Windows.Controls;

    using GameSaveManager.Core.Models;
    using GameSaveManager.DesktopApp.ViewModel;

    using Microsoft.Extensions.Options;

    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        private readonly AccountPageViewModel AccountPageViewModel;

        public AccountPage(IOptions<Secrets> options)
        {
            InitializeComponent();
            AccountPageViewModel = new AccountPageViewModel(options);
            DataContext = AccountPageViewModel;
        }

        private void Connect_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AccountPageViewModel.ConnectCommand.CanExecute(null))
                AccountPageViewModel.ConnectCommand.Execute(null);
        }
    }
}