using DG.Tweening;
using UnityEngine;

public class Animator : MonoBehaviour
{
    public void ScaleAnimation(Transform transform)
    {
        transform.DOScale(new Vector2(1, 1), 1).SetEase(Ease.InSine);
    }

    public void SwapSmoothly(Transform item1, Transform item2)
    {
        var sequence = DOTween.Sequence();

        sequence.Append(item1.DOMove(item2.position, 1).SetEase(Ease.InSine));
        sequence.Join(item2.DOMove(item1.position, 1).SetEase(Ease.InSine));

        //Why YoYo doesnt work in the following cases?

        //I tried to do so
        //sequence.Append(transform.DOMove(itemForSwap.transform.position, 1).SetEase(Ease.InSine).SetLoops(1, LoopType.Yoyo));
        //sequence.Join(itemForSwap.transform.DOMove(_initialPosition, 1).SetEase(Ease.InSine).SetLoops(1, LoopType.Yoyo));

        //and so
        //sequence.Append(transform.DOMove(itemForSwap.transform.position, 1).SetEase(Ease.InSine));
        //sequence.Join(itemForSwap.transform.DOMove(_initialPosition, 1).SetEase(Ease.InSine));
        //sequence.SetLoops(1, LoopType.Yoyo);
    }
}
