using UnityEngine;

public class WaterFreezePropertiesVisual : FreezePropertiesVisual<WaterElement>
{
    public override void OnInit(WaterElement data)
    {
        base.OnInit(data);
        this.amount = data.Data.freezeData.amount;
        ChangeTextAmount();
    }
    protected override void OnClearSuccessWater(ClearSuccessWaterEvent param)
    {
        if (isBusy) return;
        if (data.Data.freezeData.amount <= 0) return;
        DeCreaseAmount();
        if (!IsBreak()) return;
        data.StopFreeze();
        OnExit();
    }
}
