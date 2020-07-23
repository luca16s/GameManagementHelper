using GameSaveManager.DropboxIntegration;
using GameSaveManager.View.Helper;
using GameSaveManager.View.Pages;
using GameSaveManager.View.Windows;

using System;
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

        private async void AccountButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            var dropbox = new DropboxConnection(Environment.GetEnvironmentVariable("token"));
            //await dropbox.ConnectAsync();
            var get = await dropbox.Client.Users.GetCurrentAccountAsync().ConfigureAwait(true);
            var userInfo = DropboxConnection.GetAccountInfo(get.Name.DisplayName, get.Email);

            View.Properties.Settings.Default.SettingsKey = get.Email;
            View.Properties.Settings.Default.Name = get.Name.DisplayName;
            View.Properties.Settings.Default.Email = get.Email;
            View.Properties.Settings.Default.Save();

            MessageBox.Show(View.Properties.Settings.Default.Name);

            var a = await dropbox.CreateFolder("/Dark Souls 3").ConfigureAwait(true);
        }

        private void OpenGamesListPage(object sender, RoutedEventArgs e)
        {
            if (!HelperMethods.IsWindowOpen<GamesWindow>())
                new GamesWindow().Show();
        }

        private void OpenSettingsPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SettingsPage());
        }
    }
}
