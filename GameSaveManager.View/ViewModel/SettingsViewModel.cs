using GameSaveManager.Core;
using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.Infra;
using GameSaveManager.WPF.Commands;

using System.ComponentModel;
using System.Windows.Input;

namespace GameSaveManager.WPF.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel()
        {
            ChangeDarkMode = new ChangeDarkModeCommand();
        }

        public ICommand ChangeDarkMode { get; set; }
        public ICommand SaveCommand => new RelayCommand<SettingsModel>(o => Save());

        public void Save()
        {
            ISettingsOperations operations = new SettingsPersistence();
            var json = operations.GenereteSettingsJson(new SettingsModel { DarkMode = DarkMode });
            operations.SaveSettings(json);
        }

        private bool _DarkMode;
        public bool DarkMode
        {
            get => _DarkMode;
            set 
            {
                if (_DarkMode == value) return;

                _DarkMode = value;
                OnPropertyChanged("DarkMode");
            }
        }
    }
}
