namespace iso.gmh.core.Services;

using System;

using iso.gmh.Core.Enums;
using iso.gmh.Core.Interfaces;
using iso.gmh.Core.Utils;

public class BackupFactory : IFactory<EBackupSaveType, IBackupStrategy>
{
    public IBackupStrategy Create(EBackupSaveType saveType)
    {
        return saveType switch
        {
            EBackupSaveType.BakFile => new BakBackupStrategy(),
            EBackupSaveType.ZipFile => new ZipBackupStrategy(),
            _ => throw new InvalidOperationException(SystemMessages.ErrorFileNotSupported),
        };
    }
}