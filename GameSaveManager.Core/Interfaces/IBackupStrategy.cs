namespace GameSaveManager.Core.Interfaces
{
    using System.IO;

    using GameSaveManager.Core.Models;

    public interface IBackupStrategy
    {
        string GetFileExtension();

        FileStream GenerateBackup(GameInformationModel gameInformation);

        void PrepareBackup(GameInformationModel gameInformation);
    }
}