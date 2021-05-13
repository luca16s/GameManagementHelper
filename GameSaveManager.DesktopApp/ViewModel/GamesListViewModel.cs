namespace GameSaveManager.DesktopApp.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.DesktopApp.Commands;
    using GameSaveManager.DesktopApp.Helper;

    public class GamesListViewModel : BaseViewModel
    {
        private readonly ICloudOperations Operations;
        private RelayCommand<GamesListEntry> deleteSaveCommand;
        private ObservableCollection<GamesListEntry> savesList;

        private GamesListEntry selectedSave;

        private Visibility showList;

        public GamesListViewModel()
            : this(null, null, false)
        { }

        public GamesListViewModel(IEnumerable<(string saveName, string path)> saveList, ICloudOperations operations, bool visible)
        {
            SavesList = Converter(saveList);
            ShowList = visible
                    ? Visibility.Visible
                    : Visibility.Collapsed;

            Operations = operations;
        }

        public ICommand DeleteSaveCommand
            => deleteSaveCommand
            ??= new RelayCommand<GamesListEntry>(async save => await DeleteSave(save).ConfigureAwait(true), _ => true);

        public ObservableCollection<GamesListEntry> SavesList
        {
            get => savesList;
            set
            {
                if (savesList == value)
                    return;

                savesList = value;
                OnPropertyChanged();
            }
        }

        public GamesListEntry SelectedSave
        {
            get => selectedSave;
            set
            {
                if (selectedSave == value)
                    return;

                selectedSave = value;
                OnPropertyChanged();
            }
        }

        public Visibility ShowList
        {
            get => showList;
            set
            {
                if (showList == value)
                    return;

                showList = value;
                OnPropertyChanged();
            }
        }

        private static ObservableCollection<GamesListEntry> Converter(IEnumerable<(string saveName, string path)> saveList)
        {
            if (saveList == null)
                return new();

            var list = new ObservableCollection<GamesListEntry>();

            foreach ((string saveName, string path) in saveList)
            {
                list.Add(new(saveName, path));
            }

            return list;
        }

        private async Task DeleteSave(GamesListEntry gameEntry)
        {
            if (await Operations.DeleteSave(gameEntry.PathToFile))
                _ = SavesList.Remove(gameEntry);
        }
    }
}