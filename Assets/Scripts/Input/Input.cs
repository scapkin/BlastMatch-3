using Interface;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Input
{
    public class Input : MonoBehaviour,IPointerDownHandler
    {
        private Vector3 _gemPos;
    
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject.CompareTag("Gem"))
            {
                eventData.pointerCurrentRaycast.gameObject.GetComponent<IInteractable>().OnInteract();
                //_gemPos = eventData.pointerCurrentRaycast.gameObject.transform.position;
            }
        }
    }
}
