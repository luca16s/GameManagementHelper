using System.ComponentModel;

namespace GameSaveManager.Core.Enums
{
    public enum DriveServicesEnum
    {
        [Description("Dropbox")]
        Dropbox,
        [Description("Google Drive")]
        GoogleDrive,
        [Description("OneDrive")]
        OneDrive
    }
}
