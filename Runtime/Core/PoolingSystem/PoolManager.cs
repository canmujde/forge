using System.Collections.Generic;
using Config;
using UnityEngine;

namespace Core.PoolingSystem
{
    public static class PoolManager
    {
        private static PoolableScriptableObject[] _poolableObjects;
        private static Dictionary<string, ObjectPool> _pools = new Dictionary<string, ObjectPool>();
        private static Transform _poolParent;

        static PoolManager()
        {
            _poolableObjects = Resources.LoadAll<PoolableScriptableObject>("PoolableObjects");
            _poolParent = new GameObject("_Pool").transform;
            foreach (var poolableData in _poolableObjects)
            {
                if (poolableData.prefab == null) continue;
                CreatePool(poolableData.prefab.name, poolableData.prefab, poolableData.initialSize, _poolParent, poolableData.expandable);
            }
            Debug.Log("Pool Manager initialized");
        }

        private static void CreatePool(string key, GameObject prefab, int initialSize, Transform parent = null, bool expandable = true)
        {
            if (!_pools.ContainsKey(key))
            {
                _pools[key] = new ObjectPool(prefab, initialSize, parent, expandable);
            }
        }

        public static void RemoveObject(string key)
        {
            if (_pools.TryGetValue(key, out var pool))
            {
                pool.FindNullsAndRemove();
            }
        }

        public static GameObject GetObject(string key)
        {
            if (_pools.TryGetValue(key, out var pool))
            {
                return pool.Get();
            }
            return null;
        }

        public static void ReturnObject(string key, GameObject obj)
        {
            if (_pools.TryGetValue(key, out var pool))
            {
                pool.ReturnToPool(obj);
            }
            else
            {
                Object.Destroy(obj);
            }
        }
    }
}