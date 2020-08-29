namespace GameSaveManager.Core.Models
{
    public class GameInformationModel
    {
        public string Title { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string CoverName { get; set; }
        public string DefaultSaveName { get; set; }
        public string DefaultGameSaveFolder { get; set; }
    }
}