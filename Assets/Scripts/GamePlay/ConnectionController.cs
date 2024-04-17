using System.Collections.Generic;
using Singleton;
using GemTypes;
using Pool;
using UnityEngine;

namespace GamePlay
{
    public class ConnectionController : Singleton<ConnectionController>
    {
        private List<Cell> _connectedObjects = new List<Cell>();


        public List<Cell> GetConnectedObjects(Cell[,] gridObjects, int x, int y,
            ObjectPoolItem.GemType type)
        {
            FindConnectedObjects(gridObjects, x, y, type);
            if (_connectedObjects.Count < 2)
            {
                _connectedObjects.Clear();
            }

            return _connectedObjects;
        }
        
        private void FindConnectedObjects(Cell[,] gridObjects, int x, int y, ObjectPoolItem.GemType type)
        {
            if (x < 0 || y < 0 || x >= gridObjects.GetLength(0) || y >= gridObjects.GetLength(1))
                return;
            if (gridObjects[x,y] == null)
                return;
            if (gridObjects[x, y].GridObject.isCheck || gridObjects[x, y].GridObject.Type != type )
                return;
            
            if (gridObjects[x, y].GridObject.Type == ObjectPoolItem.GemType.Bomb)
            {
                BombEffect(gridObjects, x, y);
                return;
            }

            if (gridObjects[x, y].GridObject.Type == ObjectPoolItem.GemType.Horizontal)
            {
                ExplodeHorizontal(gridObjects, x);
                return;
            }

            if (gridObjects[x, y].GridObject.Type == ObjectPoolItem.GemType.Vertical)
            {
                ExplodeWidth(gridObjects, y);
                return;
            }

            

            gridObjects[x, y].GridObject.isCheck = true;

            if (!_connectedObjects.Contains(gridObjects[x, y]))
            {
                _connectedObjects.Add(gridObjects[x, y]);
            }

            FindConnectedObjects(gridObjects, x + 1, y, type);
            FindConnectedObjects(gridObjects, x - 1, y, type);
            FindConnectedObjects(gridObjects, x, y + 1, type);
            FindConnectedObjects(gridObjects, x, y - 1, type);
        }

        

        private void BombEffect(Cell[,] gridObjects, int x, int y)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < gridObjects.GetLength(0) && j >= 0 && j < gridObjects.GetLength(1))
                    {
                        if (gridObjects[i, j] != null)
                        {
                            if (gridObjects[i, j].GridObject.Type == ObjectPoolItem.GemType.Bomb)
                            {
                                
                            }

                            if (!_connectedObjects.Contains(gridObjects[i, j]))
                            {
                                _connectedObjects.Add(gridObjects[i, j]);
                            }
                        }
                    }
                }
            }
        }

        private void ExplodeHorizontal(Cell[,] gridObjects, int x)
        {
            int height = gridObjects.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                if (gridObjects[x, y] != null)
                {
                    if (!_connectedObjects.Contains(gridObjects[x, y]))
                    {
                        _connectedObjects.Add(gridObjects[x, y]);
                    }
                }
            }
        }

        public void ExplodeWidth(Cell[,] gridObjects, int y)
        {
            int width = gridObjects.GetLength(0);

            for (int x = 0; x < width; x++)
            {
                if (gridObjects[x, y] != null)
                {
                    if (!_connectedObjects.Contains(gridObjects[x, y]))
                    {
                        _connectedObjects.Add(gridObjects[x, y]);
                    }
                }
            }
        }

        private void CheckConnectedObjectTypes()
        {
            foreach (var obj in _connectedObjects)
            {
                if (obj.GridObject.Type == ObjectPoolItem.GemType.Bomb)
                {
                }

                if (obj.GridObject.Type == ObjectPoolItem.GemType.Horizontal)
                {
                }

                if (obj.GridObject.Type == ObjectPoolItem.GemType.Vertical)
                {
                }
            }
        }
    }
}