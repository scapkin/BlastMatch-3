using System;
using System.Collections.Generic;
using DG.Tweening;
using GemTypes;
using ScriptableObject;
using UnityEngine;

namespace GamePlay
{
    public class GridManager : MonoBehaviour
    {
        public static Action<GridObject> AddGridObjectAction;
        public static Action<int,int> CheckGridConnectionAction;
        public static Action<GridObject,int,int> SetGridAction;
        [SerializeField] private GridProperties gridProperties;
    
        private int _colLength;
        private int _rowLength;
        private GridObject[,] _gridArray = new GridObject[8, 8];
        private List<GridObject> _connectedGridObjects = new List<GridObject>();

        private void Awake()
        {
            _colLength = gridProperties.GridSize;
            _rowLength = gridProperties.GridSize;
            _gridArray = new GridObject[_colLength, _rowLength];
            
        }

        private void OnEnable()
        {
            CheckGridConnectionAction += CheckGridConnection;
            AddGridObjectAction += OnAddGridObject;
            SetGridAction += SetGrid;
        }
        private void OnDisable()
        {
            CheckGridConnectionAction -= CheckGridConnection;
            AddGridObjectAction -= OnAddGridObject;
            SetGridAction -= SetGrid;
        }
    
        private void CheckGridConnection(int x,int y)
        {
            _connectedGridObjects = ConnectionController.Instance.GetConnectedObjects(_gridArray, _gridArray[x,y].PosX, _gridArray[x,y].PosY, _gridArray[x,y].Type);
            GridRemover.RemoveGridObject(_gridArray,_connectedGridObjects);
            GridDropManager.Drop(_gridArray,_connectedGridObjects);
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
    
        private void OnAddGridObject(GridObject obj)
        {
            _gridArray[obj.PosX, obj.PosY] = obj;
            GridTransformMove(_gridArray[obj.PosX, obj.PosY], new Vector3(obj.PosX * -gridProperties.Space, obj.PosY * gridProperties.Space, 0));
        }
        private void GridTransformMove(GridObject grid, Vector3 pos)
        {
            grid.GemObject.transform.DOMove(pos, 0.3f);
        }
        
        private void SetGrid(GridObject obj, int x, int y)
        {
            GridTransformMove(obj, new Vector3(x * -gridProperties.Space, y * gridProperties.Space, 0));
            _gridArray[x, y] = obj;
            obj.PosX = x;
            obj.PosY = y;
            
        }
        
    }
}