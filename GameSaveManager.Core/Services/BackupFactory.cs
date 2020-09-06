using GameSaveManager.Core.Enums;
using GameSaveManager.Core.Interfaces;

using System;
using System.Resources;

namespace GameSaveManager.Core.Services
{
    public class BackupFactory : IFactory<IBackupStrategy>
    {
        public IBackupStrategy Create(BackupSaveType saveType)
        {
            return saveType switch
            {
                BackupSaveType.BakFile => new BakBackupStrategy(),
                BackupSaveType.ZipFile => new ZipBackupStrategy(),
                _ => throw new InvalidOperationException(Properties.Resources.ErrorSaveNotSupported),
            };
        }
    }
}