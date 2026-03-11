using UnityEngine;
public class WaterHiddenPropertiesVisual : HiddenPropertiesVisual<WaterElement>
{
    protected override void OnRegister()
    {
        base.OnRegister();
        EventDispatcher.RegisterEvent<ClearSuccessWaterEvent>(OnClearSuccessWater);
    }
    protected override void OnUnregister()
    {
        base.OnUnregister();
        EventDispatcher.RemoveEvent<ClearSuccessWaterEvent>(OnClearSuccessWater);
    }
    protected virtual void OnClearSuccessWater(ClearSuccessWaterEvent param)
    {
        if (isBusy || !data.Data.hiddenData.isHidden ||
    !param.water.Data.hiddenData.isHidden ||
    param.water != data) return;
        OnExit();
        data.StopHidden();
        Debug.LogError("break");
    }
}
