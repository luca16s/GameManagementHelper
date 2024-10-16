﻿namespace iso.gmh.desktop.ViewModel;

using iso.gmh.desktop;

using iso.gmh.Core.Enums;

public partial class SettingsViewModel : BaseViewModel
{
    public SettingsViewModel()
    {
        App.BackupType = BackupSaveType;
        App.DriveService = DriveServiceSelected;
    }

    private EDrive _DriveServiceSelected;

    public EDrive DriveServiceSelected
    {
        get => _DriveServiceSelected;
        set
        {
            if (_DriveServiceSelected == value)
                return;

            _DriveServiceSelected =
                App.DriveService = value;

            OnPropertyChanged();
        }
    }

    private ESaveType _backupSaveType;

    public ESaveType BackupSaveType
    {
        get => _backupSaveType;
        set
        {
            if (_backupSaveType == value)
                return;

            _backupSaveType =
                App.BackupType = value;

            OnPropertyChanged();
        }
    }

    private string _Name;

    public string Name
    {
        get => _Name;
        set
        {
            if (_Name == value)
                return;

            _Name = value;

            OnPropertyChanged();
        }
    }

    private string _Email;

    public string Email
    {
        get => _Email;
        set
        {
            if (_Email == value)
                return;

            _Email = value;

            OnPropertyChanged();
        }
    }
}