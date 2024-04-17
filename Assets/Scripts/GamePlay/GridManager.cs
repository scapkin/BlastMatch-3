using System;
using System.Collections.Generic;
using DG.Tweening;
using GemTypes;
using Pool;
using ScriptableObject;
using UnityEngine;

namespace GamePlay
{
    public class GridManager : MonoBehaviour
    {
        public static Action<GridObject> AddGridObjectAction;
        public static Action<int,int> CheckGridConnectionAction;
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
        }
        private void OnDisable()
        {
            CheckGridConnectionAction -= CheckGridConnection;
            AddGridObjectAction -= OnAddGridObject;
        }
    
        private void CheckGridConnection(int x,int y)
        {
            _connectedGridObjects = ConnectionController.Instance.GetConnectedObjects(_gridArray, _gridArray[x,y].PosX, _gridArray[x,y].PosY, _gridArray[x,y].Type);
            GridRemover.Instance.RemoveGridObject(_gridArray,_connectedGridObjects);
            _connectedGridObjects.Clear();
        }
    
        private void OnAddGridObject(GridObject obj)
        {
            _gridArray[obj.PosX, obj.PosY] = obj;
            GridTransformMove(_gridArray[obj.PosX, obj.PosY], new Vector3(obj.PosX * -gridProperties.Space, obj.PosY * gridProperties.Space, 0));
        }
        private void GridTransformMove(GridObject gem, Vector3 pos)
        {
            gem.GemObject.transform.DOMove(pos, 0.3f);
        }
    }
}