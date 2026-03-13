using UnityEngine;

public class WaterElement : BaseElement<WaterElementVisual, WaterData>
{
    private bool pauseFill = false;
    protected override void SetUpProperties()
    {
        base.SetUpProperties();
        allPros = PropertisFactory.GetWaterProperties(data);
    }
    public void RegisterTarget(Transform tf,bool isLast)
    {
        visual.RegisterTarget(tf,isLast);
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
    public void StopHidden()
    {
        visual.ShowColor();
        visual.ShowText();
    }
    public void StartFreeze()
    {
        pauseFill = true;
    }
    public void StopFreeze()
    {
        pauseFill = false;
    }
    public bool IsPaused() {
        return pauseFill;
    }
}
