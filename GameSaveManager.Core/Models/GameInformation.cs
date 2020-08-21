using System;
using System.IO;

namespace GameSaveManager.Core.Models
{
    public class GameInformation
    {
        public string SaveName { get; set; }

        public string FilePath { get; set; }

        public string GameName { get; set; }

        public string FolderName { get; set; }

        public string FileExtension { get; set; }

        public DateTime CreationDate { get; set; }

        public string OnlineDriveFolder => $"/{FolderName}/";

        public string ZipTempFolder => Path.GetTempPath() + SaveName;

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