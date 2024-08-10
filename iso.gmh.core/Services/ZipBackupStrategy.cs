namespace iso.gmh.core.Services;

using System.IO;
using System.IO.Compression;

using iso.gmh.Core.Enums;
using iso.gmh.Core.Interfaces;
using iso.gmh.Core.Models;

using KapiCoreLib.Extensions;

public class ZipBackupStrategy : IBackupStrategy
{
    public string GetFileExtension() => EBackupSaveType.ZipFile.Description();

    public FileStream GenerateBackup(GameInformationModel gameInformation)
    {
        if (gameInformation == null)
            return null;

        string folder = "";
        //FileSystemUtils.FindFolderPath(gameInformation.DefaultGameSaveFolder);

        string saveName = gameInformation.BuildSaveName();

        ZipFile.CreateFromDirectory(folder, Path.Combine(Path.GetTempPath(), saveName));

        return new FileStream(Path.Combine(Path.GetTempPath(), saveName), FileMode.Open, FileAccess.Read);
    }

    public void PrepareBackup(GameInformationModel gameInformation)
    {
        if (gameInformation == null)
            return;

        string folder = "";
        //FileSystemUtils.FindFolderPath(gameInformation.DefaultGameSaveFolder);

        ZipFile.ExtractToDirectory(Path.Combine(Path.GetTempPath(), gameInformation.BuildSaveName()), folder, true);
    }
}