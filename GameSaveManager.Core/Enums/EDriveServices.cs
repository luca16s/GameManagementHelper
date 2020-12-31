namespace GameSaveManager.Core.Enums
{
    using System.ComponentModel;

    public enum EDriveServices
    {
        [Description("Dropbox")]
        Dropbox = 0,

        [Description("Google Drive")]
        GoogleDrive = 1,

        [Description("OneDrive")]
        OneDrive = 2
    }
}