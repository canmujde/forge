namespace PoolingSystem
{
    public interface IPoolable
    {
        void OnPool();
        void OnRetrieved();

    }
}