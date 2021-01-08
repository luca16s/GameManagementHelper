namespace GameSaveManager.DesktopApp.Pages
{
    using System.Collections.ObjectModel;
    using System.Windows.Controls;

    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;
    using GameSaveManager.DesktopApp.ViewModel;

    using Microsoft.Extensions.Options;

    /// <summary>
    /// Interaction logic for GamesPage.xaml
    /// </summary>
    public partial class GamesPage : Page
    {
        private readonly GamesPageViewModel GamesPageViewModel;
        public GamesPage(IFactory<IBackupStrategy> backupStrategy, IOptions<ObservableCollection<GameInformationModel>> options)
        {
            InitializeComponent();
            GamesPageViewModel = new GamesPageViewModel(backupStrategy, options);
            DataContext = GamesPageViewModel;
        }
    }
}