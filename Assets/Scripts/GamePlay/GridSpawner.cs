using System;
using System.Collections.Generic;
using GemTypes;
using Pool;
using ScriptableObject;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay
{
    public class GridSpawner : MonoBehaviour
    {
        public static Action<int,int> CreateNevGridAction;
        
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
            CreateNevGridAction += CreateGridObject;
        }

        private void OnDisable()
        {
            CreateNevGridAction -= CreateGridObject;
        }


        private void InitializeGrid()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    CreateGridObject(i,j);
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

        private void GetRandomGem()
        {
            _rnd = Random.Range(0, _gemTypeLength);
            _rnd = selectedGemEnumValues[_rnd];
        }

        private GameObject GridSpawn(ObjectPoolItem.GemType type, Vector3 pos)
        {
            return ObjectPool.Instance.GetObjectFromPool(type, pos);
        }
        
        private void CreateGridObject(int x, int y)
        {
            GetRandomGem();
            GridSpawn(ObjectPoolItem.GemType.Background, new Vector2((-x * _space), (y * _space)));
            _obj = GridSpawn((ObjectPoolItem.GemType)_rnd, new Vector2((-x * _space), (y * _space)));
            GridObject gem = _obj.GetComponent<GridObject>();
            gem.PosX = x;
            gem.PosY = y;
            gem.Type = (ObjectPoolItem.GemType)_rnd;
            gem.GemObject = _obj;
            GridManager.AddGridObjectAction?.Invoke(gem);
        }
    }
}