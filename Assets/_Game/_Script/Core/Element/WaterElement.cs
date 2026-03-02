using DG.Tweening;
using System;
using UnityEngine;

public class WaterElement : BaseElement<WaterElementVisual, WaterData>
{

    public void WaterFill(CupElement cup)
    {
        Tf.SetParent(cup.Tf);
        cup.WaterFill();
        //cup.StopCheck();
                 cup.CheckClearCup();
        Tf.DOLocalMove(Vector3.zero, 0.5f).OnComplete(() =>
             {
                 // test
             });
    }
}
