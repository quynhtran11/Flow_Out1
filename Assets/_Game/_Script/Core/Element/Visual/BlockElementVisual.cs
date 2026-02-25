using DG.Tweening;
using UnityEngine;

public class BlockElementVisual : BaseElementVisual<BlockData>
{
    private void ActiveInteract(bool isBusy)
    {
        if (isBusy) return;
        float delay = (float)data.id * .05f;
        skin.DOKill();
        Vector3 scaleInit = new Vector3(.975f, .95f, 1f);
        Vector3 scaleAfter = new Vector3(1.025f, 1.05f, 1f);
        skin.transform.localScale = scaleInit;
        skin.DOScaleX(scaleAfter.x, 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(delay);
        skin.DOScaleY(scaleAfter.y, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(delay);
    }
    public override void AfterInit()
    {
        Tf.DOKill();
        Tf.position = new Vector3(Tf.position.x, Tf.position.y, Tf.position.z - 10);
        float delay = (float)data.id * .05f;
        Tf.DOMove(centerPos, .5f).SetEase(Ease.OutBack, .4f).SetDelay(delay);
    }
    public override void SetBusy(bool isBusy)
    {
        base.SetBusy(isBusy);
        ActiveInteract(isBusy);
    }
    public void MoveNextMatrix(Vector3 pos)
    {
        Tf.DOKill();
        Tf.DOMove(pos, .5f);
    }
}
