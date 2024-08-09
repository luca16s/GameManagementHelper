namespace iso.gmh.desktop.Pages;

using System.Windows;
using System.Windows.Controls;

using iso.gmh.desktop.ViewModel;

public partial class SettingsPage : Page
{
    public SettingsPage()
    {
        InitializeComponent();
        DataContext = new SettingsPageViewModel();
    }

    private void ChangeTheme_Click(object sender, RoutedEventArgs e)
    {
        //Settings.Default.DarkMode = !Settings.Default.DarkMode;

        //var paletteHelper = new PaletteHelper();

        //ITheme theme = paletteHelper.GetTheme();

        //if (Settings.Default.DarkMode)
        //    theme.SetBaseTheme(Theme.Dark);
        //else
        //    theme.SetBaseTheme(Theme.Light);

        //paletteHelper.SetTheme(theme);
    }
}