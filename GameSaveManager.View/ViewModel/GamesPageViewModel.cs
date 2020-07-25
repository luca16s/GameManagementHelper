using GameSaveManager.Core.Enums;

using System.Windows.Input;

namespace GameSaveManager.View.ViewModel
{
    public class GamesPageViewModel : ViewModelBase
    {
        //public ICommand DownloadSaveCommand { get; set; }
        //public ICommand UploadSaveCommand { get; set; }

        private bool _IsButtonEnabled = false;
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

        //private static bool EnableBackupButtons()
        //{

        //}

        private static string SetGameImage(GamesSupported game)
        {
            return game.ToString();
        }
    }
}
