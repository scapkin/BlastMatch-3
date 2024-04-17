using System;
using System.Collections.Generic;
using DG.Tweening;
using GemTypes;
using ScriptableObject;
using UnityEngine;
using Pool;
using Grid = Base.Grid;

namespace GamePlay
{
    public class GridManager : MonoBehaviour
    {
        public static Action<Cell> AddGridObjectAction;
        public static Action<int,int> CheckGridConnectionAction;
        public static Action<Cell,int,int> SetGridAction;
        [SerializeField] private GridProperties gridProperties;

        private Cell[,] _gridArray;
        private List<Cell> _connectedGridObjects = new List<Cell>();

        private void Awake()
        {
            _gridArray = new Cell[gridProperties.GridSize, gridProperties.GridSize];
            Debug.Log(gridProperties.GridSize);
        }

        private void OnEnable()
        {
            CheckGridConnectionAction += CheckGridConnection;
            AddGridObjectAction += AddGridObject;
            SetGridAction += SetGrid;
        }
        private void OnDisable()
        {
            CheckGridConnectionAction -= CheckGridConnection;
            AddGridObjectAction -= AddGridObject;
            SetGridAction -= SetGrid;
        }
    
        private void CheckGridConnection(int x,int y)
        {
            _connectedGridObjects = ConnectionController.Instance.GetConnectedObjects(_gridArray, _gridArray[x,y].GridObject.PosX, _gridArray[x,y].GridObject.PosY, _gridArray[x,y].GridObject.Type);
            GridRemover.RemoveGridObject(_gridArray,_connectedGridObjects,x,y);
            // if (_connectedGridObjects.Count>5)
            // {
            //     GridSpawner.CreateNewGridAction?.Invoke(x,y, ObjectPoolItem.GemType.Bomb);
            // }
            // GridDropManager.Drop(_gridArray,_connectedGridObjects);
            GridTest();
            _connectedGridObjects.Clear();
        }

        private void GridTest()
        {
            int count = 0;
            for (int i = 0; i < _gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < _gridArray.GetLength(1); j++)
                {
                    if (_gridArray[i, j] == null)
                    {
                        count++;
                    }
                }
            }

            Debug.Log(count);
        }
    
        private void AddGridObject(Cell obj)
        {
            //Debug.Log(obj.GridObject.PosX + " " + obj.GridObject.PosY+ " " + obj.GridObject.Type + " " + obj.GridObject.GemObject.name);
            _gridArray[obj.GridObject.PosX, obj.GridObject.PosY] = obj;
            GridTransformMove(_gridArray[obj.GridObject.PosX, obj.GridObject.PosY], new Vector3(obj.GridObject.PosX * -gridProperties.Space, obj.GridObject.PosY * gridProperties.Space, 0));
        }
        private void GridTransformMove(Cell grid, Vector3 pos)
        {
            grid.GridObject.GemObject.transform.DOMove(pos, 0.3f);
        }
        
        private void SetGrid(Cell obj, int x, int y)
        {
            GridTransformMove(obj, new Vector3(x * -gridProperties.Space, y * gridProperties.Space, 0));
            _gridArray[x, y] = obj;
            obj.GridObject.PosX = x;
            obj.GridObject.PosY = y;
            
        }
        
    }
}