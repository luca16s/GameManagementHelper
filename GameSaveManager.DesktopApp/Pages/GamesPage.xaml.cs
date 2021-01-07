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

        private void Upload_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GamesPageViewModel.UploadCommand.CanExecute(null))
                GamesPageViewModel.UploadCommand.Execute(null);
        }

        private void Download_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (GamesPageViewModel.DownloadCommand.CanExecute(null))
                GamesPageViewModel.DownloadCommand.Execute(null);
        }
    }
}