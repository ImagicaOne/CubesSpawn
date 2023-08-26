using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class ItemsReorderer : MonoBehaviour
{
    [SerializeField]
    private MatchProvider _matchProvider;
    
    [SerializeField]
    private ItemsSpawner _itemsSpawner;
    
    [SerializeField]
    private AnimationController _animationController;
    
    [SerializeField]
    private SoundManager _soundManager;
    
    private int _fieldSize;

    public void Initialize(int fieldSize)
    {
        _fieldSize = fieldSize;
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
        _soundManager.PlayOnRemoving();
        _animationController.DecreaseScaleAnimation(items).OnComplete(
        () =>
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
        }
    }

    private void DropItems()
    {
        var size = 2;
        List<(Item, Vector3)> itemsPositions = new ();
        List<Item> itemsToSwap = new ();
        
        for (int column = 0; column < _fieldSize; column++)
        {
            int count = 0;

            for (int row = _fieldSize - 1; row >= 0; row--)
            {
                if (ItemsProvider.Instance.Items[row, column].GetSprite() == null)
                {
                    count++;
                }
                else if (count > 0)
                {
                    var x1 = ItemsProvider.Instance.Items[row, column].transform.position.x;
                    var y1 = ItemsProvider.Instance.Items[row, column].transform.position.y;
                    itemsPositions.Add((ItemsProvider.Instance.Items[row, column],
                        new Vector3(x1,y1 - size * count)));

                    ItemsProvider.Instance.Items[row, column].SetIndex(row + count, column);
                    itemsToSwap.Add(ItemsProvider.Instance.Items[row, column]);

                }
            }
        }

        
        for (int column = 0; column < _fieldSize; column++)
        {
            int count = 0;

            for (int row = 0; row < _fieldSize; row++)
            {
                if (ItemsProvider.Instance.Items[row, column].GetSprite() != null)
                {
                    count++;
                }
                else if (count > 0)
                {
                    var x1 = ItemsProvider.Instance.Items[row, column].transform.position.x;
                    var y1 = ItemsProvider.Instance.Items[row, column].transform.position.y;
                    itemsPositions.Add((ItemsProvider.Instance.Items[row, column],
                        new Vector3(x1, y1 + size * count)));
                    
                    ItemsProvider.Instance.Items[row, column].SetIndex(row - count, column);
                    itemsToSwap.Add(ItemsProvider.Instance.Items[row, column]);
                }
            }
        }

        _animationController.MoveSmoothly(itemsPositions.ToArray(), () =>
        {
            foreach (var item in itemsToSwap)
            {
                ItemsProvider.Instance.Items[item.X, item.Y] = item;
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
        });
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
