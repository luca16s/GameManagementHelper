using System;
using System.IO;

namespace GameSaveManager.Core.Utils
{
    public static class FileSystemUtils
    {
        public static string FindPath(string folder)
        {
            return Directory.Exists(Path.Combine(GetGameFolderLocationAppData(), folder))
                ? Path.Combine(GetGameFolderLocationAppData(), folder)
                : Directory.Exists(Path.Combine(GetDocumentsFolder(), folder))
                    ? Path.Combine(GetDocumentsFolder(), folder)
                    : null;
        }

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