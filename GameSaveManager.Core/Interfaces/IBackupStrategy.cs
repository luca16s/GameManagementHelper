namespace GameSaveManager.Core.Interfaces
{
    using System.IO;

    using GameSaveManager.Core.Models;

    public interface IBackupStrategy
    {
        FileStream GenerateBackup(GameInformationModel gameInformation);

        string GetFileExtension();

        void PrepareBackup(GameInformationModel gameInformation);
    }
}