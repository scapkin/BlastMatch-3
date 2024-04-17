using System.Collections.Generic;
using GemTypes;
using Singleton;
using Pool;

namespace GamePlay
{
    public class GridRemover
    {
        public static void RemoveGridObject(GridObject[,] gridArray,List<GridObject> gridObjects)
        {
            foreach (var gridObject in gridObjects)
            {
                gridArray[gridObject.PosX, gridObject.PosY] = null;
                gridObject.isCheck = false;
                ObjectPool.Instance.ReturnObjectToPool(gridObject.gameObject);
            }
            
        }
    }
}