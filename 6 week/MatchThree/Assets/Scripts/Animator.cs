using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Animator : MonoBehaviour
{
    private Sequence _sequence;

    public void CreateSequence()
    {
        _sequence = DOTween.Sequence();
    }
    
    public void SetOnComplete(Action action)
    {
        _sequence.OnComplete(() => action.Invoke());
    }
    
    public void IncreaseScaleAnimation(Item item)
    {
        if (_sequence is null)
        {
            CreateSequence();
        }
        
        _sequence.Join(item.transform.DOScale(new Vector2(1, 1), 1).SetEase(Ease.InSine));
    }
    
    public void DecreaseScaleAnimation(Item item)
    {
        CreateSequence();

        _sequence.Join(item.transform.DOScale(new Vector2(0, 0), 1).SetEase(Ease.InSine));
    }

    public void SwapSmoothlyWithReturn(Item item1, Item item2)
    {
        CreateSequence();
        
        _sequence.Append(item1.transform.DOMove(item2.transform.position, 1).SetEase(Ease.InSine));
        _sequence.Join(item2.transform.DOMove(item1.transform.position, 1).SetEase(Ease.InSine));
        _sequence.SetLoops(2, LoopType.Yoyo);
    }

    public void SwapSmoothly(Item item1, Item item2)
    {
        CreateSequence();
            
        _sequence.Join(item1.transform.DOMove(item2.transform.position, 1).SetEase(Ease.InSine));
        _sequence.Join(item2.transform.DOMove(item1.transform.position, 1).SetEase(Ease.InSine));
    }
}
