namespace GameSaveManager.DesktopApp.Pages
{
    using System.Windows.Controls;

    using GameSaveManager.DesktopApp.ViewModel;

    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            DataContext = new SettingsPageViewModel();
        }
    }
}