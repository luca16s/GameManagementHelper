using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.View.ViewModel;

using Microsoft.Extensions.Options;

using System.Collections.Generic;
using System.Windows.Controls;

namespace GameSaveManager.View.Pages
{
    /// <summary>
    /// Interaction logic for GamesPage.xaml
    /// </summary>
    public partial class GamesPage : Page
    {
        public GamesPage(IFactory<IBackupStrategy> backupStrategy, IOptions<List<GameInformationModel>> options)
        {
            InitializeComponent();
            DataContext = new GamesPageViewModel(backupStrategy, options);
        }
    }
}