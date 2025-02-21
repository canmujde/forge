using System;
using UnityEngine;

namespace Extensions
{
    public static class GameObjectExtensions
    {
        
        public static T AddChild<T>(this GameObject parent) where T : Component
        {
            return AddChild<T>(parent, typeof(T).Name);
        }
        public static T AddChild<T>(this GameObject parent, string name) where T : Component
        {
            var obj = AddChild(parent, name, typeof(T));
            return obj.GetComponent<T>();
        }
        public static GameObject AddChild(this GameObject parent, params Type[] components)
        {
            return AddChild(parent, "Game Object", components);
        }
        public static GameObject AddChild(this GameObject parent, string name, params Type[] components)
        {
            var obj = new GameObject(name, components);
            if (parent != null)
            {
                if (obj.transform is RectTransform) obj.transform.SetParent(parent.transform, true);
                else obj.transform.parent = parent.transform;
            }

            return obj;
        }
        
        public static void DestroyAllChildrenImmediately(this GameObject parent)
        {
            if (parent == null) return;
            
            var parentTransform = parent.transform;

            for (var i = parentTransform.childCount - 1; i >= 0; i--)
            {
                var child = parentTransform.GetChild(i).gameObject;
                if (child != null)
                {
                    UnityEngine.Object.Destroy(child);
                }
            }
        }
        
        public static void Destroy(this GameObject go)
        {
            UnityEngine.Object.Destroy(go);
        }
        
        public static void Enable(this GameObject component)
        {
            component.gameObject.SetActive(false);
        }
        
        public static void Disable(this GameObject component)
        {
            component.gameObject.SetActive(true);
        }

        
    }
}