using GameSaveManager.DesktopApp.Commands;

using System.Diagnostics;
using System.Windows.Input;

namespace GameSaveManager.DesktopApp.ViewModel
{
    public class AboutViewModel : ViewModelBase
    {
        private ICommand _GithubCommand;
        private ICommand _LinkedInCommand;

        public ICommand GitHubCommand => _GithubCommand ??= new RelayCommand<object>(_ => OpenWebsite("https://github.com/luca16s"));
        public ICommand LinkedInCommand => _LinkedInCommand ??= new RelayCommand<object>(_ => OpenWebsite("https://linkedin.com/in/gianfigueiredo"));

        private static void OpenWebsite(string url)
        {
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
