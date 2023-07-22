using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DragController : MonoBehaviour
{
    [SerializeField]
    private MatchProvider _matchProvider;
    
    [SerializeField]
    private ItemsReorderer _itemsReorderer;
    
    private int _fieldSize;
    
    private UnityEvent<Item, Item> _onSwap = new ();
    
    private UnityEvent<Action> _onSwapComplete = new ();
    
    private UnityEvent<Item, Item> _onSwapWithReturn = new ();

    public void Initialize(int fieldSize, 
        UnityAction<Item, Item>[] onSwap, 
        UnityAction<Action>[] onSwapComplete,
        UnityAction<Item, Item>[] onSwapWithReturn)
    {
        _fieldSize = fieldSize;
        
        foreach (var item in ItemsProvider.Instance.Items)
        {
            item._onDragEvent.AddListener(item => OnDragItem(item));
        }
        
        foreach (var action in onSwap)
        {
            _onSwap.AddListener(action);
        }
        
        foreach (var action in onSwapComplete)
        {
            _onSwapComplete.AddListener(action);
        }
        
        foreach (var action in onSwapWithReturn)
        {
            _onSwapWithReturn.AddListener(action);
        }
    }
    
    public void OnDragItem(Item item)
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        
        var itemForSwap = FindItemForSwap(position, item);

        _itemsReorderer.SwapItems(item, itemForSwap);
        
        var matches = _matchProvider.GetAllMatches();
        if (matches.Length > 0)
        {
            _onSwap.Invoke(item, itemForSwap);
            _onSwapComplete.Invoke(() => _itemsReorderer.HandleSwapWithMatches(matches));
        }
        else
        {
            _itemsReorderer.SwapItems(item, itemForSwap);
            _onSwapWithReturn.Invoke(item, itemForSwap);
        }
    }

    //find closest item among neighbours OR find selfitem to do nothing
    private Item FindItemForSwap(Vector2 mousePosition, Item item)
    {
        var itemsForCheck = GetNeighbourItems(item);
        return itemsForCheck.OrderBy(i => Vector2.Distance(mousePosition, i.transform.position)).First();
    }
    
    private Item[] GetNeighbourItems(Item item)
    {
        Item[] neighbourItems = new Item[5];

        neighbourItems[0] = item;

        var x = item.X;
        var y= item.Y;

        if (x - 1 >= 0)
        {
            neighbourItems[1] = ItemsProvider.Instance.Items[x - 1, y];
        }

        if (x + 1 < _fieldSize)
        {
            neighbourItems[2] = ItemsProvider.Instance.Items[x + 1, y];
        }

        if (y - 1 >= 0)
        {
            neighbourItems[3] = ItemsProvider.Instance.Items[x, y - 1];
        }

        if (y + 1 < _fieldSize)
        {
            neighbourItems[4] = ItemsProvider.Instance.Items[x, y + 1];
        }

        return neighbourItems.Where(i => i != null).ToArray();
    }
}
