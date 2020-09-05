using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;
using GameSaveManager.Core.Utils;

using System;
using System.IO;

namespace GameSaveManager.Core.Services
{
    public class BakBackupStrategy : IBackupStrategy
    {
        public string GetFileExtension() => ".bak";

        public FileStream GenerateBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null) return null;

            var folder = FileSystemUtils.GetGameFolderLocationAppData() + "\\" + gameInformation.DefaultGameSaveFolder;

            var filesPathList = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);

            for (int i = 0; i < filesPathList.Length; i++)
            {
                string path = filesPathList[i];
                File.Copy(path, $"{FileSystemUtils.GetTempFolder() + gameInformation.CreateSaveName()}");
            }

            return new FileStream(FileSystemUtils.GetTempFolder() + gameInformation.CreateSaveName(), FileMode.Open, FileAccess.Read);
        }

        public void PrepareBackup(GameInformationModel gameInformation)
        {
            if (gameInformation == null) return;

            throw new NotImplementedException();
        }
    }
}