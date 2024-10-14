namespace iso.gmh.Core.Interfaces;

public interface IFactory<T, out TReturn>
{
    TReturn Create(
        T type
    );
}