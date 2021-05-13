namespace GameSaveManager.DesktopApp.ViewModel
{
    using GameSaveManager.Core.Enums;
    using GameSaveManager.Windows;

    public class SettingsPageViewModel : BaseViewModel
    {
        private EBackupSaveType _backupSaveType;

        private EDriveServices _DriveServiceSelected;

        private string _Email;

        private string _Name;

        public SettingsPageViewModel()
        {
            App.BackupType = BackupSaveType;
            App.DriveService = DriveServiceSelected;
        }

        public EBackupSaveType BackupSaveType
        {
            get => _backupSaveType;
            set
            {
                if (_backupSaveType == value)
                    return;

                _backupSaveType =
                    App.BackupType = value;

                OnPropertyChanged();
            }
        }

        public EDriveServices DriveServiceSelected
        {
            get => _DriveServiceSelected;
            set
            {
                if (_DriveServiceSelected == value)
                    return;

                _DriveServiceSelected =
                    App.DriveService = value;

                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _Email;
            set
            {
                if (_Email == value)
                    return;

                _Email = value;

                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _Name;
            set
            {
                if (_Name == value)
                    return;

                _Name = value;

                OnPropertyChanged();
            }
        }
    }
}