using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using GemTypes;
using Pool;

namespace GamePlay
{
    public class GridDropManager
    {
        public static void Drop(Cell[,] gridArray ,List<Cell> dropList)
        {
            Dictionary<int, int> minYValues = new Dictionary<int, int>();
            int count = 0;

            // Find the lowest y value for each x value
            foreach (Cell obj in dropList)
            {
                int x = obj.GridObject.PosX;
                int y = obj.GridObject.PosY;

                if (!minYValues.ContainsKey(x) || y < minYValues[x])
                {
                    minYValues[x] = y;
                }
            }

            foreach (KeyValuePair<int, int> kvp in minYValues)
            {
                //x: kvp.Key -- y: kvp.Value
                for (int i = kvp.Value; i < gridArray.GetLength(0); i++)
                {
                    if (gridArray[kvp.Key, i] != null)
                    {
                        GridManager.SetGridAction?.Invoke(gridArray[kvp.Key, i], kvp.Key, i - count);
                        gridArray[kvp.Key, i] = null;
                    }
                    else
                    {
                        count++;
                    }
                }
                for (int i = gridArray.GetLength(0) - count; i < gridArray.GetLength(0); i++)
                {
                    GridSpawner.CreateNewGridAction?.Invoke(kvp.Key, i, ObjectPoolItem.GemType.Normal, Cell.CellTypes.Normal);
                }
                count = 0;
            }
        }
    }
}