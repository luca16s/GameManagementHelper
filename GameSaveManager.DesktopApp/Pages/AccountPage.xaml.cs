namespace GameSaveManager.DesktopApp.Pages
{
    using GameSaveManager.Core.Models;
    using GameSaveManager.DesktopApp.ViewModel;

    using Microsoft.Extensions.Options;

    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        public AccountPage(IOptions<Secrets> options)
        {
            InitializeComponent();
            DataContext = new AccountPageViewModel(options);
        }
    }
}