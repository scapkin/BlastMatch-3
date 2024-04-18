using System.Collections.Generic;
using DG.Tweening;
using GemTypes;
using Singleton;
using Pool;

namespace GamePlay
{
    public class GridRemover
    {
        public static void RemoveGridObjects(Cell[,] gridArray,List<Cell> gridObjects,int x,int y)
        {
            foreach (var gridObject in gridObjects)
            {
                gridArray[gridObject.GridObject.PosX, gridObject.GridObject.PosY] = null;
                gridObject.GridObject.isCheck = false;
                ObjectPool.Instance.ReturnObjectToPool(gridObject.GridObject.gameObject);
            }
        }
        public static void RemoveSingleObject(Cell[,] gridArray,int x,int y)
        {
            gridArray[x,y].GridObject.isCheck = false;
            ObjectPool.Instance.ReturnObjectToPool(gridArray[x,y].GridObject.gameObject);
            gridArray[x,y] = null;
            
        }
    }
    
}