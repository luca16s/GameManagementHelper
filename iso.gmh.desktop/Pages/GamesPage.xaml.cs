namespace iso.gmh.desktop.Pages;

using System.Collections.ObjectModel;
using System.Windows.Controls;

using iso.gmh.desktop.ViewModel;
using iso.gmh.Core.Models;

using Microsoft.Extensions.Options;
using iso.gmh.Core.Enums;
using iso.gmh.Core.Interfaces;
using Dropbox.Api;

public partial class GamesPage : Page
{
    public GamesPage(
        IConnection<DropboxClient> connection,
        IOptions<ObservableCollection<Game>> options,
        IFactory<ESaveType, IBackupStrategy> backupStrategy
    )
    {
        InitializeComponent();
        DataContext = new GameViewModel(connection, options, backupStrategy);
    }
}