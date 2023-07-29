using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ItemsSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _startPosition;
    
    [SerializeField]
    private GameObject _tilePrefab;
    
    [SerializeField]
    private Item _itemPrefab;
    
    [SerializeField]
    private int _spaceInterval;
    
    [SerializeField]
    private ItemSpriteProvider _itemSpriteProvider;
    
    [SerializeField]
    private MatchProvider _matchProvider;
    
    [SerializeField]
    private AnimationController _animationController;

    private int _fieldSize;
    
    public void Initialize(int fieldSize)
    {
        _fieldSize = fieldSize;

        SpawnGrid();
        SetSprites();
    }
    
    public void SpawnGrid()
    {
        for (int row = 0; row < _fieldSize; row++)
        {
            for (int column = 0; column < _fieldSize; column++)
            {
                Vector3 pos = new Vector3(column * _spaceInterval, -row * _spaceInterval, 0) + _startPosition.position; 
                
                // spawn Tile
                var tile = Instantiate(_tilePrefab, pos, Quaternion.identity);
                
                //spawn Item under the Tile
                var item = Instantiate(_itemPrefab, tile.transform);
                
                //add Item to array
                ItemsProvider.Instance.Items[row, column] = item;
                
                //let Item know her Index in array
                item.SetIndex(row, column);
            }
        }
    }

    public void SetSprites()
    {
        for (int row = 0; row < _fieldSize; row++)
        {
            for (int column = 0; column < _fieldSize; column++)
            {
                var item = ItemsProvider.Instance.Items[row, column];
                if (item.GetSprite() is null)
                {
                    do
                    {
                        item.SetSprite(_itemSpriteProvider.GetRandomSprite());
                    }
                    while (_matchProvider.GetAllMatches().Length > 0);
                }
            }
        }
        
        _animationController.IncreaseScaleAnimation(ItemsProvider.Instance.Items.Cast<Item>().ToArray());
    }
}
