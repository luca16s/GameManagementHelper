namespace GameSaveManager.Windows
{
    using System.Windows;

    using GameSaveManager.DesktopApp.Pages;

    public partial class MainWindow : Window
    {
        private readonly AccountPage AccountPage;
        private readonly GamesPage GamesPage;
        private readonly SettingsPage SettingsPage;
        private readonly AboutPage AboutPage;

        public string Games => "M7,6H17A6,6 0 0,1 23,12A6,6 0 0,1 17,18C15.22,18 13.63,17.23 12.53,16H11.47C10.37,17.23 8.78,18 7,18A6,6 0 0,1 1,12A6,6 0 0,1 7,6M6,9V11H4V13H6V15H8V13H10V11H8V9H6M15.5,12A1.5,1.5 0 0,0 14,13.5A1.5,1.5 0 0,0 15.5,15A1.5,1.5 0 0,0 17,13.5A1.5,1.5 0 0,0 15.5,12M18.5,9A1.5,1.5 0 0,0 17,10.5A1.5,1.5 0 0,0 18.5,12A1.5,1.5 0 0,0 20,10.5A1.5,1.5 0 0,0 18.5,9Z";
        public string About => "M13,9H11V7H13M13,17H11V11H13M12,2A10,10 0 0,0 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12A10,10 0 0,0 12,2Z";
        public string Account => "M6,17C6,15 10,13.9 12,13.9C14,13.9 18,15 18,17V18H6M15,9A3,3 0 0,1 12,12A3,3 0 0,1 9,9A3,3 0 0,1 12,6A3,3 0 0,1 15,9M3,5V19A2,2 0 0,0 5,21H19A2,2 0 0,0 21,19V5A2,2 0 0,0 19,3H5C3.89,3 3,3.9 3,5Z";
        public string Settings => "M17.25,12C17.25,12.23 17.23,12.46 17.2,12.68L18.68,13.84C18.81,13.95 18.85,14.13 18.76,14.29L17.36,16.71C17.27,16.86 17.09,16.92 16.93,16.86L15.19,16.16C14.83,16.44 14.43,16.67 14,16.85L13.75,18.7C13.72,18.87 13.57,19 13.4,19H10.6C10.43,19 10.28,18.87 10.25,18.7L10,16.85C9.56,16.67 9.17,16.44 8.81,16.16L7.07,16.86C6.91,16.92 6.73,16.86 6.64,16.71L5.24,14.29C5.15,14.13 5.19,13.95 5.32,13.84L6.8,12.68C6.77,12.46 6.75,12.23 6.75,12C6.75,11.77 6.77,11.54 6.8,11.32L5.32,10.16C5.19,10.05 5.15,9.86 5.24,9.71L6.64,7.29C6.73,7.13 6.91,7.07 7.07,7.13L8.81,7.84C9.17,7.56 9.56,7.32 10,7.15L10.25,5.29C10.28,5.13 10.43,5 10.6,5H13.4C13.57,5 13.72,5.13 13.75,5.29L14,7.15C14.43,7.32 14.83,7.56 15.19,7.84L16.93,7.13C17.09,7.07 17.27,7.13 17.36,7.29L18.76,9.71C18.85,9.86 18.81,10.05 18.68,10.16L17.2,11.32C17.23,11.54 17.25,11.77 17.25,12M19,3H5C3.89,3 3,3.89 3,5V19A2,2 0 0,0 5,21H19A2,2 0 0,0 21,19V5C21,3.89 20.1,3 19,3M12,10C10.89,10 10,10.89 10,12A2,2 0 0,0 12,14A2,2 0 0,0 14,12C14,10.89 13.1,10 12,10Z";

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

        private void OpenGamesListPage(object sender, RoutedEventArgs e) => MainFrame.Navigate(GamesPage);

        private void OpenSettingsPage(object sender, RoutedEventArgs e) => MainFrame.Navigate(SettingsPage);

        private void OpenAboutPage(object sender, RoutedEventArgs e) => MainFrame.Navigate(AboutPage);
    }
}