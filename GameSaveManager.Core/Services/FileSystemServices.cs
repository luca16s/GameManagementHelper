using System;
using System.IO;

namespace GameSaveManager.Core.Services
{
    public static class FileSystemServices
    {
        public static string GetAppDataFolderPath(string folderName) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $"{folderName}");
    }
}