using GameSaveManager.DesktopApp.ViewModel;

using System.Windows.Controls;

namespace GameSaveManager.DesktopApp.Pages
{
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