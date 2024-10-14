namespace iso.gmh.core.Services;

using System;

using iso.gmh.Core.Enums;
using iso.gmh.Core.Interfaces;
using iso.gmh.Core.Utils;

public class BackupFactory : IFactory<ESaveType, IBackupStrategy>
{
    public IBackupStrategy Create(ESaveType saveType)
    {
        return saveType switch
        {
            ESaveType.BAK => new BakBackupStrategy(),
            ESaveType.ZIP => new ZipBackupStrategy(),
            _ => throw new InvalidOperationException(SystemMessages.ErrorFileNotSupported),
        };
    }
}