namespace iso.gmh.Core.Models;

using System;

using iso.gmh.Core.Enums;
using iso.gmh.Core.Utils;
using iso.gmh.Core.Validations;

using KapiCoreLib.Extensions;

public class GameInformationModel
{
    private readonly GameInformationModelValidatons ValidationRules = new();

    public string Name { get; set; }
    public string Title { get; set; }
    public string CoverPath { get; set; }
    public string Developer { get; set; }
    public string Publisher { get; set; }
    public string DefaultSaveName { get; set; }
    public string OnlineSaveFolder { get; set; }

    private string _UserDefinedSaveName;

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

    public string SaveBackupExtension { get; private set; }
    public string DefaultSaveExtension { get; set; }
    public string DefaultGameSaveFolder { get; set; }

    public string RestoreSaveName() => string.Concat(DefaultSaveName, '.', DefaultSaveExtension);

    public string BuildSaveName() => BuildSaveName(string.Empty);

    public string BuildSaveName(string saveName)
    {
        return !string.IsNullOrWhiteSpace(saveName)
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