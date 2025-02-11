namespace Runtime.Core.PoolingSystem
{
    public interface IPoolable
    {
        void OnPool();
        void OnRetrieved();

    }
}