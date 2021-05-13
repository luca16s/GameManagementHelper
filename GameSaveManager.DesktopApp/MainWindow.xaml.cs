namespace GameSaveManager.Windows
{
    using System.Windows;

    using GameSaveManager.DesktopApp.Pages;

    public partial class MainWindow : Window
    {
        private readonly AboutPage AboutPage;
        private readonly AccountPage AccountPage;
        private readonly GamesPage GamesPage;
        private readonly SettingsPage SettingsPage;

        public MainWindow(AccountPage accountPage, GamesPage gamesPage, SettingsPage settingsPage, AboutPage aboutPage)
        {
            InitializeComponent();
            DataContext = this;

            AccountPage = accountPage;
            GamesPage = gamesPage;
            SettingsPage = settingsPage;
            AboutPage = aboutPage;
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(AccountPage);

        private void OpenAboutPage(object sender, RoutedEventArgs e) => MainFrame.Navigate(AboutPage);

        private void OpenGamesListPage(object sender, RoutedEventArgs e) => MainFrame.Navigate(GamesPage);

        private void OpenSettingsPage(object sender, RoutedEventArgs e) => MainFrame.Navigate(SettingsPage);
    }
}