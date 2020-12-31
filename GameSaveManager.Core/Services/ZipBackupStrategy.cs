namespace GameSaveManager.Core.Services
{
    using System.IO;
    using System.IO.Compression;

    using GameSaveManager.Core.Enums;
    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;
    using GameSaveManager.Core.Utils;

    public class ZipBackupStrategy : IBackupStrategy
    {
        public string GetFileExtension() => EBackupSaveType.ZipFile.Description();

        public FileStream GenerateBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null)
                return null;

            string folder = FileSystemUtils.FindPath(gameInformation.DefaultGameSaveFolder);

            string saveName = gameInformation.BuildSaveName();

            ZipFile.CreateFromDirectory(folder, Path.Combine(FileSystemUtils.GetTempFolder(), saveName));

            return new FileStream(Path.Combine(FileSystemUtils.GetTempFolder(), saveName), FileMode.Open, FileAccess.Read);
        }

        public void PrepareBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null)
                return;

            string folder = FileSystemUtils.FindPath(gameInformation.DefaultGameSaveFolder);

            ZipFile.ExtractToDirectory(Path.Combine(FileSystemUtils.GetTempFolder(), gameInformation.BuildSaveName()), folder, true);
        }
    }
}