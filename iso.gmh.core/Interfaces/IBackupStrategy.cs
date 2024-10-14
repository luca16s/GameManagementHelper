namespace iso.gmh.Core.Interfaces;

using System.IO;

using iso.gmh.Core.Models;

public interface IBackupStrategy
{
    string GetFileExtension();

    void PrepareBackup(
        GameInformationModel gameInformation
    );

    FileStream GenerateBackup(
        GameInformationModel gameInformation
    );
}