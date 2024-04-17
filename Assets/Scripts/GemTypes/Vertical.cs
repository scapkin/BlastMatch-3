using Base;
using GamePlay;
using Interface;

namespace GemTypes
{
    public class Vertical: Grid,IInteractable
    {
        public void OnInteract()
        {
            GridManager.CheckGridConnectionAction?.Invoke(x,y);
        }
    }
}