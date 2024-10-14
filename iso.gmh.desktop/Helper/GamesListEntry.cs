namespace iso.gmh.desktop.Helper;

public class GamesListEntry(
    string save,
    string path
)
{
    public string SaveName { get; private set; } = save;
    public string PathToFile { get; private set; } = path;
}