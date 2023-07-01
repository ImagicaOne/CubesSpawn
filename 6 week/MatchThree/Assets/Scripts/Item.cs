using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IEndDragHandler, IDragHandler
{
    public int Index => _index;

    [SerializeField]
    private int _index;

    public UnityEvent<Item> _onDragEvent;

    public void SetIndex(int index)
    {
        _index = index;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _onDragEvent.Invoke(this);       
    }

    public void OnDrag(PointerEventData eventData)
    {
        //I wondered why without this method dragging not working at all!?
    }
}
