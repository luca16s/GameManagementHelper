using System.ComponentModel;

namespace GameSaveManager.Core.Enums
{
    public enum BackupSaveType
    {
        [Description(".bak")]
        BakFile,

        [Description(".zip")]
        ZipFile,
    }
}
