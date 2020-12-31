namespace GameSaveManager.Core.Interfaces
{
    using GameSaveManager.Core.Enums;

    public interface IFactory<out T>
    {
        T Create(EBackupSaveType saveType);
    }
}