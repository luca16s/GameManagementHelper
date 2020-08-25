using GameSaveManager.Core.Enums;

namespace GameSaveManager.Core.Interfaces
{
    public interface IFactory<out T>
    {
        T Create(BackupSaveType saveType);
    }
}