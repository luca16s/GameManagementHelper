using System.ComponentModel;

namespace GameSaveManager.Core.Enums
{
    public enum EGamesSupported
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