using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "Poolable", menuName = "ScriptableObjects/Poolable")]
    public class PoolableScriptableObject : ScriptableObject
    {
        public GameObject prefab;
        public int initialSize;
        public bool expandable;
    }
}