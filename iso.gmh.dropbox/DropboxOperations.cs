namespace iso.gmh.dropbox;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Dropbox.Api;
using Dropbox.Api.Files;
using Dropbox.Api.Stone;

using iso.gmh.Core.Interfaces;
using iso.gmh.Core.Models;

using KapiCoreLib.Desktop.Utils;

public class DropboxOperations(
    DropboxClient client,
    IBackupStrategy backupStrategy
    ) : ICloudOperations
{
    private readonly DropboxClient Client = client;
    private readonly IBackupStrategy BackupStrategy = backupStrategy;

    private async Task<ListFolderResult> ListFolderContent(string folderPath) => await Client.Files.ListFolderAsync(folderPath);

    private static bool CheckIfFolderExistsInList(string folderName, ListFolderResult itemsList)
    {
        foreach (Metadata item in itemsList.Entries.Where(x => x.IsFolder))
            if (string.Equals(item.Name, folderName?.Trim('/'), StringComparison.InvariantCultureIgnoreCase))
                return true;

        return false;
    }

    public async Task<IEnumerable<(string name, string path)>> GetSavesList(Game gameInformation)
    {
        string folderName = gameInformation.OnlineSaveFolder.TrimEnd('/');

        if (!await CheckFolderExistence(folderName))
            return Enumerable.Empty<(string name, string path)>();

        ListFolderResult fileList = await ListFolderContent(folderName);

        var listaSaves = new List<(string name, string path)>();

        string fileExtension = BackupStrategy.GetFileExtension();

        foreach (var item in fileList.Entries
                                     .Where(entrie => entrie.IsFile
                                                   && entrie.Name.Contains(fileExtension))
                                     .Select(save => new
                                     {
                                         name = save.Name.Replace("." + fileExtension, string.Empty, StringComparison.InvariantCultureIgnoreCase),
                                         path = save.PathLower
                                     }))
            listaSaves.Add((item.name, item.path));

        return listaSaves;
    }

    public async Task<bool> DownloadSaveData(Game gameInformation)
    {
        if (gameInformation == null
            || !await CheckFolderExistence(gameInformation.OnlineSaveFolder))
            return false;

        ListFolderResult fileList = await ListFolderContent(gameInformation.OnlineSaveFolder.TrimEnd('/'));

        Metadata fileFound = fileList.Entries.FirstOrDefault(save => save.IsFile
        && save.Name.Equals(gameInformation.BuildSaveName(), StringComparison.InvariantCultureIgnoreCase));

        if (fileFound is null)
            return false;

        using IDownloadResponse<FileMetadata> result = await Client.Files.DownloadAsync(Path.Combine(gameInformation.OnlineSaveFolder, fileFound.Name));

        using (FileStream stream = File.OpenWrite(Path.Combine(Path.GetTempPath(), fileFound.Name)))
        {
            byte[] dataToWrite = await result.GetContentAsByteArrayAsync();
            stream.Write(dataToWrite, 0, dataToWrite.Length);
        }

        BackupStrategy.PrepareBackup(gameInformation);

        return true;
    }

    public async Task<bool> UploadSaveData(Game gameInformation, bool overwriteSave)
    {
        if (gameInformation == null)
            return false;

        try
        {
            gameInformation.SetSaveBackupExtension(BackupStrategy.GetFileExtension());

            using FileStream fileStream = BackupStrategy.GenerateBackup(gameInformation);

            WriteMode writeMode = overwriteSave ? WriteMode.Overwrite.Instance : WriteMode.Add.Instance;

            FileMetadata response = await Client
                .Files
                .UploadAsync(Path.Combine(gameInformation.OnlineSaveFolder, gameInformation.BuildSaveName()), writeMode, body: fileStream)
                ;

            return string.IsNullOrEmpty(response.ContentHash);
        }
        finally
        {
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            bool fileExists = File.Exists(Path.Combine(Path.GetTempPath(), gameInformation.BuildSaveName()));

            if (fileExists && isWindows)
                _ = FileSystemUtils.DeleteFile(Path.Combine(Path.GetTempPath(), gameInformation.BuildSaveName()));
        }
    }

    public async Task<bool> CheckFolderExistence(string folderName)
    {
        if (string.IsNullOrWhiteSpace(folderName))
            return false;

        ListFolderResult itemsList = await Client.Files.ListFolderAsync("");

        bool hasFolder = CheckIfFolderExistsInList(folderName, itemsList);

        if (itemsList.HasMore)
        {
            itemsList = await Client.Files.ListFolderContinueAsync(folderName);
            hasFolder = CheckIfFolderExistsInList(folderName, itemsList);
        }

        return hasFolder;
    }

    public async Task<bool> CreateFolder(string path)
    {
        CreateFolderResult result = await Client.Files.CreateFolderV2Async(path?.TrimEnd('/'));
        return string.IsNullOrEmpty(result.Metadata.Id);
    }

    public async Task<bool> DeleteSave(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return false;

        try
        {
            DeleteResult result = await Client.Files.DeleteV2Async(path);
        }
        catch (ApiException<DeleteError> ex)
        {
            throw new ApplicationException(ex.Message);
        }

        return true;
    }
}