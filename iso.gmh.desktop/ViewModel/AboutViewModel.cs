namespace iso.gmh.desktop.ViewModel;

using System.Diagnostics;
using System.Windows.Input;

using iso.gmh.desktop.Commands;

public partial class AboutViewModel : BaseViewModel
{
    private const string UrlLinkedin = @"https:\linkedin.com\in\gianfigueiredo";
    private const string UrlGithub = @"https:\github.com\luca16s";

    private ICommand _GithubCommand;
    private ICommand _LinkedInCommand;

    public ICommand GitHubCommand => _GithubCommand ??= new RelayCommand<object>(_ => OpenWebsite(UrlGithub));
    public ICommand LinkedInCommand => _LinkedInCommand ??= new RelayCommand<object>(_ => OpenWebsite(UrlLinkedin));

    private static void OpenWebsite(string url)
    {
        var psi = new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        };

        _ = Process.Start(psi);
    }
}