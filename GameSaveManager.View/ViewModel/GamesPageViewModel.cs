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
        private readonly ICloudOperations _CloudOperations;

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

        public GamesPageViewModel(IBackupStrategy backupStrategy)
        {
            using DropboxClient dropboxClient = (DropboxClient)Application.Current.Properties["CLIENT"];
            if (dropboxClient != null)
            {
                _CloudOperations = new DropboxOperations(backupStrategy, dropboxClient);
            }
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
                    SaveName = $"{value}-{DateTime.Now:MM-dd-yyyy}.zip",
                    FilePath = "",
                    CreationDate = DateTime.Now,
                    FolderName = value.ToString(),
                    GameName = value.Description(),
                    GameCoverImagePath = value.ToString(),
                };

                ImagePath = GameInformation.GameCoverImagePath;

                OnPropertyChanged(nameof(GamesSupported));
            }
        }

        private async Task<bool> UploadSave()
        {
            if (_CloudOperations == null) return false;

            var exists = await _CloudOperations.CheckFolderExistence(GameInformation.FolderName).ConfigureAwait(true);

            if (!exists) await _CloudOperations.CreateFolder(GameInformation.FolderName).ConfigureAwait(true);

            return await _CloudOperations.UploadSaveData(GameInformation).ConfigureAwait(true);
        }

        private async Task<bool> DownloadSave()
        {
            if (_CloudOperations == null) return false;

            return await _CloudOperations.DownloadSaveData(GameInformation).ConfigureAwait(true);
        }
    }
}