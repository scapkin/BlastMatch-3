using UnityEngine;

namespace Pooling
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        public GameObject prefab;
        public GemType type;
        public int initialPoolSize;

        public enum GemType
        {
            Red = 0,
            Green = 1,
            Yellow = 2,
            Blue = 3,
        }
    }
}