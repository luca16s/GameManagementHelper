namespace iso.gmh.desktop
{
    using System.Windows;

    using iso.gmh.desktop.Pages;

    public partial class MainWindow : Window
    {
        private readonly GamesPage GamesPage;
        private readonly SteamPage SteamPage;
        private readonly AboutPage AboutPage;
        private readonly AccountPage AccountPage;
        private readonly SettingsPage SettingsPage;

        public MainWindow(
            AccountPage accountPage,
            GamesPage gamesPage,
            SteamPage steamPage,
            SettingsPage settingsPage,
            AboutPage aboutPage
        )
        {
            InitializeComponent();
            DataContext = this;

            AccountPage = accountPage;
            GamesPage = gamesPage;
            SettingsPage = settingsPage;
            AboutPage = aboutPage;
            SteamPage = steamPage;
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(AccountPage);

        private void OpenGamesListPage(object sender, RoutedEventArgs e) => MainFrame.Navigate(GamesPage);

        private void OpenSteamPage(object sender, RoutedEventArgs e) => MainFrame.Navigate(SteamPage);

        private void OpenSettingsPage(object sender, RoutedEventArgs e) => MainFrame.Navigate(SettingsPage);

        private void OpenAboutPage(object sender, RoutedEventArgs e) => MainFrame.Navigate(AboutPage);
    }
}