using GameSaveManager.View.ViewModel;

using System.Windows.Controls;

namespace GameSaveManager.View.Pages
{
    /// <summary>
    /// Interaction logic for GamesPage.xaml
    /// </summary>
    public partial class GamesPage : Page
    {
        public GamesPage()
        {
            InitializeComponent();
            DataContext = new GamesPageViewModel();
        }
    }
}