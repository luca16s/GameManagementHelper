namespace GameSaveManager.DesktopApp.Pages
{
    using GameSaveManager.DesktopApp.ViewModel;

    using System.Windows.Controls;

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
