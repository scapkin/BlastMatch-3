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
            
            if (gridObjects[x, y].CellType == Cell.CellTypes.Bomb)
            {
                BombEffect(gridObjects, x, y);
                return;
            }

            if (gridObjects[x, y].CellType == Cell.CellTypes.Horizontal)
            {
                ExplodeHorizontal(gridObjects, y);
                return;
            }

            if (gridObjects[x, y].CellType == Cell.CellTypes.Vertical)
            {
                ExplodeVerticle(gridObjects, x);
                
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

        
        private void CheckConnectedObjectTypes(Cell[,] gridObjects,int x,int y)
        {
            if (gridObjects[x,y].CellType == Cell.CellTypes.Bomb)
            {
                BombEffect(gridObjects,gridObjects[x,y].GridObject.PosX,gridObjects[x,y].GridObject.PosY);
            }

            if (gridObjects[x,y].GridObject.Type == ObjectPoolItem.GemType.Vertical)
            {
                ExplodeVerticle(gridObjects, x);
                
            }

            if (gridObjects[x,y].GridObject.Type == ObjectPoolItem.GemType.Horizontal)
            {
                ExplodeHorizontal(gridObjects, y);
            }
        }
        private void BombEffect(Cell[,] gridObjects, int x, int y)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < gridObjects.GetLength(0) && j >= 0 && j < gridObjects.GetLength(1))
                    {
                        if (gridObjects[i, j] != null && !gridObjects[i, j].GridObject.isCheck)
                        {
                            gridObjects[i, j].GridObject.isCheck = true;
                            if (!_connectedObjects.Contains(gridObjects[i, j]))
                            {
                                CheckConnectedObjectTypes(gridObjects,i,j);
                                
                                
                                _connectedObjects.Add(gridObjects[i, j]);
                            }
                        }
                    }
                }
            }
            
        }

        private void ExplodeVerticle(Cell[,] gridObjects, int x)
        {
            int height = gridObjects.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                if (gridObjects[x, y] != null&& !gridObjects[x, y].GridObject.isCheck)
                {
                    gridObjects[x, y].GridObject.isCheck = true;
                    if (!_connectedObjects.Contains(gridObjects[x, y]))
                    {
                        CheckConnectedObjectTypes(gridObjects,x,y);
                        _connectedObjects.Add(gridObjects[x, y]);
                    }
                }
            }
        }

        public void ExplodeHorizontal(Cell[,] gridObjects, int y)
        {
            int width = gridObjects.GetLength(0);

            for (int x = 0; x < width; x++)
            {
                if (gridObjects[x, y] != null&& !gridObjects[x, y].GridObject.isCheck)
                {
                    gridObjects[x, y].GridObject.isCheck = true;
                    if (!_connectedObjects.Contains(gridObjects[x, y]))
                    {
                        CheckConnectedObjectTypes(gridObjects,x,y);
                        _connectedObjects.Add(gridObjects[x, y]);
                    }
                }
            }
        }

        
    }
}