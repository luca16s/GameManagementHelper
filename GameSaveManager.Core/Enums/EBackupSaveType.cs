using System.ComponentModel;

namespace GameSaveManager.Core.Enums
{
    public enum EBackupSaveType
    {
        [Description("bak")]
        BakFile,

        [Description("zip")]
        ZipFile,
    }
}