using System.IO;

namespace GameSaveManager.Core.Models
{
    public class GameInformationModel
    {
        public string CoverPath { get; set; }
        public string DefaultFileExtension { get; set; }
        public string DefaultGameSaveFolder { get; set; }
        public string DefaultSaveName { get; set; }
        public string Developer { get; set; }
        public string GameSaveExtension { get; set; }
        public string Name { get; set; }
        public string OnlineSaveFolder { get; set; }
        public string Publisher { get; set; }
        public string SaveBackupExtension { get; set; }
        public string SaveExtension { get; set; }
        public string Title { get; set; }

        public string CreateSaveName(string saveName = null) => string.IsNullOrWhiteSpace(saveName)
            ? DefaultSaveName
            : saveName + SaveBackupExtension;
    }
}