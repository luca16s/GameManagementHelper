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

            var folder = FileSystemUtils.FindPath(gameInformation.DefaultGameSaveFolder);

            var saveName = gameInformation.BuildSaveName();

            ZipFile.CreateFromDirectory(folder, Path.Combine(FileSystemUtils.GetTempFolder(), saveName));

            return new FileStream(Path.Combine(FileSystemUtils.GetTempFolder(), saveName), FileMode.Open, FileAccess.Read);
        }

        public void PrepareBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null) return;

            var folder = FileSystemUtils.FindPath(gameInformation.DefaultGameSaveFolder);

            ZipFile.ExtractToDirectory(Path.Combine(FileSystemUtils.GetTempFolder(), gameInformation.BuildSaveName()), folder);
        }
    }
}