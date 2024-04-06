using System;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionController
{
    private Func<List<GridObject>, List<GridObject>> func = (db) => { return new List<GridObject>(); };
    public static Action<GridObject[,]> AddGridObjectAction;
    private List<GridObject> _connectedObjects = new List<GridObject>();
    public void CheckConnection(GridObject[,] gridObjects, int collumn, int row)
    {
        for (int i = 0; i < gridObjects.GetLength(0); i++)
        {
            for (int j = 0; j < gridObjects.GetLength(1); j++)
            {
                if (gridObjects[i, j] != null)
                {
                }
            }
        }
    }
    
    
}