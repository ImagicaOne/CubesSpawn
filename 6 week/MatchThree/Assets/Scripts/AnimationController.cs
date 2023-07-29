using System;
using DG.Tweening;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public void IncreaseScaleAnimation(Item[] items)
    {
        var sequence = DOTween.Sequence();

        foreach (var item in items)
        {
            sequence.Join(item.transform.DOScale(new Vector2(1, 1), 1).SetEase(Ease.InSine));
        }
    }
    
    public Sequence DecreaseScaleAnimation(Item[] items)
    {
        var sequence = DOTween.Sequence();
        
        foreach (var item in items)
        {
            sequence.Join(item.transform.DOScale(new Vector2(0, 0), 1).SetEase(Ease.InSine));
        }

        return sequence;
    }

    public void SwapSmoothlyWithReturn(Item item1, Item item2)
    {
        var sequence = DOTween.Sequence();
        
        sequence.Append(item1.transform.DOMove(item2.transform.position, 1).SetEase(Ease.InSine));
        sequence.Join(item2.transform.DOMove(item1.transform.position, 1).SetEase(Ease.InSine));
        sequence.SetLoops(2, LoopType.Yoyo);
    }

    public void SwapSmoothly(Item item1, Item item2, Action onCompleteAction)
    {
        var sequence = DOTween.Sequence();
            
        sequence.Join(item1.transform.DOMove(item2.transform.position, 1).SetEase(Ease.InSine));
        sequence.Join(item2.transform.DOMove(item1.transform.position, 1).SetEase(Ease.InSine));
        sequence.OnComplete(onCompleteAction.Invoke);
    }
    
    public void MoveSmoothly((Item, Vector3)[] itemsPositions, Action onCompleteAction)
    {
        var sequence = DOTween.Sequence();
        
        foreach (var item in itemsPositions)
        {
            var (a, b) = item;
            sequence.Join(a.transform.DOMove(b, 0.5f).SetEase(Ease.InSine));
        }
        
        sequence.OnComplete(onCompleteAction.Invoke);
    }
}
