using GameSaveManager.Core.Models;

using System;
using System.IO;
using System.IO.Compression;

namespace GameSaveManager.Core.Services
{
    public class FileSystemServices
    {
        private readonly GameInformation GameInformation;

        public FileSystemServices(GameInformation gameInformation)
        {
            if (gameInformation == null) return;

            GameInformation = gameInformation;
        }

        private string GetGameFolderLocationAppData() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GameInformation.FolderName);

        public bool CheckFileExistence()
        {
            return File.Exists(GameInformation.ZipTempFolder);
        }

        public FileStream GenerateZipFile()
        {
            var folder = GetGameFolderLocationAppData();

            ZipFile.CreateFromDirectory(folder, GameInformation.ZipTempFolder);

            return new FileStream(GameInformation.ZipTempFolder, FileMode.Open, FileAccess.Read);
        }

        public bool DeleteZipFile()
        {
            File.Delete(GameInformation.ZipTempFolder);

            return CheckFileExistence();
        }

        public void ExtractZipFile()
        {
            var folder = GetGameFolderLocationAppData();

            ZipFile.ExtractToDirectory(GameInformation.ZipTempFolder, folder);
        }

        public void CreateBackupFile()
        {
            File.Copy(GameInformation.SaveName, GameInformation.ZipTempFolder);
        }
    }
}