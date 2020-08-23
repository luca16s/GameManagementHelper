using GameSaveManager.View.ViewModel;

using System.Windows.Controls;

namespace GameSaveManager.View.Pages
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        public AccountPage()
        {
            InitializeComponent();
            DataContext = new AccountPageViewModel();
        }
    }
}