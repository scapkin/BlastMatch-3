using Pool;
using UnityEngine;

namespace Base
{
    public abstract class Grid: MonoBehaviour
    {
        public bool isCheck = false;
        public int x;
        public int y;
        public GameObject Gem;
        public ObjectPoolItem.GemType _type;
        
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