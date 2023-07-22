using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class MainController : MonoBehaviour
{
    [SerializeField]
    private int _fieldSize;

    [SerializeField]
    private ItemsSpawner _itemsSpawner;
    
    [SerializeField]
    private DragController _dragController;
    
    [FormerlySerializedAs("_spritesReorderer")] [SerializeField]
    private ItemsReorderer itemsReorderer;
    
    [SerializeField]
    private MatchProvider _matchProvider;
    
    [SerializeField]
    private Animator _animator;
    
    private void Start()
    {
        ItemsProvider.Instance.Items = new Item[_fieldSize, _fieldSize];
        
        _matchProvider.Initialize(_fieldSize);
        
        itemsReorderer.Initialize(_fieldSize, 
            new UnityAction<Item>[]
            {
                _animator.DecreaseScaleAnimation
            },
            new UnityAction<Action>[]
            {
                _animator.SetOnComplete
            },
            new UnityAction<Item, Item>[]
            {
                _animator.SwapSmoothly
            },
            new UnityAction<Action>[]
            {
                _animator.SetOnComplete
            });
        
        _itemsSpawner.Initialize(_fieldSize, new UnityAction<Item>[]
            {
                _animator.IncreaseScaleAnimation
            });
        
        _dragController.Initialize(_fieldSize, 
            new UnityAction<Item, Item>[]
            {
                _animator.SwapSmoothly
            }, 
            new UnityAction<Action>[]
            {
                _animator.SetOnComplete
            },
            new UnityAction<Item, Item>[] 
            {
                _animator.SwapSmoothlyWithReturn
            });
    }
}
