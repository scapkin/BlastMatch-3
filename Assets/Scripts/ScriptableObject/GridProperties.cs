using System;
using System.Collections.Generic;
using Pool;
using UnityEngine;

namespace ScriptableObject
{
    [CreateAssetMenu(fileName = "GridProperties", menuName = "GridProperties", order = 0)]
    public class GridProperties : UnityEngine.ScriptableObject
    {
        public int BombMinRequirement = 7;
        public int HorizontalMinRequirement = 5;
        public int VerticalMinRequirement = 3;
        public int GridSize = 8;
        public List<Gem> Gems;
        public int MoveCount = 30;
        public GameObject GridBackgroundObject;
        public float Space = 0.65f;
        [Serializable]
        public class Gem
        {
            public ObjectPoolItem.GemType Type;
            public int Goal;
        }
    }
}