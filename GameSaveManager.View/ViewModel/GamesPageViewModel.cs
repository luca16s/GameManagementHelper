using Dropbox.Api;

using GameSaveManager.Core.Enums;
using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.Core.Services;
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
        private GameInformation GameInformation;

        public GamesPageViewModel()
        {
            using DropboxClient dropboxClient = (DropboxClient)Application.Current.Properties["Client"];
            if (dropboxClient != null)
            {
                _CloudOperations = new DropboxOperations(dropboxClient);
            }
        }

        private ICommand _UploadSaveCommand;

        public ICommand UploadSaveCommand => _UploadSaveCommand ??= new RelayCommand<object>(async _ => await UploadSave()
        .ConfigureAwait(true));

        private ICommand downloadSaveCommand;

        public ICommand DownloadSaveCommand => downloadSaveCommand ??= new RelayCommand<object>(async _ => await DownloadSave()
        .ConfigureAwait(true));

        private bool isButtonEnable;

        public bool IsButtonEnable
        {
            get => isButtonEnable;
            set
            {
                if (isButtonEnable == value) return;

                isButtonEnable = value;
                OnPropertyChanged(nameof(IsButtonEnable));
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
                    SaveName = $"{value}-{DateTime.Now:dd-MM-yyyy}",
                    FilePath = "",
                    CreationDate = DateTime.Now,
                    FolderName = value.ToString(),
                    GameName = value.Description(),
                    GameCoverImagePath = value.ToString(),
                };

                ImagePath = GameInformation.GameCoverImagePath;
                IsButtonEnable = value != GamesSupported.None;

                OnPropertyChanged(nameof(GamesSupported));
            }
        }

        private async Task<bool> UploadSave()
        {
            var enviromentPath = FileSystemServices.GetAppDataFolderPath(GameInformation.FolderName);

            if (_CloudOperations == null) return false;

            var exists = await _CloudOperations.CheckFolderExistence(GameInformation.FolderName).ConfigureAwait(true);

            if (!exists) await _CloudOperations.CreateFolder(GameInformation.FolderName).ConfigureAwait(true);

            var result = await _CloudOperations.UploadSaveData(enviromentPath,
                                                  $"/{GameInformation.FolderName}",
                                                  GameInformation.FolderName)
                                                  .ConfigureAwait(true);

            return string.IsNullOrEmpty(result);
        }

        private async Task<bool> DownloadSave()
        {
            if (_CloudOperations == null) return false;

            return await _CloudOperations.DownloadSaveData($"/{GameInformation.FolderName}").ConfigureAwait(true);
        }
    }
}