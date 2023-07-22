using System;
using UnityEngine;
using UnityEngine.Events;

public class ItemsReorderer : MonoBehaviour
{
    [SerializeField]
    private MatchProvider _matchProvider;
    
    [SerializeField]
    private ItemsSpawner _itemsSpawner;
    
    private int _fieldSize;
    
    private UnityEvent<Item> _onRemove = new ();
    
    private UnityEvent<Action> _onRemoveComplete = new ();
    
    private UnityEvent<Item, Item> _onDrop = new ();
    
    private UnityEvent<Action> _onDropComplete = new ();

    public void Initialize(int fieldSize, 
        UnityAction<Item>[] onRemove, 
        UnityAction<Action>[] onRemoveComplete,
        UnityAction<Item, Item>[] onDrop,
        UnityAction<Action>[] onDropComplete)
    {
        _fieldSize = fieldSize;
        
        foreach (var action in onRemove)
        {
            _onRemove.AddListener(action);
        }
        
        foreach (var action in onRemoveComplete)
        {
            _onRemoveComplete.AddListener(action);
        }
        
        foreach (var action in onDrop)
        {
            _onDrop.AddListener(action);
        }
        
        foreach (var action in onDropComplete)
        {
            _onDropComplete.AddListener(action);
        }
    }
    
    public void SwapItems(Item item1, Item item2)
    {
        SwapItemsIndexes(item1, item2);
        SwapItemsInCollection(item1, item2);
    }
    
    public void HandleSwapWithMatches(Item[] items)
    {
        RemoveItems(items);
    }
    
    private void RemoveItems(Item[] items)
    {
        foreach (var item in items)
        {
            _onRemove.Invoke(item);
        }
        _onRemoveComplete.Invoke(() =>
        {
            ClearSprites(items);
            DropItems();
        });
    }

    private void ClearSprites(Item[] items)
    {
        foreach (var item in items)
        {
            //clear sprite
            item.SetSprite(null);

            //return normal size after animation of removing sprite.
            item.transform.localScale = new Vector3(1, 1, 0);
        }
    }

    private void DropItems()
    {
        for (int column = 0; column < _fieldSize; column++)
        {
            int count = 0;
            for (int row = _fieldSize - 1; row >= 0 ; row--)
            {
                if (ItemsProvider.Instance.Items[row, column].GetSprite() == null)
                {
                    count++;
                }
                else if (count > 0)
                {
                    SwapItems(ItemsProvider.Instance.Items[row, column],
                        ItemsProvider.Instance.Items[row + count, column]);
                    
                    (ItemsProvider.Instance.Items[row, column].transform.position,
                            ItemsProvider.Instance.Items[row + count, column].transform.position)
                        = (ItemsProvider.Instance.Items[row + count, column].transform.position,
                            ItemsProvider.Instance.Items[row, column].transform.position);
                    
                    /*_onDrop.Invoke(ItemsProvider.Instance.Items[row, column],
                        ItemsProvider.Instance.Items[row + count, column]);
                    _onDropComplete.Invoke();*/
                }
            }
        }

        var matches = _matchProvider.GetAllMatches();
        if (matches.Length > 0)
        {
            RemoveItems(matches);
        }
        else
        {
            _itemsSpawner.SetSprites();
        }
    }
    
    private void SwapItemsIndexes(Item item1, Item item2)
    {
        int tempX = item1.X;
        int tempY = item1.Y;
        
        item1.SetIndex(item2.X, item2.Y);
        item2.SetIndex(tempX, tempY);
    }
    
    private void SwapItemsInCollection(Item item1, Item item2)
    {
        ItemsProvider.Instance.Items[item1.X, item1.Y] = item1;
        ItemsProvider.Instance.Items[item2.X, item2.Y] = item2;
    }
}
