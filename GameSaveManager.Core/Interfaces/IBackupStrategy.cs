using GameSaveManager.Core.Models;

using System.IO;

namespace GameSaveManager.Core.Interfaces
{
    public interface IBackupStrategy
    {
        string GetFileExtension();

        FileStream GenerateBackup(GameInformationModel gameInformation);

        void PrepareBackup(GameInformationModel gameInformation);
    }
}