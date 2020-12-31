namespace GameSaveManager.Core.Enums
{
    using System.ComponentModel;

    public enum EBackupSaveType
    {
        [Description("bak")]
        BakFile,

        [Description("zip")]
        ZipFile,
    }
}