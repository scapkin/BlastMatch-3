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
            foreach (var gridObject in gridObjects)
            {
                gridArray[gridObject.GridObject.PosX, gridObject.GridObject.PosY] = null;
                gridObject.GridObject.isCheck = false;
                ObjectPool.Instance.ReturnObjectToPool(gridObject.GridObject.gameObject);
            }
            if (gridObjects.Count>5)
            {
                GridSpawner.CreateNewGridAction?.Invoke(x,y, ObjectPoolItem.GemType.Bomb);
            }

            GridDropManager.Drop(gridArray,gridObjects);
        }
    }
}