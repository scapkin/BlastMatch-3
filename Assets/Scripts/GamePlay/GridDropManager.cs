using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using GemTypes;

namespace GamePlay
{
    public class GridDropManager
    {
        public static void Drop(GridObject[,] _gridArray ,List<GridObject> dropList)
        {
            Dictionary<int, int> minYValues = new Dictionary<int, int>();
            int count = 0;

            // Find the lowest y value for each x value
            foreach (GridObject obj in dropList)
            {
                int x = obj.PosX;
                int y = obj.PosY;

                if (!minYValues.ContainsKey(x) || y < minYValues[x])
                {
                    minYValues[x] = y;
                }
            }

            foreach (KeyValuePair<int, int> kvp in minYValues)
            {
                //x: kvp.Key -- y: kvp.Value
                for (int i = kvp.Value; i < _gridArray.GetLength(0); i++)
                {
                    if (_gridArray[kvp.Key, i] != null)
                    {
                        GridManager.SetGridAction?.Invoke(_gridArray[kvp.Key, i], kvp.Key, i - count);
                        _gridArray[kvp.Key, i] = null;
                    }
                    else
                    {
                        count++;
                    }
                }
                for (int i = 8 - count; i < 8; i++)
                {
                    GridSpawner.CreateNevGridAction?.Invoke(kvp.Key, i);
                }
                count = 0;
            }
        }
        
        
    }
}