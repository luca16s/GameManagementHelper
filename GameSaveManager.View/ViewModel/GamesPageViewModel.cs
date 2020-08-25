using Dropbox.Api;

using GameSaveManager.Core.Enums;
using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.DropboxIntegration;
using GameSaveManager.View.Commands;
using GameSaveManager.View.Helper;

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GameSaveManager.View.ViewModel
{
    public class GamesPageViewModel : ViewModelBase
    {
        private readonly IFactory<IBackupStrategy> BackupFactory;
        private IBackupStrategy BackupStrategy;

        private ICloudOperations _CloudOperations;
        private ICloudOperations CloudOperations => _CloudOperations ??= GetConnectionClient();

        private RelayCommand<GamesPageViewModel> _UploadCommand;
        private RelayCommand<GamesPageViewModel> _DownloadCommand;

        private bool CanUpload => GamesSupported != GamesSupported.None;
        private bool CanDownload => GamesSupported != GamesSupported.None;

        public ICommand UploadCommand
            => _UploadCommand
            ??= new RelayCommand<GamesPageViewModel>(async _ => await UploadSave().ConfigureAwait(true), _ => CanUpload);

        public ICommand DownloadCommand
            => _DownloadCommand
            ??= new RelayCommand<GamesPageViewModel>(async _ => await DownloadSave().ConfigureAwait(true), _ => CanDownload);

        private GameInformation GameInformation;

        public GamesPageViewModel(IFactory<IBackupStrategy> backupStrategy)
        {
            BackupFactory = backupStrategy;
        }

        private string _ImagePath;

        public string ImagePath
        {
            get => _ImagePath;
            set
            {
                if (_ImagePath == value) return;
                _ImagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        private GamesSupported _GamesSupported;

        public GamesSupported GamesSupported
        {
            get => _GamesSupported;
            set
            {
                if (_GamesSupported == value) return;

                _GamesSupported = value;

                GameInformation = new GameInformation
                {
                    SaveName = value.ToString(),
                    FilePath = "",
                    CreationDate = DateTime.Now,
                    FolderName = value.ToString(),
                    GameName = value.Description(),
                    GameCoverImagePath = value.ToString(),
                    GameSaveExtension = "*.sl2",
                };

                ImagePath = GameInformation.GameCoverImagePath;

                OnPropertyChanged(nameof(GamesSupported));
            }
        }

        private ICloudOperations GetConnectionClient()
        {
            using var DriveConnectionClient = (DropboxClient)Application.Current.Properties["CLIENT"];

            var backupType = (BackupSaveType)Application.Current.Properties["BACKUP_TYPE"];
            BackupStrategy = BackupFactory.Create(backupType);

            if (DriveConnectionClient != null) return new DropboxOperations(BackupStrategy, DriveConnectionClient);

            return null;
        }

        private async Task<bool> UploadSave()
        {
            if (CloudOperations == null) return false;

            GameInformation.BackupFileExtension = BackupStrategy.GetFileExtension();

            var exists = await CloudOperations.CheckFolderExistence(GameInformation.FolderName).ConfigureAwait(true);

            if (!exists) await CloudOperations.CreateFolder(GameInformation.FolderName).ConfigureAwait(true);

            return await CloudOperations.UploadSaveData(GameInformation).ConfigureAwait(true);
        }

        private async Task<bool> DownloadSave()
        {
            if (CloudOperations == null) return false;

            GameInformation.BackupFileExtension = BackupStrategy.GetFileExtension();

            return await CloudOperations.DownloadSaveData(GameInformation).ConfigureAwait(true);
        }
    }
}