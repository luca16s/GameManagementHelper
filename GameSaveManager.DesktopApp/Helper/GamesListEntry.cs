namespace GameSaveManager.DesktopApp.Helper
{
    public class GamesListEntry
    {
        public GamesListEntry(string save, string path)
        {
            SaveName = save;
            PathToFile = path;
        }

        public string PathToFile { get; private set; }
        public string SaveName { get; private set; }
    }
}