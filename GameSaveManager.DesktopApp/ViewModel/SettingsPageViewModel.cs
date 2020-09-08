using GameSaveManager.Core.Enums;

using System.Windows;

namespace GameSaveManager.DesktopApp.ViewModel
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public SettingsPageViewModel()
        {
            Application.Current.Properties["BACKUP_TYPE"] = BackupSaveType;
        }

        private DriveServicesEnum _DriveServiceSelected;

        public DriveServicesEnum DriveServiceSelected
        {
            get => _DriveServiceSelected;
            set
            {
                if (_DriveServiceSelected == value) return;

                _DriveServiceSelected = value;
                OnPropertyChanged(nameof(DriveServiceSelected));
            }
        }

        private BackupSaveType _backupSaveType;

        public BackupSaveType BackupSaveType
        {
            get => _backupSaveType;
            set
            {
                if (_backupSaveType == value) return;

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
                if (_Name == value) return;

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
                if (_Email == value) return;

                _Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
    }
}