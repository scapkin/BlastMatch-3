using Base;
using GamePlay;
using Interface;
using UnityEngine;
using Grid = Base.Grid;

namespace GemTypes
{
    public class Bomb : Grid,IInteractable
    {
        public void OnInteract()
        {
            GridManager.CheckGridConnectionAction?.Invoke(x,y);
        }
    }
}