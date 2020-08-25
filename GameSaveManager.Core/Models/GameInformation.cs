using System;
using System.IO;

namespace GameSaveManager.Core.Models
{
    public class GameInformation
    {
        private string _SaveName;
        public string SaveName
        {
            get => _SaveName;
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

        public string FolderName { get; set; }

        public string BackupFileExtension { get; set; }

        public DateTime CreationDate { get; set; }

        public string OnlineDriveFolder => $"/{FolderName}/";

        public string ZipTempFolder => Path.GetTempPath() + SaveName + BackupFileExtension;

        private string _GameCoverImagePath;

        public string GameCoverImagePath
        {
            get => _GameCoverImagePath;
            set
            {
                if (value == null) return;
                _GameCoverImagePath = $"../resources/gameCover/{value}.jpg";
            }
        }
    }
}