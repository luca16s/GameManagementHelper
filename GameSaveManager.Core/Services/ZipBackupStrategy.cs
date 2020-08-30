using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.Core.Utils;

using System.IO;
using System.IO.Compression;

namespace GameSaveManager.Core.Services
{
    public class ZipBackupStrategy : IBackupStrategy
    {
        public string GetFileExtension() => ".zip";

        public FileStream GenerateBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null) return null;

            var folder = FileSystemUtils.GetGameFolderLocationAppData(gameInformation.DefaultGameSaveFolder);

            ZipFile.CreateFromDirectory(folder, FileSystemUtils.GetTempFolder() + gameInformation.CreateSaveName());

            return new FileStream(FileSystemUtils.GetTempFolder() + gameInformation.CreateSaveName(), FileMode.Open, FileAccess.Read);
        }

        public void PrepareBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null) return;

            var folder = FileSystemUtils.GetGameFolderLocationAppData(gameInformation.DefaultGameSaveFolder);

            ZipFile.ExtractToDirectory(FileSystemUtils.GetTempFolder() + gameInformation.CreateSaveName(), folder);
        }
    }
}