using System;
using System.IO;

namespace GameSaveManager.Core.Utils
{
    public static class FileSystemUtils
    {
        public static string GetGameFolderLocationAppData(string folderName) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), folderName);
    }
}