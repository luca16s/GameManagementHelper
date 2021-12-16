namespace iso.gmh.services.DriveServices
{
    using System;

    using iso.gmh.Core.Enums;
    using iso.gmh.Core.Interfaces;
    using iso.gmh.Core.Utils;
    using iso.gmh.dropboxService;
    using iso.gmh.oneDriveService;

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