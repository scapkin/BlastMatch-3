using Base;
using GemTypes;
using UnityEngine;
using Grid = Base.Grid;

namespace Pool
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        public GameObject prefab;
        public Grid GridObject;
        public GemType type;
        public int initialPoolSize;

        public enum GemType
        {
            Blue = 0,
            Green = 1,
            Orange = 2,
            Purple = 3,
            Red = 4,
            Yellow = 5,
            Bomb = 6,
            Horizontal = 7,
            Vertical = 8,
            Background = 9
        }
    }
}