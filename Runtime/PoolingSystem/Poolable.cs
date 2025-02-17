using UnityEngine;

namespace Core.PoolingSystem
{
    public class Poolable : MonoBehaviour, IPoolable
    {
        private string _id;
        public string Id => _id;

        public string idd;

        private void Update()
        {
            idd = _id;
        }

        private void Awake()
        {
            _id = gameObject.name;
        }
        public void OnPool()
        {
            
        }
        public void OnRetrieved()
        {
            
        }
        public void OnDestroy()
        {
            PoolManager.RemoveObject(_id);
        }
    }
}