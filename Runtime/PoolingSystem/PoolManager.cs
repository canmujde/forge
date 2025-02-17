using System.Collections.Generic;
using Config;
using UnityEngine;

namespace Core.PoolingSystem
{
    public static class PoolManager
    {
        private static readonly Dictionary<string, ObjectPool> Pools = new Dictionary<string, ObjectPool>();

        static PoolManager()
        {
            var poolableObjects = Resources.LoadAll<PoolableScriptableObject>("PoolableObjects");
            var poolParent = new GameObject("_Pool").transform;
            poolParent.hideFlags = HideFlags.HideInHierarchy;
            foreach (var poolableData in poolableObjects)
            {
                if (poolableData.prefab == null) continue;
                CreatePool(poolableData.prefab.name, poolableData.prefab, poolableData.initialSize, poolParent, poolableData.expandable);
            }
            Debug.Log("Pool Manager initialized");
        }

        private static void CreatePool(string key, GameObject prefab, int initialSize, Transform parent = null, bool expandable = true)
        {
            if (!Pools.ContainsKey(key))
            {
                Pools[key] = new ObjectPool(prefab, initialSize, parent, expandable);
            }
        }

        public static void RemoveObject(string key)
        {
            if (Pools.TryGetValue(key, out var pool))
            {
                pool.FindNullsAndRemove();
            }
        }

        public static GameObject GetObject(string key)
        {
            return Pools.TryGetValue(key, out var pool) ? pool.Get() : null;
        }
        public static GameObject GetObject(string key, Transform setParent)
        {
            if (Pools.TryGetValue(key, out var pool))
            {
                var get = pool.Get();
                get.transform.SetParent(setParent);
                return get;
            }
            return null;
        }

        public static void ReturnObject(string key, GameObject obj)
        {
            if (Pools.TryGetValue(key, out var pool))
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