using UnityEngine;
using DG.Tweening;

public class CoinFly : BLBMono
{
    public RectTransform rect;

    public float appearScale = 1.2f;
    public float appearDuration = 0.25f;

    public float delayBeforeFly = 0.15f;
    public float flyDuration = 0.6f;

    public void Play(RectTransform target, System.Action onDone)
    {
        rect.localScale = Vector3.zero;
        rect.DOScale(appearScale, appearDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(delayBeforeFly, () =>
                {
                    rect.DOMove(target.position, flyDuration)
                        .SetEase(Ease.InQuad)
                        .OnComplete(() =>
                        {
                            onDone?.Invoke();
                            Destroy(gameObject);
                        });
                });
            });
    }
}
