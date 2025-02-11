using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Core.PoolingSystem
{
    public class ObjectPool
    {
        private Queue<GameObject> _pool = new Queue<GameObject>();
        private GameObject _prefab;
        private Transform _parent;
        private bool _expandable;

        public ObjectPool(GameObject prefab, int initialSize, Transform parent = null, bool expandable = true)
        {
            var poolObject = new GameObject($"_Pool_{prefab.name}")
            {
                transform =
                {
                    parent = parent
                }
            };

            _prefab = prefab;
            _parent = poolObject.transform;
            _expandable = expandable;

            for (int i = 0; i < initialSize; i++)
            {
                AddObjectToPool();
            }
        }

        private void AddObjectToPool()
        {
            var obj = Object.Instantiate(_prefab, _parent.transform);
            obj.name = _prefab.name;
            obj.AddComponent<Poolable>();
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
      

        public GameObject Get()
        {
            if (_pool.Count > 0)
            {
                var obj = _pool.Dequeue();
                var poolable = obj.GetComponent<IPoolable>();
                poolable?.OnRetrieved();
                obj.SetActive(true);
                return obj;
            }

            if (!_expandable) return null;

            AddObjectToPool();
            var instance = _pool.Dequeue();
            var instancePoolable = instance.GetComponent<IPoolable>();
            instancePoolable?.OnRetrieved();
            instance.SetActive(true);
            

            return instance;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            _pool.Enqueue(obj);
            var poolable = obj.GetComponent<IPoolable>();
            poolable?.OnPool();
        }
        
        
        public void FindNullsAndRemove()
        {
            var originalCount = _pool.Count;
            for (int i = 0; i < originalCount; i++)
            {
                var item = _pool.Dequeue();
                if (item != null)
                {
                    _pool.Enqueue(item);
                }
            }
        }
    }
}