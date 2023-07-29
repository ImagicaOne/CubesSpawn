using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField]
    private int _fieldSize;

    [SerializeField]
    private ItemsSpawner _itemsSpawner;
    
    [SerializeField]
    private DragController _dragController;
    
    [SerializeField]
    private ItemsReorderer _itemsReorderer;
    
    [SerializeField]
    private MatchProvider _matchProvider;
    
    private void Start()
    {
        ItemsProvider.Instance.Items = new Item[_fieldSize, _fieldSize];
        
        _matchProvider.Initialize(_fieldSize);
        
        _itemsReorderer.Initialize(_fieldSize);
        
        _itemsSpawner.Initialize(_fieldSize);
        
        _dragController.Initialize(_fieldSize);
    }
}
