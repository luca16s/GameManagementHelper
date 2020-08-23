using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.Core.Utils;

using System.IO;

namespace GameSaveManager.Core.Services
{
    public class BakBackupStrategy : IBackupStrategy
    {
        public FileStream GenerateBackup(GameInformation gameInformation)
        {
            if (gameInformation == null) return null;

            var folder = FileSystemUtils.GetGameFolderLocationAppData(gameInformation.FolderName);

            File.Copy(folder, gameInformation.ZipTempFolder);

            return new FileStream(gameInformation.ZipTempFolder, FileMode.Open, FileAccess.Read);
        }

        public void PrepareBackup(GameInformation gameInformation)
        {
            if (gameInformation == null) return;

            throw new System.NotImplementedException();
        }
    }
}