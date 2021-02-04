namespace GameSaveManager.Core.Utils
{
    using System;
    using System.IO;

    public static class FileSystemUtils
    {
        public static string FindPath(string folder)
        {
            if (Directory.Exists(Path.Combine(GetGameFolderLocationAppData(), folder)))
                return Path.Combine(GetGameFolderLocationAppData(), folder);
            else if (Directory.Exists(Path.Combine(GetDocumentsFolder(), folder)))
                return Path.Combine(GetDocumentsFolder(), folder);

            return string.Empty;
        }

        public static string GetTempFolder() => Path.GetTempPath();

        public static string GetDocumentsFolder() => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static string GetGameFolderLocationAppData() => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static bool DeleteCreatedFile(string path)
        {
            if (File.Exists(path))
                File.Delete(path);

            return !File.Exists(path) || DeleteCreatedFile(path);
        }
    }
}