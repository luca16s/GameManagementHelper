namespace GameSaveManager.Core.Interfaces
{
    using GameSaveManager.Core.Models;

    using System.IO;

    public interface IBackupStrategy
    {
        string GetFileExtension();

        FileStream GenerateBackup(GameInformationModel gameInformation);

        void PrepareBackup(GameInformationModel gameInformation);
    }
}