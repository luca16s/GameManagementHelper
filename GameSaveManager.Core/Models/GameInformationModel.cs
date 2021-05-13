namespace GameSaveManager.Core.Models
{
    using System;

    using CoreLibrary.Utils.Extensions;

    using GameSaveManager.Core.Enums;
    using GameSaveManager.Core.Utils;
    using GameSaveManager.Core.Validations;

    public class GameInformationModel
    {
        private readonly GameInformationModelValidatons ValidationRules = new();

        private string _UserDefinedSaveName;
        public string CoverPath { get; set; }
        public string DefaultGameSaveFolder { get; set; }
        public string DefaultSaveExtension { get; set; }
        public string DefaultSaveName { get; set; }
        public string Developer { get; set; }
        public string Name { get; set; }
        public string OnlineSaveFolder { get; set; }
        public string Publisher { get; set; }
        public string SaveBackupExtension { get; private set; }
        public string Title { get; set; }

        public string UserDefinedSaveName
        {
            get => _UserDefinedSaveName;
            set
            {
                _UserDefinedSaveName = ValidationRules.Validate(this).IsValid
                    ? value
                    : DefaultSaveName;
            }
        }

        public string BuildSaveName() => BuildSaveName(string.Empty);

        public string BuildSaveName(string saveName)
        {
            return !string.IsNullOrWhiteSpace(saveName)
                ? string.Concat(saveName, SaveBackupExtension)
                : string.Concat(_UserDefinedSaveName, SaveBackupExtension);
        }

        public string RestoreSaveName() => string.Concat(DefaultSaveName, '.', DefaultSaveExtension);

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