using GameSaveManager.Core.Models;

using System;
using System.IO;
using System.IO.Compression;

namespace GameSaveManager.Core.Services
{
    public static class FileSystemServices
    {
        private static string GetGameFolderLocationAppData(string folderName) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $"{folderName}");

        public static FileStream GenerateZipFile(GameInformation gameInformation)
        {
            if (gameInformation == null) return null;

            var folder = GetGameFolderLocationAppData(folderName: gameInformation.FolderName);

            ZipFile.CreateFromDirectory(folder, gameInformation.ZipTempFolder);

            return new FileStream(gameInformation.ZipTempFolder, FileMode.Open, FileAccess.Read);
        }

        public static bool CheckFileExistence(string zipTempFolder)
        {
            return File.Exists(zipTempFolder);
        }

        public static bool DeleteZipFile(GameInformation gameInformation)
        {
            if (gameInformation == null) return false;

            File.Delete(gameInformation.ZipTempFolder);

            return CheckFileExistence(gameInformation.ZipTempFolder);
        }

        public static void ExtractZipFile(GameInformation gameInformation)
        {
            if (gameInformation == null) return;

            var folder = GetGameFolderLocationAppData(folderName: gameInformation.FolderName);

            ZipFile.ExtractToDirectory(gameInformation.ZipTempFolder, folder);
        }
    }
}