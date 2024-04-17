using System.Collections.Generic;
using Singleton;
using GemTypes;
using Pool;

namespace GamePlay
{
    public class ConnectionController : Singleton<ConnectionController>
    {
        private List<GridObject> _connectedObjects = new List<GridObject>();


        public List<GridObject> GetConnectedObjects(GridObject[,] gridObjects, int x, int y,
            ObjectPoolItem.GemType type)
        {
            FindConnectedObjects(gridObjects, x, y, type);
            if (_connectedObjects.Count < 2)
            {
                _connectedObjects.Clear();
            }

            return _connectedObjects;
        }
        
        private void FindConnectedObjects(GridObject[,] gridObjects, int x, int y, ObjectPoolItem.GemType type)
        {
            if (x < 0 || y < 0 || x >= gridObjects.GetLength(0) || y >= gridObjects.GetLength(1))
                return;
            if (gridObjects[x,y] == null)
                return;
            
            if (gridObjects[x, y].isCheck || gridObjects[x, y].Type != type )
                return;
            
            if (gridObjects[x, y].Type == ObjectPoolItem.GemType.Bomb)
            {
                BombEffect(gridObjects, x, y);
                return;
            }

            if (gridObjects[x, y].Type == ObjectPoolItem.GemType.Horizontal)
            {
                ExplodeHorizontal(gridObjects, x);
                return;
            }

            if (gridObjects[x, y].Type == ObjectPoolItem.GemType.Vertical)
            {
                ExplodeWidth(gridObjects, y);
                return;
            }

            

            gridObjects[x, y].isCheck = true;

            if (!_connectedObjects.Contains(gridObjects[x, y]))
            {
                _connectedObjects.Add(gridObjects[x, y]);
            }

            FindConnectedObjects(gridObjects, x + 1, y, type);
            FindConnectedObjects(gridObjects, x - 1, y, type);
            FindConnectedObjects(gridObjects, x, y + 1, type);
            FindConnectedObjects(gridObjects, x, y - 1, type);
        }

        

        private void BombEffect(GridObject[,] gridObjects, int x, int y)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < gridObjects.GetLength(0) && j >= 0 && j < gridObjects.GetLength(1))
                    {
                        if (gridObjects[i, j] != null)
                        {
                            if (gridObjects[i, j].Type == ObjectPoolItem.GemType.Bomb)
                            {
                            }

                            _connectedObjects.Add(gridObjects[i, j]);
                        }
                    }
                }
            }
        }

        private void ExplodeHorizontal(GridObject[,] gridObjects, int x)
        {
            int height = gridObjects.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                if (gridObjects[x, y] != null)
                {
                    _connectedObjects.Add(gridObjects[x, y]);
                }
            }
        }

        public void ExplodeWidth(GridObject[,] gridObjects, int y)
        {
            int width = gridObjects.GetLength(0);

            for (int x = 0; x < width; x++)
            {
                if (gridObjects[x, y] != null)
                {
                    _connectedObjects.Add(gridObjects[x, y]);
                }
            }
        }

        private void CheckConnectedObjectTypes()
        {
            foreach (var obj in _connectedObjects)
            {
                if (obj.Type == ObjectPoolItem.GemType.Bomb)
                {
                }

                if (obj.Type == ObjectPoolItem.GemType.Horizontal)
                {
                }

                if (obj.Type == ObjectPoolItem.GemType.Vertical)
                {
                }
            }
        }
    }
}