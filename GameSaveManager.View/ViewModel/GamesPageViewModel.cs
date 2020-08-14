using Dropbox.Api;

using GameSaveManager.Core.Enums;
using GameSaveManager.Core.Interfaces;
using GameSaveManager.DropboxIntegration;
using GameSaveManager.View.Commands;

using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GameSaveManager.View.ViewModel
{
    public class GamesPageViewModel : ViewModelBase
    {
        private readonly ICloudOperations _CloudOperations;

        public GamesPageViewModel()
        {
            var client = (DropboxClient)Application.Current.Properties["Client"];
            _CloudOperations = new DropboxOperations(client);
        }

        private ICommand _UploadSaveCommand;
        public ICommand UploadSaveCommand
        {
            get
            {
                if (_UploadSaveCommand == null) _UploadSaveCommand = new RelayCommand<object>(async p => 
                {
                    await UploadSave().ConfigureAwait(true);
                });
                return _UploadSaveCommand;
            }
        }

        private ICommand _DownloadSaveCommand;
        public ICommand DownloadSaveCommand
        {
            get
            {
                if (_DownloadSaveCommand == null) _DownloadSaveCommand = new RelayCommand<object>(p => DownloadSave());
                return _DownloadSaveCommand;
            }
        }

        private bool _IsButtonEnabled;
        public bool IsButtonEnable
        {
            get => _IsButtonEnabled;
            set
            {
                if (_IsButtonEnabled == value) return;

                _IsButtonEnabled = value;
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
                _ImagePath = $"../resources/gameCover/{value}.jpg";
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
                ImagePath = SetGameImage(value);
                IsButtonEnable = value != GamesSupported.None;

                OnPropertyChanged(nameof(GamesSupported));
            }
        }

        private async Task<bool> UploadSave()
        {
            var folderName = GamesSupported.ToString();

            var enviromentPath = Path
                .Combine(Environment
                .GetFolderPath(Environment.SpecialFolder.ApplicationData), $"{folderName}");

            var exists = await _CloudOperations.CheckFolderExistence(folderName).ConfigureAwait(true);

            if (!exists) await _CloudOperations.CreateFolder(folderName).ConfigureAwait(true);

            var result = await _CloudOperations.UploadSaveData(enviromentPath,
                                                  $"/{folderName}",
                                                  folderName)
                                                  .ConfigureAwait(true);

            return string.IsNullOrEmpty(result);
        }

        private bool DownloadSave()
        {
            return _CloudOperations.DownloadSaveData();
        }

        private static string SetGameImage(GamesSupported game) => game.ToString();
    }
}
