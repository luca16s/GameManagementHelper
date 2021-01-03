namespace GameSaveManager.Core.Models
{
    using System;

    using GameSaveManager.Core.Enums;
    using GameSaveManager.Core.Utils;

    public class GameInformationModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string CoverPath { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string DefaultSaveName { get; set; }
        public string OnlineSaveFolder { get; set; }
        public string UserDefinedSaveName { get; set; }
        public string SaveBackupExtension { get; private set; }
        public string DefaultSaveExtension { get; set; }
        public string DefaultGameSaveFolder { get; set; }

        public string RestoreSaveName() => string.Concat(DefaultSaveName, '.', DefaultSaveExtension);

        public string BuildSaveName() => BuildSaveName(string.Empty);

        public string BuildSaveName(string saveName)
        {
            string nameToBeUsed = string.IsNullOrWhiteSpace(UserDefinedSaveName)
                ? DefaultSaveName
                : UserDefinedSaveName;

            return string.IsNullOrWhiteSpace(saveName)
                ? string.Concat(nameToBeUsed, SaveBackupExtension)
                : string.Concat(saveName, SaveBackupExtension);
        }

        public void SetSaveBackupExtension(string saveExtension)
        {
            try
            {
                if (Enum.IsDefined(typeof(EBackupSaveType), saveExtension.GetEnumValueFromDescription<EBackupSaveType>()))
                    SaveBackupExtension = string.Concat('.', saveExtension);
            }
            catch (ArgumentException)
            {
                throw new NotSupportedException(SystemMessages.ErrorSaveExtensionNotSupported);
            }
        }
    }
}