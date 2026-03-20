using UnityEngine;

public class StarVFX : BaseVFX
{
    public void DisableVFX()
    {
        var emi = vfx.emission;
        emi.enabled = false;
        var main = vfx.main;
        main.loop = false;
        Invoke(nameof(Return), .5f);
    }
    private void Return()
    {
        var emi = vfx.emission;
        emi.enabled = true;
        var main = vfx.main;
        main.loop = true;
        VFXManager.Instance.ReturnObject(type, vfx);
    }
}
