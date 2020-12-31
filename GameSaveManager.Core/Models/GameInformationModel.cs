namespace GameSaveManager.Core.Models
{
    public class GameInformationModel
    {
        public string CoverPath { get; set; }
        public string DefaultSaveExtension { get; set; }
        public string DefaultGameSaveFolder { get; set; }
        public string DefaultSaveName { get; set; }
        public string Developer { get; set; }
        public string Name { get; set; }
        public string OnlineSaveFolder { get; set; }
        public string Publisher { get; set; }
        public string SaveBackupExtension { get; private set; }
        public string Title { get; set; }

        public string BuildSaveName() => BuildSaveName(string.Empty);

        public string BuildSaveName(string saveName)
        {
            return !string.IsNullOrWhiteSpace(value: saveName)
                ? string.Concat(saveName, SaveBackupExtension)
                : string.Concat(DefaultSaveName, SaveBackupExtension);
        }

        public string RestoreSaveName() => string.Concat(DefaultSaveName, DefaultSaveExtension);

        public void SetSaveBackupExtension(string saveExtension)
        {
            SaveBackupExtension = !string.IsNullOrWhiteSpace(saveExtension)
                ? saveExtension
                : string.Empty;
        }
    }
}