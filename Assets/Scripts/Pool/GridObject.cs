using Pooling;
using UnityEngine;

namespace Pool
{
    public class GridObject
    {
        public int x;
        public int y;
        public GameObject Gem;
        private ObjectPoolItem.GemType _type;

        public GridObject(int i, int j, ObjectPoolItem.GemType rnd,GameObject gemObj)
        {
            this.x = i;
            this.y = j;
            this._type = rnd;
            this.Gem = gemObj;
        }
    
        public void SetGem(int i, int j) {
            this.x = i;
            this.y = j;
        }
        
        public ObjectPoolItem.GemType GetGemType() => _type;
    }
}