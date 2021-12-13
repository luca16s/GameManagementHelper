namespace iso.gmh.desktop.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using iso.gmh.Core.Interfaces;
    using iso.gmh.desktop.Helper;
    using iso.gmh.desktop.Commands;

    public class GamesListViewModel : BaseViewModel
    {
        private ObservableCollection<GamesListEntry> savesList;

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

        private GamesListEntry selectedSave;

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

        private readonly ICloudOperations Operations;

        private RelayCommand<GamesListEntry> deleteSaveCommand;

        public ICommand DeleteSaveCommand
            => deleteSaveCommand
            ??= new RelayCommand<GamesListEntry>(async save => await DeleteSave(save).ConfigureAwait(true), _ => true);

        private Visibility showList;

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

        private async Task DeleteSave(GamesListEntry gameEntry)
        {
            if (await Operations.DeleteSave(gameEntry.PathToFile))
                _ = SavesList.Remove(gameEntry);
        }

        private static ObservableCollection<GamesListEntry> Converter(IEnumerable<(string saveName, string path)> saveList)
        {
            if (saveList == null)
                return new();

            var list = new ObservableCollection<GamesListEntry>();

            foreach ((string saveName, string path) in saveList)
                list.Add(new(saveName, path));

            return list;
        }
    }
}