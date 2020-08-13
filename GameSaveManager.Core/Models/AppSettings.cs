using GameSaveManager.Core.Utils;

namespace GameSaveManager.Core.Models
{
    public class AppSettings
    {
        public static string LoopbackHost => @"http://127.0.0.1:52475/";
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
}
