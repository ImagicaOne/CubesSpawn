using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IEndDragHandler, IDragHandler
{
     public UnityEvent<Item> DragEnded;
    
     [field: SerializeField]
    public int X { get; private set; }
    
    [field: SerializeField]
    public int Y { get; private set; }

    public SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
    
    public Sprite GetSprite()
    {
        return _spriteRenderer.sprite;
    }
    
    public void SetIndex(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragEnded.Invoke(this);       
    }

    public void OnDrag(PointerEventData eventData)
    {
        //I wondered why without this method dragging not working at all!?
    }
}
