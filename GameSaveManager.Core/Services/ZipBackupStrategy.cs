using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.Core.Utils;

using System.IO;
using System.IO.Compression;

namespace GameSaveManager.Core.Services
{
    public class ZipBackupStrategy : IBackupStrategy
    {
        public string GetFileExtension()
        {
            return ".zip";
        }

        public FileStream GenerateBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null) return null;

            var folder = FileSystemUtils.GetGameFolderLocationAppData() + "\\" + gameInformation.DefaultGameSaveFolder;

            var saveName = gameInformation.BuildSaveName();

            ZipFile.CreateFromDirectory(folder, FileSystemUtils.GetTempFolder() + saveName);

            return new FileStream(FileSystemUtils.GetTempFolder() + saveName, FileMode.Open, FileAccess.Read);
        }

        public void PrepareBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null) return;

            var folder = FileSystemUtils.GetGameFolderLocationAppData() + "\\" + gameInformation.DefaultGameSaveFolder;

            ZipFile.ExtractToDirectory(FileSystemUtils.GetTempFolder() + gameInformation.BuildSaveName(), folder);
        }
    }
}