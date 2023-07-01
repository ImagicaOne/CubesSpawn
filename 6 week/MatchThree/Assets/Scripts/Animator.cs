using DG.Tweening;
using UnityEngine;

public class Animator : MonoBehaviour
{
    public void ScaleAnimation(Transform transform)
    {
        transform.DOScale(new Vector2(1, 1), 1).SetEase(Ease.InSine);
    }

    public void SwapSmoothlyWithReturn(Transform item1, Transform item2)
    {
        var sequence = DOTween.Sequence();

        sequence.Append(item1.DOMove(item2.position, 1).SetEase(Ease.InSine));
        sequence.Join(item2.DOMove(item1.position, 1).SetEase(Ease.InSine));
        sequence.SetLoops(2, LoopType.Yoyo);
    }

    public void SwapSmoothly(Transform item1, Transform item2)
    {
        var sequence = DOTween.Sequence();

        sequence.Append(item1.DOMove(item2.position, 1).SetEase(Ease.InSine));
        sequence.Join(item2.DOMove(item1.position, 1).SetEase(Ease.InSine));

    }
}
