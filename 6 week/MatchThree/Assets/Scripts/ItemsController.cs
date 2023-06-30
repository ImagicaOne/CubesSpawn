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

    public Item[] _items;

    private int _size;
    private int _count;

    public void Initialize(int size)
    {
        _size = size;   
        _count = (int)Mathf.Pow(size, 2);
        _items = new Item[_count];
        SpawnItems();
    }

    private void SpawnItems()
    {
        for (int i = 0; i < _count; i++)
        {
            var newItem = Instantiate(_itemPrefab, gameObject.transform);
            newItem.GetComponent<Image>().sprite = ChooseSprite();
            var item = newItem.GetComponent<Item>();
            item.SetIndex(i);
            _items[i] = item;

            _animator.ScaleAnimation(newItem.transform);

            item._onDragEvent.AddListener((item) => OnDragItem(item));           
        }
    }

    public Sprite ChooseSprite()
    {
        //add logic for check previous sprte indexes and choose according to the game logic
        return _spriteCollection.Items[Random.Range(0, _spriteCollection.Items.Length)];
    }

    public Item FindItemForSwap(Vector2 mousePosition, int index)
    {
        var itemsForCheck = GetNeighbourItems(index);
        return itemsForCheck.OrderBy(i => Vector2.Distance(mousePosition, i.transform.localPosition)).First();
    }

    public void SwapItemsInCollection(int index1, int index2)
    {
        Item temp = _items[index1];
        _items[index1] = _items[index2];
        _items[index2] = temp;
    }

    public bool CheckCondition()
    {
        return true;
    }

    public void OnDragItem(Item item)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, Input.mousePosition, Camera.main, out var localPoint);
        
        //find closest item among neighbours
        var itemForSwap = FindItemForSwap(localPoint, item.Index);

        if (CheckCondition())
        {
            int tmpIndex = item.Index;

            item.SetIndex(itemForSwap.GetComponent<Item>().Index);
            itemForSwap.GetComponent<Item>().SetIndex(tmpIndex);

            SwapItemsInCollection(item.Index, itemForSwap.Index);

            _animator.SwapSmoothly(item.transform, itemForSwap.transform);
        }
        else
        {
            _animator.SwapSmoothly(item.transform, itemForSwap.transform);
        }

    }

    private Item[] GetNeighbourItems(int index)
    {
        Item[] neighbourItems = new Item[4];

        if ((index + 1) % _size != 0)
        {
            neighbourItems[0] = _items[index + 1];
        }

        if ((index - _size) % _size != 0)
        {
            neighbourItems[1] = _items[index - 1];
        }

        if (index - _size > 0)
        {
            neighbourItems[2] = _items[index - _size];
        }

        if (index + _size < _count - 1)
        {
            neighbourItems[3] = _items[index + _size];
        }

        return neighbourItems.Where(i => i != null).ToArray();
    }
}
