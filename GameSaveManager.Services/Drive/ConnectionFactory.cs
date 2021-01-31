namespace GameSaveManager.Services.Drive
{
    using System;

    using GameSaveManager.Core.Enums;
    using GameSaveManager.Core.Interfaces;
    using GameSaveManager.Core.Utils;
    using GameSaveManager.DropboxApi;
    using GameSaveManager.OneDriveApi;

    public class ConnectionFactory : IFactory<EDriveServices, IConnection>
    {
        public IConnection Create(EDriveServices type)
        {
            return type switch
            {
                EDriveServices.Dropbox => new DropboxConnection(),
                EDriveServices.OneDrive => new OneDriveConnection(),
                _ => throw new InvalidOperationException(SystemMessages.ErrorConnectionNotSupported),
            };
        }
    }
}
