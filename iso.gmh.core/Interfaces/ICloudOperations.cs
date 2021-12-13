﻿namespace iso.gmh.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using iso.gmh.Core.Models;

    public interface ICloudOperations
    {
        Task<bool> CreateFolder(string path);

        Task<bool> CheckFolderExistence(string folderName);

        Task<bool> DownloadSaveData(GameInformationModel gameInformation);

        Task<IEnumerable<(string name, string path)>> GetSavesList(GameInformationModel gameInformation);

        Task<bool> DeleteSave(string path);

        Task<bool> UploadSaveData(GameInformationModel gameInformation, bool overwriteSave);
    }
}