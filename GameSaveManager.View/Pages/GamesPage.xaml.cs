using GameSaveManager.Core.Interfaces;
using GameSaveManager.View.ViewModel;

using System.Windows.Controls;

namespace GameSaveManager.View.Pages
{
    /// <summary>
    /// Interaction logic for GamesPage.xaml
    /// </summary>
    public partial class GamesPage : Page
    {
        public GamesPage(IFactory<IBackupStrategy> backupStrategy)
        {
            InitializeComponent();
            DataContext = new GamesPageViewModel(backupStrategy);
        }
    }
}