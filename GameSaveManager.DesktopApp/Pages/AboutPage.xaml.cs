namespace GameSaveManager.DesktopApp.Pages
{
    using System.Windows.Controls;

    using GameSaveManager.DesktopApp.ViewModel;

    /// <summary>
    /// Interaction logic for AboutPage.xaml
    /// </summary>
    public partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();
            DataContext = new AboutViewModel();
        }
    }
}
