using Dropbox.Api;

using GameSaveManager.Core.Enums;
using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.DesktopApp.Commands;
using GameSaveManager.Dropbox;

using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GameSaveManager.DesktopApp.ViewModel
{
    public class GamesPageViewModel : ViewModelBase
    {
        private readonly IFactory<IBackupStrategy> BackupFactory;
        private IBackupStrategy BackupStrategy;

        private ICloudOperations CloudOperations => GetConnectionClient();

        private RelayCommand<GamesPageViewModel> _UploadCommand;
        private RelayCommand<GamesPageViewModel> _DownloadCommand;

        private bool CanExecute => GamesSupported != GamesSupported.None;

        public ICommand UploadCommand
            => _UploadCommand
            ??= new RelayCommand<GamesPageViewModel>(async _ => await UploadSave().ConfigureAwait(true), _ => CanExecute);

        public ICommand DownloadCommand
            => _DownloadCommand
            ??= new RelayCommand<GamesPageViewModel>(async _ => await DownloadSave().ConfigureAwait(true), _ => CanExecute);

        private GameInformationModel GameInformation;
        private readonly List<GameInformationModel> GameInformationList;

        public GamesPageViewModel(IFactory<IBackupStrategy> backupStrategy, IOptions<List<GameInformationModel>> options)
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

                if (value.ToString().Equals(_ImagePath)) return;
                _ImagePath = value.ToString();
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

                GameInformation = GameInformationList.Find(game => string.Equals(value.ToString(), game.Name, StringComparison.InvariantCultureIgnoreCase));

                ImagePath = GameInformation?.CoverPath;

                SaveName = string.Empty;

                OnPropertyChanged(nameof(GamesSupported));
            }
        }

        private string _SaveName;

        public string SaveName
        {
            get
            {
                if (CanExecute)
                    return _SaveName;

                return string.Empty;
            }
            set
            {
                if (_SaveName == value) return;

                _SaveName = GameInformation?.BuildSaveName(value);

                OnPropertyChanged(nameof(SaveName));
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

            GameInformation.SaveBackupExtension = BackupStrategy.GetFileExtension();

            var exists = await CloudOperations.CheckFolderExistence(GameInformation.OnlineSaveFolder).ConfigureAwait(true);

            if (!exists) await CloudOperations.CreateFolder(GameInformation.OnlineSaveFolder).ConfigureAwait(true);

            return await CloudOperations.UploadSaveData(GameInformation).ConfigureAwait(true);
        }

        private async Task<bool> DownloadSave()
        {
            if (CloudOperations == null) return false;

            GameInformation.SaveBackupExtension = BackupStrategy.GetFileExtension();

            return await CloudOperations.DownloadSaveData(GameInformation).ConfigureAwait(true);
        }
    }
}