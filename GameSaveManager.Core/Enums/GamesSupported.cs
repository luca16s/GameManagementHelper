using System.ComponentModel;

namespace GameSaveManager.Core.Enums
{
    public enum GamesSupported
    {
        [Description("Nenhum")]
        None,
        [Description("Dark Souls")]
        DarkSouls,
        [Description("Dark Souls II")]
        DarkSoulsII,
        [Description("Dark Souls III")]
        DarkSoulsIII,
    }
}
