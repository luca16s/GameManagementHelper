using GameSaveManager.Windows.Windows;
using GameSaveManager.WPF.Helper;
using GameSaveManager.WPF.Windows;

using System.Configuration;
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
            if (!Helper.IsWindowOpen<AccountWindow>())
                new AccountWindow().Show();
        }

        private void OpenGamesListPage(object sender, RoutedEventArgs e)
        {
            if (!Helper.IsWindowOpen<GamesWindow>())
                new GamesWindow().Show();
        }

        private void OpenSettingsPage(object sender, RoutedEventArgs e)
        {
            if (!Helper.IsWindowOpen<ConfigurationWindow>())
                new ConfigurationWindow().Show();
        }
    }
}
