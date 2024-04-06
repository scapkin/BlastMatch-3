using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Pool;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GridProperties gridProperties;

    private GridObject[,] _initGridArray;
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
        _initGridArray = new GridObject[gridProperties.GridSize, gridProperties.GridSize];

        SelectedGemListFiller();
        InitializeGrid();
    }


    private void InitializeGrid()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                GetRandomGem();
                GemSpawn(ObjectPoolItem.GemType.Background, new Vector2((-i * _space), (j * _space)));
                _obj = GemSpawn((ObjectPoolItem.GemType)_rnd, new Vector2((-i * _space), (j * _space)));
                GridManager.AddGridObjectAction?.Invoke(new GridObject(i, j, (ObjectPoolItem.GemType)_rnd, _obj));
                _initGridArray[i, j] = new GridObject(i, j, (ObjectPoolItem.GemType)_rnd, _obj);
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

    private GameObject GemSpawn(ObjectPoolItem.GemType type, Vector3 pos)
    {
        return ObjectPool.Instance.GetObjectFromPool(type, pos);
    }
}