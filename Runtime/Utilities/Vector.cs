using UnityEngine;

namespace Utilities
{
    public static class Vector
    {
        public static Vector3 RandomDirection()
        {
            return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        }
        public static Vector3 Center(Vector3 a, Vector3 b)
        {
            return (a + b) / 2f;
        }
        
        
    }
}