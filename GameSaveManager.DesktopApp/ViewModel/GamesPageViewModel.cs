namespace GameSaveManager.DesktopApp.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    using Dropbox.Api;

    using GameSaveManager.Core.Enums;
    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;
    using GameSaveManager.DesktopApp.Commands;
    using GameSaveManager.DesktopApp.Helper;
    using GameSaveManager.DropboxApi;

    using Microsoft.Extensions.Options;

    public class GamesPageViewModel : BaseViewModel
    {
        private IBackupStrategy BackupStrategy;
        private ICloudOperations CloudOperations => GetConnectionClient();
        private readonly IFactory<IBackupStrategy> BackupFactory;

        private RelayCommand<GamesPageViewModel> _UploadCommand;
        private RelayCommand<GamesPageViewModel> _DownloadCommand;

        private bool CanExecute => SelectedGame != null;

        public ICommand UploadCommand
            => _UploadCommand
            ??= new RelayCommand<GamesPageViewModel>(async _ => await UploadSave(MessageBox.Show("Deseja sobrescrever o arquivo salvo?", "Game Save Manager", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No)).ConfigureAwait(true), _ => CanExecute);

        public ICommand DownloadCommand
            => _DownloadCommand
            ??= new RelayCommand<GamesPageViewModel>(async _ => await DownloadSave().ConfigureAwait(true), _ => CanExecute);

        private GameInformationModel GameInformation;
        private readonly ObservableCollection<GameInformationModel> GameInformationList;

        public GamesPageViewModel(IFactory<IBackupStrategy> backupStrategy, IOptions<ObservableCollection<GameInformationModel>> options)
        {
            if (backupStrategy == null
                || options == null)
                return;

            BackupFactory = backupStrategy;
            GameInformationList = options.Value;
        }

        public ObservableCollection<GamesComboboxEntry> GamesSupported => new(GameInformationList
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

        private ICloudOperations GetConnectionClient()
        {
            using var client = (DropboxClient)Application.Current.Properties["CLIENT"];
            var backupSaveType = (EBackupSaveType)Application.Current.Properties["BACKUP_TYPE"];
            BackupStrategy = BackupFactory.Create(backupSaveType);

            return client == null
                ? null
                : new DropboxOperations(BackupStrategy, client);
        }

        private async Task GetSavesList(GameInformationModel gameInformation)
        {
            if (CloudOperations == null)
            {
                GamesListVM = new();
                return;
            }

            IEnumerable<(string name, string path)> listaSavesOnline = await CloudOperations.GetSavesList(gameInformation)
                .ConfigureAwait(true);

            GamesListVM = new(listaSavesOnline, CloudOperations, listaSavesOnline.Any());

            return;
        }

        private async Task<bool> UploadSave(MessageBoxResult messageBoxResult)
        {
            if (CloudOperations == null)
                return false;

            GameInformation.SetSaveBackupExtension(BackupStrategy.GetFileExtension());

            bool exists = await CloudOperations.CheckFolderExistence(GameInformation.OnlineSaveFolder).ConfigureAwait(true);

            if (!exists)
                _ = await CloudOperations.CreateFolder(GameInformation.OnlineSaveFolder).ConfigureAwait(true);

            return await CloudOperations.UploadSaveData(GameInformation, messageBoxResult == MessageBoxResult.Yes).ConfigureAwait(true);
        }

        private async Task<bool> DownloadSave()
        {
            if (CloudOperations == null)
                return false;

            if (!string.IsNullOrWhiteSpace(GamesListVM.SelectedSave?.SaveName))
                GameInformation.UserDefinedSaveName = GamesListVM.SelectedSave?.SaveName;

            GameInformation.SetSaveBackupExtension(BackupStrategy.GetFileExtension());

            return await CloudOperations.DownloadSaveData(GameInformation).ConfigureAwait(true);
        }
    }
}