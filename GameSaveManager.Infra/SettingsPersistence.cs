using GameSaveManager.Core.Interfaces;
using GameSaveManager.Core.Models;

using System;
using System.IO;
using System.Text.Json;

namespace GameSaveManager.Infra
{
    public class SettingsPersistence : ISettingsOperations
    {
        public string GenereteSettingsJson(SettingsModel settings)
        {
            return JsonSerializer.Serialize(settings);
        }

        public SettingsModel LoadSettings()
        {
            return JsonSerializer.Deserialize<SettingsModel>(File.ReadAllText(Environment.CurrentDirectory));
        }

        public void SaveSettings(string settings)
        {
            File.WriteAllText($@"{Environment.CurrentDirectory}\settings.json", settings);
        }
    }
}
