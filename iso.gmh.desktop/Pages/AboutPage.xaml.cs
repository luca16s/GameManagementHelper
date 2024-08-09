namespace iso.gmh.desktop.Pages;

using System.Windows.Controls;

using iso.gmh.desktop.ViewModel;

public partial class AboutPage : Page
{
    public AboutPage()
    {
        InitializeComponent();
        DataContext = new AboutViewModel();
    }
}