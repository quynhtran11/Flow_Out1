using UnityEngine;

public class CupHiddenPropertiesVisual : HiddenPropertiesVisual<CupElement>
{
    protected override void OnRegister()
    {
        base.OnRegister();
        EventDispatcher.RegisterEvent<CupQualifiedInteractEvent>(OnCupQualifiedInteract);
    }
    protected override void OnUnregister()
    {
        base.OnUnregister();
        EventDispatcher.RemoveEvent<CupQualifiedInteractEvent>(OnCupQualifiedInteract);
    }

    protected void OnCupQualifiedInteract(CupQualifiedInteractEvent param)
    {
        if (isBusy || !data.Data.hiddenData.isHidden ||
!param.cup.Data.hiddenData.isHidden ||
param.cup != data) return;
        OnExit();
        data.StopHidden();
    }
}