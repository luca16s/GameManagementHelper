namespace GameSaveManager.DesktopApp.Pages
{
    using System.Windows.Controls;

    using GameSaveManager.DesktopApp.ViewModel;

    public partial class AboutPage : Page
    {
        public static AboutViewModel AboutViewModel => new();

        public AboutPage()
        {
            InitializeComponent();
            DataContext = AboutViewModel;
        }
    }
}
