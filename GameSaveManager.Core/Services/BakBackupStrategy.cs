using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.Core.Utils;

using System.IO;

namespace GameSaveManager.Core.Services
{
    public class BakBackupStrategy : IBackupStrategy
    {
        public string GetFileExtension()
        {
            return ".bak";
        }

        public FileStream GenerateBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null) return null;

            var folder = FileSystemUtils.FindPath(gameInformation.DefaultGameSaveFolder);

            var saveName = gameInformation.BuildSaveName();

            var filesPathList = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);

            for (var i = 0; i < filesPathList.Length; i++)
            {
                var path = filesPathList[i];
                File.Copy(path, Path.Combine(FileSystemUtils.GetTempFolder(), saveName));
            }

            return new FileStream(Path.Combine(FileSystemUtils.GetTempFolder(), saveName), FileMode.Open, FileAccess.Read);
        }

        public void PrepareBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null) return;

            var saveName = Path.Combine(FileSystemUtils.FindPath(gameInformation.DefaultGameSaveFolder), gameInformation.RestoreSaveName());

            File.Move(Path.Combine(FileSystemUtils.GetTempFolder(), gameInformation.BuildSaveName()), saveName);
        }
    }
}