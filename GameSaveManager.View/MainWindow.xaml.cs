using GameSaveManager.View.Pages;

using System.Windows;

namespace GameSaveManager.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AccountPage());
        }

        private void OpenGamesListPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new GamesPage());
        }

        private void OpenSettingsPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SettingsPage());
        }
    }
}
