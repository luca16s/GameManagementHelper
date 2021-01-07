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

        private void LinkedInClick_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AboutViewModel.LinkedInCommand.CanExecute(null))
                AboutViewModel.LinkedInCommand.Execute(null);
        }

        private void GitHubClick_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (AboutViewModel.GitHubCommand.CanExecute(null))
                AboutViewModel.GitHubCommand.Execute(null);
        }
    }
}
