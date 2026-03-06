using DG.Tweening;
using UnityEngine;

public class AddConveyorSlotButton : BLBMono
{
    private void OnMouseDown()
    {
        Debug.LogError("down");
        Tf.DOKill();
        Tf.transform.localScale = Vector3.one;
        Tf.DOPunchScale(Vector3.one * .25f, .25f,5);
        // check dieu kien mua slot 
        EventDispatcher.Dispatch(new AddSlotEvent() { });
    }
}
