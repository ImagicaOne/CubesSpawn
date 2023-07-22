using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchProvider : MonoBehaviour
{
    private int _fieldSize;

    public void Initialize(int fieldSize)
    {
        _fieldSize = fieldSize;
    }

    public Item[] GetAllMatches()
    {
        var h = GetHorizontalMatches();
        var v = GetVerticalMatches();
        return h.Union(v).Distinct().ToArray();
    }

    public Item[] GetHorizontalMatches()
    {
        var items = ItemsProvider.Instance.Items;
        var result = new List<Item>();
        Sprite spriteCurr, spriteNext; 
        
        // Dimension A - row
        // Dimension B - column
        for (int distA = 0; distA < _fieldSize; distA++)
        {
            int count = 1;

            for (int distB = 0; distB < _fieldSize; distB++)
            {
                if (items[distA, distB] is null)
                {
                    spriteCurr = null; 
                }
                else
                {
                    spriteCurr = items[distA, distB].GetSprite();  
                }
                
                if (items[distA, distB + 1] is null)
                {
                    spriteNext = null;
                }
                else
                {
                    spriteNext = items[distA, distB + 1].GetSprite();  
                }
                 

                if (distB == _fieldSize - 2)
                {
                    if (spriteCurr == spriteNext && count >= 2)
                    {
                        count += 1;
                        result.AddRange(GetItemsToDelete(distA, distA,  distB - count + 2, distB + 1));
                        count = 1;
                    }

                    else if (spriteCurr != spriteNext && count >= 3)
                    {
                        result.AddRange(GetItemsToDelete(distA, distA,  distB - count + 1, distB)); 
                        count = 1;
                    }
                    
                    break;
                }

                if ( (spriteCurr != null || spriteNext != null ) && spriteCurr == spriteNext)
                {
                    count += 1;
                    continue;
                }
                else if ( (spriteCurr != null || spriteNext != null ) && spriteCurr != spriteNext)
                {
                    if (count >= 3)
                    {
                        result.AddRange(GetItemsToDelete(distA, distA, distB - count + 1, distB));
                    }
                    count = 1;
                }
            }
        }

        return result.Distinct().ToArray();
    }

    public Item[] GetVerticalMatches()
    {
        var items = ItemsProvider.Instance.Items;
        var result = new List<Item>();
        Sprite spriteCurr, spriteNext; 

        // Dimension B - row
        // Dimension A - column
        for (int distA = 0; distA < _fieldSize; distA++)
        {
            int count = 1;

            for (int distB = 0; distB < _fieldSize; distB++)
            {
                if (items[distB, distA] is null)
                {
                    spriteCurr = null; 
                }
                else
                {
                    spriteCurr = items[distB, distA].GetSprite();  
                }
                
                if (items[distB + 1, distA] is null)
                {
                    spriteNext = null;
                }
                else
                {
                    spriteNext = items[distB + 1, distA].GetSprite();  
                }
                 

                if (distB == _fieldSize - 2)
                {
                    if (spriteCurr == spriteNext && count >= 2)
                    {
                        count += 1;
                        result.AddRange(GetItemsToDelete(distB - count + 2, distB  + 1, distA, distA));
                        count = 1;
                    }

                    else if (spriteCurr != spriteNext && count >= 3)
                    {
                        result.AddRange(GetItemsToDelete(distB - count + 1, distB ,distA, distA)); 
                        count = 1;
                    }
                    
                    break;
                }

                if ( (spriteCurr != null || spriteNext != null ) && spriteCurr == spriteNext)
                {
                    count += 1;
                    continue;
                }
                else if ( (spriteCurr != null || spriteNext != null ) && spriteCurr != spriteNext)
                {
                    if (count >= 3)
                    {
                        result.AddRange(GetItemsToDelete(distB - count + 1, distB, distA, distA));
                    }
                    count = 1;
                }
            }
        }

        return result.Distinct().ToArray();
    }

    private List<Item> GetItemsToDelete(int distAStart, int distAEnd, int distBStart, int distBEnd)
    {
        var result = new List<Item>();
        var items = ItemsProvider.Instance.Items;
        for (int a = distAStart; a <= distAEnd; a++)
        {
            for (int b = distBStart; b <= distBEnd; b++)
            {
                result.Add(items[a, b]);
            }
        }
        return result;
    }


    #region MyRegion obsolete implementation
    
    
        public Item[] GetAllMatches(Item item)
    {
        var verticalItems = GetMatchItems(GetColumnForCheck(item));
        var horizontalItems = GetMatchItems(GetRowForCheck(item));

        return verticalItems.Union(horizontalItems).ToArray();
    }

    private Item[] GetMatchItems(Item[] items)
    {
        //not needed to check if count of items < 3
        if (items.Length < 3)
        {
            return Array.Empty<Item>();
        }

        int count = 1;

        for (int i = 1; i < items.Length; i++)
        {
            var sprite = items[i].GetSprite();

            if (items[i - 1].GetSprite() == sprite)
            {
                count++;
            }
            else if (count >= 3)
            {
                // get range of match items
                return items.Skip(i - count).Take(count).ToArray();
            }
            else
            {
                count = 1;
            }
        }

        if (count >= 2)
        {
            // get range of match items
            return items.Skip(items.Length - count).Take(count).ToArray();
        }

        return Array.Empty<Item>();
    }

    private Item[] GetColumnForCheck(Item item)
    {
        Item[] neighbourItems = new Item[5];

        var x = item.X;
        var y = item.Y;

        if (x - 2 >= 0)
        {
            neighbourItems[0] = ItemsProvider.Instance.Items[x - 2, y];
        }

        if (x - 1 >= 0)
        {
            neighbourItems[1] = ItemsProvider.Instance.Items[x - 1, y];
        }

        neighbourItems[2] = item;

        if (x + 1 < _fieldSize)
        {
            neighbourItems[3] = ItemsProvider.Instance.Items[x + 1, y];
        }

        if (x + 2 < _fieldSize)
        {
            neighbourItems[4] = ItemsProvider.Instance.Items[x + 2, y];
        }

        return neighbourItems.Where(i => i != null).ToArray();
    }

    private Item[] GetRowForCheck(Item item)
    {
        Item[] neighbourItems = new Item[5];

        var x = item.X;
        var y = item.Y;

        if (y - 2 >= 0)
        {
            neighbourItems[0] = ItemsProvider.Instance.Items[x, y - 2];
        }

        if (y - 1 >= 0)
        {
            neighbourItems[1] = ItemsProvider.Instance.Items[x, y - 1];
        }

        neighbourItems[2] = item;

        if (y + 1 < _fieldSize)
        {
            neighbourItems[3] = ItemsProvider.Instance.Items[x, y + 1];
        }

        if (y + 2 < _fieldSize)
        {
            neighbourItems[4] = ItemsProvider.Instance.Items[x, y + 2];
        }

        return neighbourItems.Where(i => i != null).ToArray();
    }
    
    #endregion
}