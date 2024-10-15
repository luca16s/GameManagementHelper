namespace iso.gmh.desktop.ViewModel;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using Dropbox.Api;

using iso.gmh.Core.Enums;
using iso.gmh.Core.Interfaces;
using iso.gmh.Core.Models;
using iso.gmh.desktop;
using iso.gmh.desktop.Commands;
using iso.gmh.desktop.Helper;
using iso.gmh.dropbox;

using Microsoft.Extensions.Options;

public partial class GameViewModel(
    IConnection<DropboxClient> Connection,
    IOptions<ObservableCollection<Game>> Options,
    IFactory<ESaveType, IBackupStrategy> BackupStrategy
) : BaseViewModel
{
    private IBackupStrategy BackupStrategy;
    private ICloudOperations CloudOperations => GetClientOperations();

    private RelayCommand<GameViewModel> _UploadCommand;
    private RelayCommand<GameViewModel> _DownloadCommand;

    private bool CanExecute => SelectedGame != null;

    public ICommand UploadCommand
        => _UploadCommand
        ??= new RelayCommand<GameViewModel>(async _ => await UploadSave(MessageBox.Show("Deseja sobrescrever o arquivo salvo?", "Game Save Manager", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No)), _ => CanExecute);

    public ICommand DownloadCommand
        => _DownloadCommand
        ??= new RelayCommand<GameViewModel>(async _ => await DownloadSave(), _ => CanExecute);

    private Game GameInformation;

    public ObservableCollection<GamesComboboxEntry> GamesSupported => new(Options.Value
        .Select(game => new GamesComboboxEntry
        {
            Title = game.Title,
            Game = game
        }).ToList());

    private GamesComboboxEntry _SelectedGame;

    public GamesComboboxEntry SelectedGame
    {
        get => _SelectedGame;
        set
        {
            if (_SelectedGame == value)
                return;

            _SelectedGame = value;

            GameInformation = _SelectedGame.Game;

            _ = GetSavesList(GameInformation);

            ImagePath = GameInformation?.CoverPath;

            UserDefinedSaveName = string.Empty;

            OnPropertyChanged();
        }
    }

    private string _ImagePath;

    public object ImagePath
    {
        get => string.IsNullOrWhiteSpace(_ImagePath)
            ? DependencyProperty.UnsetValue
            : _ImagePath;
        set
        {
            value ??= string.Empty;

            if (value.ToString().Equals(_ImagePath))
                return;

            _ImagePath = value.ToString();
            OnPropertyChanged(nameof(ImagePath));
        }
    }

    private string _UserDefinedSaveName;

    public string UserDefinedSaveName
    {
        get => CanExecute ? _UserDefinedSaveName : string.Empty;
        set
        {
            if (_UserDefinedSaveName == value)
                return;

            if (GameInformation != null)
            {
                GameInformation.UserDefinedSaveName = GameInformation?.BuildSaveName(value);
                _UserDefinedSaveName = GameInformation.UserDefinedSaveName;
            }

            OnPropertyChanged();
        }
    }

    private GamesListViewModel gamesListVM;

    public GamesListViewModel GamesListVM
    {
        get => gamesListVM;
        set
        {
            if (gamesListVM == value)
                return;

            gamesListVM = value;

            OnPropertyChanged();
        }
    }

    private ObservableCollection<string> saveList;

    public ObservableCollection<string> SaveList
    {
        get => saveList;
        set
        {
            if (saveList == value)
                return;

            saveList = value;

            OnPropertyChanged();
        }
    }

    private DropboxOperations GetClientOperations()
    {
        if (Connection is null)
            return default;

        //BackupStrategy = BackupFactory.Create(App.BackupType);

        return //client == null
               //? null
               //: 
               //new OperationFactory(BackupStrategy, client).Create(App.DriveService)
            new DropboxOperations(Connection.Client, BackupStrategy);
    }

    private async Task GetSavesList(Game gameInformation)
    {
        if (CloudOperations == null)
        {
            GamesListVM = new();
            return;
        }

        IEnumerable<(string name, string path)> listaSavesOnline = await CloudOperations.GetSavesList(gameInformation)
            ;

        GamesListVM = new(listaSavesOnline, CloudOperations, listaSavesOnline.Any());

        return;
    }

    private async Task<bool> UploadSave(MessageBoxResult messageBoxResult)
    {
        if (CloudOperations == null)
            return false;

        GameInformation.SetSaveBackupExtension(BackupStrategy.GetFileExtension());

        bool exists = await CloudOperations.CheckFolderExistence(GameInformation.OnlineSaveFolder);

        if (!exists)
            _ = await CloudOperations.CreateFolder(GameInformation.OnlineSaveFolder);

        return await CloudOperations.UploadSaveData(GameInformation, messageBoxResult == MessageBoxResult.Yes);
    }

    private async Task<bool> DownloadSave()
    {
        if (CloudOperations == null)
            return false;

        if (!string.IsNullOrWhiteSpace(GamesListVM.SelectedSave?.SaveName))
            GameInformation.UserDefinedSaveName = GamesListVM.SelectedSave?.SaveName;

        GameInformation.SetSaveBackupExtension(BackupStrategy.GetFileExtension());

        return await CloudOperations.DownloadSaveData(GameInformation);
    }
}