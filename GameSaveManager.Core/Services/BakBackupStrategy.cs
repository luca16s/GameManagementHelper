namespace GameSaveManager.Core.Services
{
    using System;
    using System.IO;
    using System.Linq;

    using GameSaveManager.Core.Enums;
    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;
    using GameSaveManager.Core.Utils;

    public class BakBackupStrategy : IBackupStrategy
    {
        public string GetFileExtension() => EBackupSaveType.BakFile.Description();

        public FileStream GenerateBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null)
                return null;

            string folder = FileSystemUtils.FindPath(gameInformation.DefaultGameSaveFolder);

            string saveName = gameInformation.BuildSaveName();

            string[] filesPathList = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);

            string path = Array.Find(filesPathList, p => p.Contains(gameInformation.DefaultSaveName));
            if (!string.IsNullOrWhiteSpace(path))
            {
                File.Copy(path, Path.Combine(FileSystemUtils.GetTempFolder(), saveName));
            }

            return new FileStream(Path.Combine(FileSystemUtils.GetTempFolder(), saveName), FileMode.Open, FileAccess.Read);
        }

        public void PrepareBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null)
                return;

            string saveName = Path.Combine(FileSystemUtils.FindPath(gameInformation.DefaultGameSaveFolder), gameInformation.RestoreSaveName());

            File.Move(Path.Combine(FileSystemUtils.GetTempFolder(), gameInformation.BuildSaveName()), saveName, true);
        }
    }
}