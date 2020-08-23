using GameSaveManager.Core.Models;

using System.IO;

namespace GameSaveManager.Core.Interfaces
{
    public interface IBackupStrategy
    {
        FileStream GenerateBackup(GameInformation gameInformation);

        void PrepareBackup(GameInformation gameInformation);
    }
}