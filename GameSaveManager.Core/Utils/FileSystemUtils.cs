using System;
using System.IO;

namespace GameSaveManager.Core.Utils
{
    public static class FileSystemUtils
    {
        public static string GetTempFolder()
        {
            return Path.GetTempPath();
        }

        public static string GetDocumentsFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public static string GetGameFolderLocationAppData()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        public static bool CheckFileExistence(string path)
        {
            return File.Exists(path);
        }

        public static bool DeleteCreatedFile(string path)
        {
            if (CheckFileExistence(path)) File.Delete(path);

            return !CheckFileExistence(path) || DeleteCreatedFile(path);
        }
    }
}