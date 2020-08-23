using GameSaveManager.Core.Interfaces;
using GameSaveManager.View.Pages;

using System.Windows;

namespace GameSaveManager.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IBackupStrategy BackupStrategy;

        public MainWindow(IBackupStrategy backupStrategy)
        {
            InitializeComponent();
            BackupStrategy = backupStrategy;
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AccountPage());
        }

        private void OpenGamesListPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new GamesPage(BackupStrategy));
        }

        private void OpenSettingsPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SettingsPage());
        }
    }
}