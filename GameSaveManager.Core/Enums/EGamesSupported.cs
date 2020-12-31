namespace GameSaveManager.Core.Enums
{
    using System.ComponentModel;

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