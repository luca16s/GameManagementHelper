using GameSaveManager.Core.Enums;

using System.Windows.Input;

namespace GameSaveManager.View.ViewModel
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        public SettingsWindowViewModel()
        {
            //ChangeDarkMode = new ChangeDarkModeCommand();
        }

        public ICommand ChangeDarkMode { get; set; }
        //public ICommand SaveCommand => new RelayCommand<SettingsModel>(o => Save());
        //public ICommand ClearCommand => new RelayCommand<SettingsModel>(o => Clear());

        private bool _DarkMode;
        public bool DarkMode
        {
            get => _DarkMode;
            set
            {
                if (_DarkMode == value) return;

                _DarkMode = value;
                OnPropertyChanged(nameof(DarkMode));
            }
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

        //public void Save()
        //{
        //    ISettingsOperations operations = new SettingsPersistence();
        //    var json = operations.GenereteSettingsJson(
        //        new SettingsModel
        //        {
        //            DarkMode = DarkMode,
        //            DriveSelected = (int)DriveServiceSelected
        //        });
        //    operations.SaveSettings(json);
        //}

        private void Clear()
        {
            DriveServiceSelected = DriveServicesEnum.GoogleDrive;
            DarkMode = false;
        }
    }
}
