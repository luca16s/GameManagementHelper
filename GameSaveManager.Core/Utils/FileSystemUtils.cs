using System;
using System.IO;

namespace GameSaveManager.Core.Utils
{
    public static class FileSystemUtils
    {
        public static string GetTempFolder() => Path.GetTempPath();

        public static string GetDocumentsFolder() => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static string GetGameFolderLocationAppData() => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static bool CheckFileExistence(string path) => File.Exists(path);

        public static bool DeleteCreatedFile(string path)
        {
            if (CheckFileExistence(path)) File.Delete(path);

            return !CheckFileExistence(path) || DeleteCreatedFile(path);
        }
    }
}