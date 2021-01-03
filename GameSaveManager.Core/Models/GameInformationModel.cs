namespace GameSaveManager.Core.Models
{
    using System;

    using GameSaveManager.Core.Enums;
    using GameSaveManager.Core.Utils;

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
        public string SaveName { get; set; }

        public string BuildSaveName() => BuildSaveName(string.Empty);

        public string BuildSaveName(string saveName)
        {
            string nameToBeUsed = string.IsNullOrWhiteSpace(SaveName)
                ? DefaultSaveName
                : SaveName;

            return !string.IsNullOrWhiteSpace(saveName)
                ? string.Concat(saveName, '.', SaveBackupExtension)
                : string.Concat(nameToBeUsed, '.', SaveBackupExtension);
        }

        public string RestoreSaveName() => string.Concat(DefaultSaveName, $".{DefaultSaveExtension}");

        public void SetSaveBackupExtension(string saveExtension)
        {
            try
            {
                if (Enum.IsDefined(typeof(EBackupSaveType), saveExtension.GetEnumValueFromDescription<EBackupSaveType>()))
                    SaveBackupExtension = saveExtension;
            }
            catch (ArgumentException)
            {
                SaveBackupExtension = string.Empty;
                throw new NotSupportedException(SystemMessages.ErrorSaveExtensionNotSupported);
            }
        }
    }
}