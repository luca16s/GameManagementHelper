namespace GameSaveManager.DesktopApp.Helper
{
    public class GamesListEntry
    {
        public string SaveName { get; private set; }
        public string PathToFile { get; private set; }

        public GamesListEntry(string save, string path)
        {
            SaveName = save;
            PathToFile = path;
        }
    }
}
