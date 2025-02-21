using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        public static Vector3 Center(this List<Transform> transforms)
        {
            var bound = new Bounds(transforms[0].position, Vector3.zero);
            for (int i = 1; i < transforms.Count; i++)
            {
                bound.Encapsulate(transforms[i].position);
            }

            return bound.center;
        }
    
        public static List<Transform> SortByDistance(this IEnumerable<Transform> transforms, Vector3 measureFrom)
        {
            return transforms.OrderBy(x => Vector3.Distance(x.position, measureFrom)).ToList();
        }
        public static void SortByDistance(this List<Transform> transforms, Vector3 measureFrom)
        {
            transforms.Sort((a, b) => Vector3.Distance(a.position, measureFrom).CompareTo(Vector3.Distance(b.position, measureFrom)));
        }
        

    }
}