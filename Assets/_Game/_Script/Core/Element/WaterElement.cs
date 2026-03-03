using DG.Tweening;
using System;
using UnityEngine;

public class WaterElement : BaseElement<WaterElementVisual, WaterData>
{
    public void RegisterTarget(Transform tf)
    {
        visual.RegisterTarget(tf);
    }
    public void WaterFill()
    {
        visual.WaterFill();
    }
    public void ChangeSpeedWater()
    {
        visual.ChangeSpeedWater();
    }
}
