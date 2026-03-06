using DG.Tweening;
using UnityEngine;

public class ConveyorSlotElementVisual : BLBMono
{
    public void WarningConveyor()
    {
        Debug.LogError("warning");
    }
    public void OnInit(float delay)
    {
        Tf.localScale = Vector3.zero;
        Tf.DOScale(Vector3.one, .3f).SetDelay(delay).SetEase(Ease.OutBack);
    }
}
