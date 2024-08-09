namespace iso.gmh.services.DriveServices;

using System;

using iso.gmh.Core.Enums;
using iso.gmh.Core.Interfaces;
using iso.gmh.dropboxService;
using iso.gmh.oneDriveService;

public class OperationFactory : IFactory<EDriveServices, ICloudOperations>
{
    private readonly IBackupStrategy BackupStrategy;
    private readonly dynamic Client;

    public OperationFactory(IBackupStrategy backupStrategy, dynamic client)
    {
        BackupStrategy = backupStrategy;
        Client = client;
    }

    public ICloudOperations Create(EDriveServices type)
    {
        return type switch
        {
            EDriveServices.Dropbox => new DropboxOperations(BackupStrategy, Client),
            EDriveServices.OneDrive => new OneDriveOperations(BackupStrategy, Client),
            _ => throw new NotImplementedException(),
        };
    }
}