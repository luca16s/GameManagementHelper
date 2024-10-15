namespace iso.gmh.desktop.ViewModel;

using System.Diagnostics;
using System.Windows.Input;

using iso.gmh.desktop.Commands;
using iso.gmh.desktop.Properties;

public partial class AboutViewModel : BaseViewModel
{
    public string Desenvolvedor { get; } = Resources.EMPRESA;
    public ICommand GitHubCommand { get; } = new RelayCommand<string>(static _ => OpenWebsite(Resources.GITHUB));
    public ICommand LinkedInCommand { get; } = new RelayCommand<string>(static _ => OpenWebsite(Resources.LINKEDIN));

    private static void OpenWebsite(
        string url
    ) => Process.Start(new ProcessStartInfo
    {
        FileName = url,
        UseShellExecute = true
    });
}