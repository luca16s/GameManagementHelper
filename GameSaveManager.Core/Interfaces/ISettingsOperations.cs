using GameSaveManager.Core.Models;

namespace GameSaveManager.Core.Interfaces
{
    public interface ISettingsOperations
    {
        SettingsModel LoadSettings();
        string GenereteSettingsJson(SettingsModel settings);
        void SaveSettings(string settings);
    }
}
