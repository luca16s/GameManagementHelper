namespace GameSaveManager.DesktopApp.ViewModel
{
    using System;
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
    using GameSaveManager.DropboxApi;

    using Microsoft.Extensions.Options;

    public class GamesPageViewModel : ViewModelBase
    {
        private readonly IFactory<IBackupStrategy> BackupFactory;
        private IBackupStrategy BackupStrategy;

        private ICloudOperations CloudOperations => GetConnectionClient();

        private RelayCommand<GamesPageViewModel> _UploadCommand;
        private RelayCommand<GamesPageViewModel> _DownloadCommand;

        private bool CanExecute => GamesSupported != EGamesSupported.None;

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

        private EGamesSupported _GamesSupported;

        public EGamesSupported GamesSupported
        {
            get => _GamesSupported;
            set
            {
                if (_GamesSupported == value)
                    return;

                _GamesSupported = value;

                GameInformation = GameInformationList
                    .FirstOrDefault(game => string.Equals(value.ToString(), game.Name, StringComparison.InvariantCultureIgnoreCase));

                ImagePath = GameInformation?.CoverPath;

                UserDefinedSaveName = string.Empty;

                OnPropertyChanged(nameof(GamesSupported));
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

                GameInformation.UserDefinedSaveName = GameInformation?.BuildSaveName(value);

                _UserDefinedSaveName = GameInformation.UserDefinedSaveName;

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