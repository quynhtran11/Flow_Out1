using DG.Tweening;
using TMPro;
using UnityEngine;

public class HiddenPropertiesVisual<T> : BasePropertiesVisual<T>
{
    [SerializeField] private TextMeshProUGUI text;
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
    }
    public override void OnExit()
    {
        text.transform.DOKill();
        text.transform.DOScale(Vector3.zero, .3f).SetEase(Ease.InBack);
    }
}
