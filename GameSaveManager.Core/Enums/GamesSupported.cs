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
        DarkSouls2,
        [Description("Dark Souls III")]
        DarkSouls3,
    }
}
