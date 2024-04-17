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
            //TODO: Implement Bomb logic    
            GridManager.CheckGridConnectionAction?.Invoke(x,y);
        }
    }
}