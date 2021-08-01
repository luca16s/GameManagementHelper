namespace GameSaveManager.DesktopApp.Pages
{
    using System.Collections.ObjectModel;
    using System.Windows.Controls;

    using GameSaveManager.Core.Enums;
    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;
    using GameSaveManager.DesktopApp.ViewModel;

    using Microsoft.Extensions.Options;

    public partial class GamesPage : Page
    {
        public GamesPage(IFactory<EBackupSaveType, IBackupStrategy> backupStrategy,
            IOptions<ObservableCollection<GameInformationModel>> options)
        {
            InitializeComponent();
            DataContext = new GamesPageViewModel(backupStrategy, options);
        }
    }
}