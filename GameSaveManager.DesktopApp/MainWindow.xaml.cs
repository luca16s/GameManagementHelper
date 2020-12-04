using GameSaveManager.DesktopApp.Pages;

using System.Windows;

namespace GameSaveManager.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AccountPage AccountPage;
        private readonly GamesPage GamesPage;
        private readonly SettingsPage SettingsPage;

        public MainWindow(AccountPage accountPage, GamesPage gamesPage, SettingsPage settingsPage)
        {
            InitializeComponent();

            AccountPage = accountPage;
            GamesPage = gamesPage;
            SettingsPage = settingsPage;
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(AccountPage);
        }

        private void OpenGamesListPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(GamesPage);
        }

        private void OpenSettingsPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(SettingsPage);
        }

        private void OpenAboutPage(object sender, RoutedEventArgs e)
        {
        }
    }
}