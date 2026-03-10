using DG.Tweening;
using TMPro;
using UnityEngine;

public class HiddenPropertiesVisual : BasePropertiesVisual
{
    [SerializeField] private TextMeshProUGUI text;
    protected override void OnRegister()
    {
        base.OnRegister();
    }
    protected override void OnUnregister()
    {
        base.OnUnregister();
    }
    public override void OnExit()
    {
        base.OnExit();
        text.transform.DOKill();
        text.transform.DOScale(Vector3.zero, .3f).SetEase(Ease.InBack);
    }
}
