namespace GameSaveManager.DesktopApp.Pages
{
    using System.Windows;
    using System.Windows.Controls;

    using GameSaveManager.DesktopApp.Properties;
    using GameSaveManager.DesktopApp.ViewModel;

    using MaterialDesignThemes.Wpf;

    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            DataContext = new SettingsPageViewModel();
        }

        private void ChangeTheme_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.DarkMode = !Settings.Default.DarkMode;

            var paletteHelper = new PaletteHelper();

            ITheme theme = paletteHelper.GetTheme();

            if (Settings.Default.DarkMode)
            {
                theme.SetBaseTheme(Theme.Dark);
            }
            else
            {
                theme.SetBaseTheme(Theme.Light);
            }

            paletteHelper.SetTheme(theme);
        }
    }
}