using GameSaveManager.Core.Models;
using GameSaveManager.View.ViewModel;

using Microsoft.Extensions.Options;

using System.Windows.Controls;

namespace GameSaveManager.View.Pages
{
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