using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.Core.Utils;

using System.IO;
using System.IO.Compression;

namespace GameSaveManager.Core.Services
{
    public class ZipBackupStrategy : IBackupStrategy
    {
        public FileStream GenerateBackup(GameInformation gameInformation)
        {
            if (gameInformation == null) return null;

            var folder = FileSystemUtils.GetGameFolderLocationAppData(gameInformation.FolderName);

            ZipFile.CreateFromDirectory(folder, gameInformation.ZipTempFolder);

            return new FileStream(gameInformation.ZipTempFolder, FileMode.Open, FileAccess.Read);
        }
    }
}