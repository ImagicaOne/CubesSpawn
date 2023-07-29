using System;
using System.Linq;
using UnityEngine;

public class DragController : MonoBehaviour
{
    [SerializeField]
    private MatchProvider _matchProvider;
    
    [SerializeField]
    private ItemsReorderer _itemsReorderer;
    
    [SerializeField]
    private AnimationController _animationController;
    
    private int _fieldSize;

    public void Initialize(int fieldSize)
    {
        _fieldSize = fieldSize;
        
        foreach (var item in ItemsProvider.Instance.Items)
        {
            item.DragEnded.AddListener(item => OnDragItem(item));
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
            _animationController.SwapSmoothly(item, itemForSwap, () => _itemsReorderer.HandleSwapWithMatches(matches));
        }
        else
        {
            _itemsReorderer.SwapItems(item, itemForSwap);
            _animationController.SwapSmoothlyWithReturn(item, itemForSwap);
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
