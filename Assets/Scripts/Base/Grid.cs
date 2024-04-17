using Pool;
using UnityEngine;

namespace Base
{
    public abstract class Grid: MonoBehaviour
    {
        protected int x;
        protected int y;
        protected GameObject Gem;
        protected ObjectPoolItem.GemType _type;
        
        public int PosX
        {
            get { return x; }
            set { x = value; }
        }

        public int PosY
        {
            get { return y; }
            set { y = value; }
        }

        public ObjectPoolItem.GemType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public GameObject GemObject
        {
            get { return Gem; }
            set { Gem = value; }
        }
    }
}