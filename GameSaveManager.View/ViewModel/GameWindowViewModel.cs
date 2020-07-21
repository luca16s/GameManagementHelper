using GameSaveManager.View.Commands;

using System.Windows.Input;

namespace GameSaveManager.View.ViewModel
{
    public class GameWindowViewModel : ViewModelBase
    {
        public ICommand OpenGameOptionsTab => new RelayCommand<string>(o => SetTabName(o));

        private void SetTabName(string o)
        {
            TabName = o;
        }

        private string _TabName;
        public string TabName
        {
            get => _TabName;
            set
            {
                if (_TabName == value) return;

                _TabName = value;
                OnPropertyChanged(nameof(TabName));
            }
        }
    }
}
