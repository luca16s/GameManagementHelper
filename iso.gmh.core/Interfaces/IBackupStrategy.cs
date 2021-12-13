namespace iso.gmh.Core.Interfaces
{
    using System.IO;

    using iso.gmh.Core.Models;

    public interface IBackupStrategy
    {
        string GetFileExtension();

        FileStream GenerateBackup(GameInformationModel gameInformation);

        void PrepareBackup(GameInformationModel gameInformation);
    }
}