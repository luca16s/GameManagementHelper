namespace GameSaveManager.DesktopApp.Pages
{
    using System.Windows.Controls;

    using GameSaveManager.DesktopApp.ViewModel;

    public partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();
            DataContext = new AboutViewModel();
        }
    }
}
