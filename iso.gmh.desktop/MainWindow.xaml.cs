namespace iso.gmh.desktop;

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
        GamesPage gamesPage,
        SteamPage steamPage,
        AboutPage aboutPage,
        AccountPage accountPage,
        SettingsPage settingsPage
    )
    {
        InitializeComponent();
        DataContext = this;

        GamesPage = gamesPage;
        SteamPage = steamPage;
        AboutPage = aboutPage;
        AccountPage = accountPage;
        SettingsPage = settingsPage;
    }

    private void GamesButton_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(GamesPage);

    private void SteamButton_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(SteamPage);

    private void AboutButton_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(AboutPage);

    private void AccountButton_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(AccountPage);

    private void SettingsButton_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(SettingsPage);
}