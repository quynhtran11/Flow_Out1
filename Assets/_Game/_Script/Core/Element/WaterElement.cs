using UnityEngine;

public class WaterElement : BaseElement<WaterElementVisual, WaterData>
{
    protected override void SetUpProperties()
    {
        base.SetUpProperties();
        allPros = PropertisFactory.GetWaterProperties(data);
    }
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
    public void StartHidden()
    {
        visual.StartHidden();
    }
}
