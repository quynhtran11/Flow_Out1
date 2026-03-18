using UnityEngine;

public class VfxBubleLoopFill : BaseVFX
{
    protected override void DelayDeactive()
    {
        float t = GameData.Instance.GetSpeedWaterFill();
        Invoke(nameof(DisableVFX), t *2f);
    }
    private void DisableVFX()
    {
        var emis = vfx.emission;
        emis.enabled = false;
        Invoke(nameof(Return), 1f);
    }
    private void Return()
    {
        var emis = vfx.emission;
        emis.enabled = true;
        VFXManager.Instance.ReturnObject(type, vfx);
    }
}
