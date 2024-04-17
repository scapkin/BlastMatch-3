using System.Collections.Generic;
using DG.Tweening;
using GemTypes;
using Singleton;
using Pool;

namespace GamePlay
{
    public class GridRemover
    {
        public static void RemoveGridObject(Cell[,] gridArray,List<Cell> gridObjects,int x,int y)
        {
            if (gridObjects.Count>5)
            {
                gridObjects.Remove(gridArray[x,y]);
                gridArray[x,y].GridObject.isCheck = false;
                ObjectPool.Instance.ReturnObjectToPool(gridArray[x,y].GridObject.gameObject);
                gridArray[x,y] = null;
                
                GridSpawner.CreateNewGridAction?.Invoke(x,y, ObjectPoolItem.GemType.Bomb);
                
            }
            foreach (var gridObject in gridObjects)
            {
                gridArray[gridObject.GridObject.PosX, gridObject.GridObject.PosY] = null;
                gridObject.GridObject.isCheck = false;
                ObjectPool.Instance.ReturnObjectToPool(gridObject.GridObject.gameObject);
            }
            

            GridDropManager.Drop(gridArray,gridObjects);
        }
    }
}