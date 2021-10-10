namespace GameSaveManager.Services.Backup
{
    using System;
    using System.IO;

    using GameSaveManager.Core.Enums;
    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;

    using KapiCoreLib.Desktop.Utils;
    using KapiCoreLib.Extensions;

    public class BakBackupStrategy : IBackupStrategy
    {
        public string GetFileExtension() => EBackupSaveType.BakFile.Description();

        public FileStream GenerateBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null)
                return null;

            string folder = FileSystemUtils.FindFolderPath(gameInformation.DefaultGameSaveFolder);

            string saveName = gameInformation.BuildSaveName();

            string[] filesPathList = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);

            string path = Array.Find(filesPathList, p => p.Contains(gameInformation.DefaultSaveName));
            if (!string.IsNullOrWhiteSpace(path))
            {
                File.Copy(path, Path.Combine(Path.GetTempPath(), saveName));
            }

            return new FileStream(Path.Combine(Path.GetTempPath(), saveName), FileMode.Open, FileAccess.Read);
        }

        public void PrepareBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null)
                return;

            string saveName = Path.Combine(FileSystemUtils.FindFolderPath(gameInformation.DefaultGameSaveFolder), gameInformation.RestoreSaveName());

            File.Move(Path.Combine(Path.GetTempPath(), gameInformation.BuildSaveName()), saveName, true);
        }
    }
}