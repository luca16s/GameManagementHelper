namespace GameSaveManager.Core.Interfaces
{
    public interface IFactory<T, out TReturn>
    {
        TReturn Create(T saveType);
    }
}