using System.ComponentModel;

namespace GameSaveManager.Core.Enums
{
    public enum EDriveServicesEnum
    {
        [Description("Dropbox")]
        Dropbox,

        [Description("Google Drive")]
        GoogleDrive,

        [Description("OneDrive")]
        OneDrive
    }
}