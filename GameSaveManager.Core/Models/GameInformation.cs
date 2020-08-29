using GameSaveManager.Core.Utils;

using System;
using System.IO;

namespace GameSaveManager.Core.Models
{
    public class GameInformation
    {
        private string _SaveName;

        public string SaveName
        {
            get => _SaveName + BackupFileExtension;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value == _SaveName)
                    return;

                _SaveName = $"{value}-{DateTime.Now:MM-dd-yyyy}";
            }
        }

        public string FilePath { get; set; }

        public string GameName { get; set; }

        public string GameSaveExtension { get; set; }

        public string GameSaveDefaultFolder => $"{FileSystemUtils.GetGameFolderLocationAppData(FolderName)}\\0110000106640ba7\\";

        public string FolderName { get; set; }

        public string BackupFileExtension { get; set; }

        public DateTime CreationDate { get; set; }

        public string OnlineDriveFolder => $"/{FolderName}/";

        public string ZipTempFolder => Path.GetTempPath() + SaveName;

        private string _GameCoverImagePath;

        public string GameCoverImagePath
        {
            get => _GameCoverImagePath;
            set
            {
                if (value == "") return;
                _GameCoverImagePath = $"../resources/gameCover/{value}.jpg";
            }
        }
    }
}