namespace GameSaveManager.Core.Services
{
    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Models;
    using GameSaveManager.Core.Utils;

    using System.IO;

    public class BakBackupStrategy : IBackupStrategy
    {
        public string GetFileExtension() => ".bak";

        public FileStream GenerateBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null) return null;

            string folder = FileSystemUtils.FindPath(gameInformation.DefaultGameSaveFolder);

            string saveName = gameInformation.BuildSaveName();

            string[] filesPathList = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);

            for (int i = 0; i < filesPathList.Length; i++)
            {
                string path = filesPathList[i];
                File.Copy(path, Path.Combine(FileSystemUtils.GetTempFolder(), saveName));
            }

            return new FileStream(Path.Combine(FileSystemUtils.GetTempFolder(), saveName), FileMode.Open, FileAccess.Read);
        }

        public void PrepareBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null) return;

            string saveName = Path.Combine(FileSystemUtils.FindPath(gameInformation.DefaultGameSaveFolder), gameInformation.RestoreSaveName());

            File.Move(Path.Combine(FileSystemUtils.GetTempFolder(), gameInformation.BuildSaveName()), saveName);
        }
    }
}