using UnityEngine;

namespace Core
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance => _instance ?? (_instance = FindObjectOfType<T>());
    }
}
