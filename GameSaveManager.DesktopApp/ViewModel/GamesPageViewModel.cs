namespace GameSaveManager.DesktopApp.ViewModel
{
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

    public class GamesPageViewModel : ViewModelBase
    {
        private readonly IFactory<IBackupStrategy> BackupFactory;
        private IBackupStrategy BackupStrategy;

        public static string Download => "M17,13L12,18L7,13H10V9H14V13M19.35,10.03C18.67,6.59 15.64,4 12,4C9.11,4 6.6,5.64 5.35,8.03C2.34,8.36 0,10.9 0,14A6,6 0 0,0 6,20H19A5,5 0 0,0 24,15C24,12.36 21.95,10.22 19.35,10.03Z";
        public static string Upload => "M14,13V17H10V13H7L12,8L17,13M19.35,10.03C18.67,6.59 15.64,4 12,4C9.11,4 6.6,5.64 5.35,8.03C2.34,8.36 0,10.9 0,14A6,6 0 0,0 6,20H19A5,5 0 0,0 24,15C24,12.36 21.95,10.22 19.35,10.03Z";

        private ICloudOperations CloudOperations => GetConnectionClient();

        private RelayCommand<GamesPageViewModel> _UploadCommand;
        private RelayCommand<GamesPageViewModel> _DownloadCommand;

        private bool CanExecute => SelectedGame != null;

        public ICommand UploadCommand
            => _UploadCommand
            ??= new RelayCommand<GamesPageViewModel>(async _ => await UploadSave().ConfigureAwait(true), _ => CanExecute);

        public ICommand DownloadCommand
            => _DownloadCommand
            ??= new RelayCommand<GamesPageViewModel>(async _ => await DownloadSave().ConfigureAwait(true), _ => CanExecute);

        private GameInformationModel GameInformation;
        private readonly ObservableCollection<GameInformationModel> GameInformationList;

        public GamesPageViewModel(IFactory<IBackupStrategy> backupStrategy, IOptions<ObservableCollection<GameInformationModel>> options)
        {
            if (options == null)
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

                OnPropertyChanged(nameof(UserDefinedSaveName));
            }
        }

        private ICloudOperations GetConnectionClient()
        {
            using var DriveConnectionClient = (DropboxClient)Application.Current.Properties["CLIENT"];

            var backupType = (EBackupSaveType)Application.Current.Properties["BACKUP_TYPE"];
            BackupStrategy = BackupFactory.Create(backupType);

            return DriveConnectionClient != null
                ? new DropboxOperations(BackupStrategy, DriveConnectionClient)
                : null;
        }

        private async Task<bool> UploadSave()
        {
            if (CloudOperations == null)
                return false;

            GameInformation.SetSaveBackupExtension(BackupStrategy.GetFileExtension());

            bool exists = await CloudOperations.CheckFolderExistence(GameInformation.OnlineSaveFolder).ConfigureAwait(true);

            if (!exists)
                _ = await CloudOperations.CreateFolder(GameInformation.OnlineSaveFolder).ConfigureAwait(true);

            return await CloudOperations.UploadSaveData(GameInformation).ConfigureAwait(true);
        }

        private async Task<bool> DownloadSave()
        {
            if (CloudOperations == null)
                return false;

            GameInformation.SetSaveBackupExtension(BackupStrategy.GetFileExtension());

            return await CloudOperations.DownloadSaveData(GameInformation).ConfigureAwait(true);
        }
    }
}