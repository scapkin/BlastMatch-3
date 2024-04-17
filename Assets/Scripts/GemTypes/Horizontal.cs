using Base;
using GamePlay;
using Interface;

namespace GemTypes
{
    public class Horizontal: Grid,IInteractable
    {
        public void OnInteract()
        {
            GridManager.CheckGridConnectionAction?.Invoke(x,y);
        }
    }
}