namespace iso.gmh.desktop.Pages;

using System.Collections.ObjectModel;
using System.Windows.Controls;

using iso.gmh.desktop.ViewModel;
using iso.gmh.Core.Models;

using Microsoft.Extensions.Options;
using iso.gmh.Core.Enums;
using iso.gmh.Core.Interfaces;

public partial class GamesPage : Page
{
    public GamesPage(
        IFactory<ESaveType, IBackupStrategy> backupStrategy,
        IOptions<ObservableCollection<GameInformationModel>> options
    )
    {
        InitializeComponent();
        DataContext = new GamesPageViewModel(backupStrategy, options);
    }
}