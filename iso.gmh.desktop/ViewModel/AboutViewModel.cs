namespace iso.gmh.desktop.ViewModel;

using System.Diagnostics;
using System.Windows.Input;

using iso.gmh.desktop.Commands;
using iso.gmh.desktop.Properties;

public partial class AboutViewModel : BaseViewModel
{
    public string Desenvolvedor { get; } = Resources.EMPRESA;
    public ICommand GitHubCommand { get; set; } = new RelayCommand<string>(_ => OpenWebsite(Resources.GITHUB));
    public ICommand LinkedInCommand { get; set; } = new RelayCommand<string>(_ => OpenWebsite(Resources.LINKEDIN));

    private static void OpenWebsite(
        string url
    )
    {
        _ = Process.Start(new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }
}