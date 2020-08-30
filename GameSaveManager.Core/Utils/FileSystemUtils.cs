using System;
using System.IO;

namespace GameSaveManager.Core.Utils
{
    public static class FileSystemUtils
    {
        public static string GetTempFolder() => Path.GetTempPath();

        public static string GetGameFolderLocationAppData(string folderName) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), folderName);

        public static bool CheckFileExistence(string path) => File.Exists(path);

        public static bool DeleteZipFile(string path)
        {
            File.Delete(path);

            return CheckFileExistence(path);
        }
    }
}