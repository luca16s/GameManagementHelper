using GameSaveManager.Core.Enums;
using GameSaveManager.View.Commands;
using GameSaveManager.View.Properties;

using System.Windows.Input;

namespace GameSaveManager.View.ViewModel
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public ICommand SaveCommand => new RelayCommand<Settings>(_ => Save());
        public ICommand ClearCommand => new RelayCommand<Settings>(_ => Clear());

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

        public void Save()
        {
        }

        private void Clear()
        {
            DriveServiceSelected = DriveServicesEnum.Dropbox;
        }
    }
}