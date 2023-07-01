using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//On items grid
public class ItemsController : MonoBehaviour
{
    [SerializeField]
    private GameObject _itemPrefab;

    [SerializeField]
    private SpriteCollectionSO _spriteCollection;

    [SerializeField]
    private Animator _animator;

    private Item[] _items;

    private int[,] _spriteIndexes;

    private int _size;
    private int _count;

    public void Initialize(int size)
    {
        _size = size;   
        _count = (int)Mathf.Pow(size, 2);
        _items = new Item[_count];
        _spriteIndexes = new int [_size, _size];

        for (int i = 0; i < _spriteIndexes.GetLength(0); i++)
        {
            for (int j = 0; j < _spriteIndexes.GetLength(1); j++)
            {
                _spriteIndexes[i, j] = -1;
            }
        }

        SpawnItems();
    }

    public Sprite ChooseSprite(int i)
    {     
        int index = -1;
        do
        {
            index = Random.Range(0, _spriteCollection.Items.Length);
            _spriteIndexes[i / _size, i % _size] = index;
        }
        while (CheckSpawnCondition());

        return _spriteCollection.Items[index];
    }

    public void OnDragItem(Item item)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, Input.mousePosition, Camera.main, out var localPoint);
        
        //find closest item among neighbours or find selfitem
        var itemForSwap = FindItemForSwap(localPoint, item.Index);

        SwapIndexesInCollection(item.Index, itemForSwap.Index);
        if (true)
        {
            int tmpIndex = item.Index;

            item.SetIndex(itemForSwap.GetComponent<Item>().Index);
            itemForSwap.GetComponent<Item>().SetIndex(tmpIndex);

            SwapItemsInCollection(item.Index, itemForSwap.Index);

            _animator.SwapSmoothly(item.transform, itemForSwap.transform);
        }
        else
        {
            SwapIndexesInCollection(item.Index, itemForSwap.Index);
            _animator.SwapSmoothlyWithReturn(item.transform, itemForSwap.transform);
        }
    }

    private void SpawnItems()
    {
        for (int i = 0; i < _count; i++)
        {
            var newItem = Instantiate(_itemPrefab, gameObject.transform);

            newItem.GetComponent<Image>().sprite = ChooseSprite(i);
            var item = newItem.GetComponent<Item>();
            item.SetIndex(i);
            _items[i] = item;

            _animator.ScaleAnimation(newItem.transform);

            item._onDragEvent.AddListener((item) => OnDragItem(item));
        }
    }

    private Item FindItemForSwap(Vector2 mousePosition, int index)
    {
        var itemsForCheck = GetNeighbourItems(index);
        return itemsForCheck.OrderBy(i => Vector2.Distance(mousePosition, i.transform.localPosition)).First();
    }

    private void SwapItemsInCollection(int index1, int index2)
    {
        Item temp = _items[index1];
        _items[index1] = _items[index2];
        _items[index2] = temp;
    }

    private void SwapIndexesInCollection(int index1, int index2)
    {
        int temp = _spriteIndexes[index1 / _size, index1 % _size];
        _spriteIndexes[index1 / _size, index1 % _size] = _spriteIndexes[index2 / _size, index2 % _size];
        _spriteIndexes[index2 / _size, index2 % _size] = temp;
    }

    private bool CheckSpawnCondition()
    {
        for (int i = 0; i < _spriteIndexes.GetLength(0); i++)
        {
            for (int j = 0; j < _spriteIndexes.GetLength(1); j++)
            {
                if (j > 0 && j < _spriteIndexes.GetLength(1) - 1 && _spriteIndexes[i, j] != -1
                    && _spriteIndexes[i, j - 1] == _spriteIndexes[i, j]
                    && _spriteIndexes[i, j + 1] == _spriteIndexes[i, j])
                {
                    return true;
                }

                if (i > 0 && i < _spriteIndexes.GetLength(0) - 1 && _spriteIndexes[i, j] != -1
                    && _spriteIndexes[i - 1, j] == _spriteIndexes[i, j]
                    && _spriteIndexes[i + 1, j] == _spriteIndexes[i, j])
                {
                    return true;
                }
            }
        }

        return false;
    }

    private Item[] GetNeighbourItems(int index)
    {
        Item[] neighbourItems = new Item[5];

        neighbourItems[0] = _items[index];

        if ((index + 1) % _size != 0)
        {
            neighbourItems[1] = _items[index + 1];
        }

        if ((index - _size) % _size != 0)
        {
            neighbourItems[2] = _items[index - 1];
        }

        if (index - _size > 0)
        {
            neighbourItems[3] = _items[index - _size];
        }

        if (index + _size < _count - 1)
        {
            neighbourItems[4] = _items[index + _size];
        }

        return neighbourItems.Where(i => i != null).ToArray();
    }
}
