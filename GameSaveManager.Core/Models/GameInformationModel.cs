namespace GameSaveManager.Core.Models
{
    using System;

    using Flunt.Notifications;
    using Flunt.Validations;

    using GameSaveManager.Core.Enums;
    using GameSaveManager.Core.Utils;

    public class GameInformationModel : Notifiable
    {
        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                AddNotifications(new Contract()
                    .IsNotNullOrWhiteSpace(value, nameof(value), SystemMessages.SaveNameIsNullMessage));

                _Name = Valid ? value : string.Empty;
            }
        }

        private string _Title;
        public string Title
        {
            get => _Title;
            set
            {
                AddNotifications(new Contract()
                    .IsNotNullOrWhiteSpace(value, nameof(value), SystemMessages.SaveNameIsNullMessage));

                _Title = Valid ? value : string.Empty;
            }
        }

        private string _CoverPath;
        public string CoverPath
        {
            get => _CoverPath;
            set
            {
                AddNotifications(new Contract()
                    .IsNotNullOrWhiteSpace(value, nameof(value), SystemMessages.SaveNameIsNullMessage));

                _CoverPath = Valid ? value : string.Empty;
            }
        }

        private string _Developer;
        public string Developer
        {
            get => _Developer;
            set
            {
                AddNotifications(new Contract()
                    .IsNotNullOrWhiteSpace(value, nameof(value), SystemMessages.SaveNameIsNullMessage));

                _Developer = Valid ? value : string.Empty;
            }
        }

        private string _Publisher;
        public string Publisher
        {
            get => _Publisher;
            set
            {
                AddNotifications(new Contract()
                    .IsNotNullOrWhiteSpace(value, nameof(value), SystemMessages.SaveNameIsNullMessage));

                _Publisher = Valid ? value : string.Empty;
            }
        }

        private string _DefaultSaveName;
        public string DefaultSaveName
        {
            get => _DefaultSaveName;
            set
            {
                AddNotifications(new Contract()
                    .IsNotNullOrWhiteSpace(value, nameof(value), SystemMessages.SaveNameIsNullMessage));

                _DefaultSaveName = Valid ? value : string.Empty;
            }
        }

        private string _OnlineSaveFolder;
        public string OnlineSaveFolder
        {
            get => _OnlineSaveFolder;
            set
            {
                AddNotifications(new Contract()
                    .IsNotNullOrWhiteSpace(value, nameof(value), SystemMessages.SaveNameIsNullMessage));

                _OnlineSaveFolder = Valid ? value : string.Empty;
            }
        }

        private string _UserDefinedSaveName;
        public string UserDefinedSaveName
        {
            get => _UserDefinedSaveName;
            set
            {
                Clear();

                AddNotifications(new Contract()
                .HasMinLen(value, 5, nameof(value), SystemMessages.SaveNameMinLengthMessage)
                .HasMaxLen(value, 150, nameof(value), SystemMessages.SaveNameMaxLenghtMessage)
                .IsNotNullOrWhiteSpace(value, nameof(value), SystemMessages.SaveNameIsNullMessage));

                _UserDefinedSaveName = Valid
                    ? value
                    : DefaultSaveName;
            }
        }

        public string SaveBackupExtension { get; private set; }
        public string DefaultSaveExtension { get; set; }
        public string DefaultGameSaveFolder { get; set; }

        public string RestoreSaveName() => string.Concat(DefaultSaveName, '.', DefaultSaveExtension);

        public string BuildSaveName() => BuildSaveName(string.Empty);

        public string BuildSaveName(string saveName)
        {
            Clear();

            AddNotifications(new Contract()
                .HasMinLen(saveName, 5, nameof(saveName), SystemMessages.SaveNameMinLengthMessage)
                .HasMaxLen(saveName, 150, nameof(saveName), SystemMessages.SaveNameMaxLenghtMessage)
                .IsNotNullOrWhiteSpace(saveName, nameof(saveName), SystemMessages.SaveNameIsNullMessage));

            return Valid
                ? string.Concat(saveName, SaveBackupExtension)
                : string.Concat(_UserDefinedSaveName, SaveBackupExtension);
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