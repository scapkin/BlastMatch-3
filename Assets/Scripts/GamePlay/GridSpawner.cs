using System;
using System.Collections.Generic;
using GemTypes;
using Interface;
using Pool;
using ScriptableObject;
using UnityEngine;
using Grid = Base.Grid;
using Random = UnityEngine.Random;

namespace GamePlay
{
    public class GridSpawner : MonoBehaviour
    {
        public static Action<int,int,ObjectPoolItem.GemType,Cell.CellTypes > CreateNewGridAction;
        
        [SerializeField] private GridProperties gridProperties;

        private int _width;
        private int _height;
        private float _space;
        private int _rnd;
        private int _gemTypeLength;
        private GameObject _obj;
        private ObjectPoolItem.GemType _gemType;

        private List<int> selectedGemEnumValues = new List<int>();

        private void Start()
        {
            _gemTypeLength = gridProperties.Gems.Count;
            _width = gridProperties.GridSize;
            _height = gridProperties.GridSize;
            _space = gridProperties.Space;

            SelectedGemListFiller();
            InitializeGrid();
        }

        private void OnEnable()
        {
            CreateNewGridAction += CreateGridObject;
        }

        private void OnDisable()
        {
            CreateNewGridAction -= CreateGridObject;
        }


        private void InitializeGrid()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    GridSpawn(ObjectPoolItem.GemType.Background, new Vector2((-i * _space), (j * _space)));
                    CreateGridObject(i,j, ObjectPoolItem.GemType.Normal, Cell.CellTypes.Normal);
                }
            }
        }

        private void SelectedGemListFiller()
        {
            for (int i = 0; i < _gemTypeLength; i++)
            {
                selectedGemEnumValues.Add((int)gridProperties.Gems[i].Type);
            }
        }

        private void GetRandomGem(ObjectPoolItem.GemType type)
        {
            switch (type)
            {
                case ObjectPoolItem.GemType.Bomb:
                    _rnd = (int)ObjectPoolItem.GemType.Bomb;
                    break;
                case ObjectPoolItem.GemType.Horizontal:
                    _rnd = (int)ObjectPoolItem.GemType.Horizontal;
                    break;
                case ObjectPoolItem.GemType.Vertical:
                    _rnd = (int)ObjectPoolItem.GemType.Vertical;
                    break;
                default:
                    _rnd = Random.Range(0, _gemTypeLength);
                    _rnd = selectedGemEnumValues[_rnd];
                    break;
            }
        }

        private GameObject GridSpawn(ObjectPoolItem.GemType type, Vector3 pos)
        {
            return ObjectPool.Instance.GetObjectFromPool(type, pos);
        }
        
        private void CreateGridObject(int x, int y, ObjectPoolItem.GemType type, Cell.CellTypes cellTypes)
        {
            GetRandomGem(type);
            //GridSpawn(ObjectPoolItem.GemType.Background, new Vector2((-x * _space), (y * _space)));
            _obj = GridSpawn((ObjectPoolItem.GemType)_rnd, new Vector2((-x * _space), (y * _space)));
            Cell gem = new Cell();
            gem.GridObject = _obj.GetComponent<Grid>();
            gem.GridObject.PosX = x;
            gem.GridObject.PosY = y;
            gem.GridObject.Type = (ObjectPoolItem.GemType)_rnd;
            gem.CellType = cellTypes;
            gem.GridObject.GemObject = _obj;
            GridManager.AddGridObjectAction?.Invoke(gem);
        }
    }
}