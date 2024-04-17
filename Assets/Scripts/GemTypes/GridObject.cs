using Base;
using GamePlay;
using Interface;
using Pool;
using UnityEngine;
using Grid = Base.Grid;

namespace GemTypes
{
    public class GridObject: Grid,IInteractable
    {
        public bool isCheck = false;
        public void OnInteract()
        {
            GridManager.CheckGridConnectionAction?.Invoke(x,y);
        }
    }
}