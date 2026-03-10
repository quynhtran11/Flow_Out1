using UnityEngine;

public class WaterHiddenPropertiesVisual : HiddenPropertiesVisual<WaterElement>
{
    protected override void OnClearSuccessWater(ClearSuccessWaterEvent param)
    {
        base.OnClearSuccessWater(param);
        if (!data.Data.hiddenData.isHidden || 
            !param.water.Data.hiddenData.isHidden ||
            param.water != data ) return;
        OnExit();
        data.StopHidden();
        Debug.LogError("break");
    }
}
