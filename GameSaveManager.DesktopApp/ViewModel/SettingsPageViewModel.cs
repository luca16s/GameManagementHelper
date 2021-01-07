namespace GameSaveManager.DesktopApp.ViewModel
{
    using System.Windows;

    using GameSaveManager.Core.Enums;

    public class SettingsPageViewModel : ViewModelBase
    {
        public static string Clear => "M5,13H19V11H5M3,17H17V15H3M7,7V9H21V7";
        public static string Save => "M15,9H5V5H15M12,19A3,3 0 0,1 9,16A3,3 0 0,1 12,13A3,3 0 0,1 15,16A3,3 0 0,1 12,19M17,3H5C3.89,3 3,3.9 3,5V19A2,2 0 0,0 5,21H19A2,2 0 0,0 21,19V7L17,3Z";

        public SettingsPageViewModel() => Application.Current.Properties["BACKUP_TYPE"] = BackupSaveType;

        private EDriveServices _DriveServiceSelected;

        public EDriveServices DriveServiceSelected
        {
            get => _DriveServiceSelected;
            set
            {
                if (_DriveServiceSelected == value)
                    return;

                _DriveServiceSelected = value;
                OnPropertyChanged(nameof(DriveServiceSelected));
            }
        }

        private EBackupSaveType _backupSaveType;

        public EBackupSaveType BackupSaveType
        {
            get => _backupSaveType;
            set
            {
                if (_backupSaveType == value)
                    return;

                _backupSaveType = value;
                Application.Current.Properties["BACKUP_TYPE"] = value;
                OnPropertyChanged(nameof(BackupSaveType));
            }
        }

        private string _Name;

        public string Name
        {
            get => _Name;
            set
            {
                if (_Name == value)
                    return;

                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _Email;

        public string Email
        {
            get => _Email;
            set
            {
                if (_Email == value)
                    return;

                _Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
    }
}