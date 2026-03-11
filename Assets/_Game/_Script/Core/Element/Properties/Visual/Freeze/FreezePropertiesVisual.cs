using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FreezePropertiesVisual<T> : BasePropertiesVisual<T>
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private SpriteRenderer freezeIcon;
    protected int amount;
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
    public override void OnInit(T data)
    {
        base.OnInit(data);
    }
    protected bool IsBreak()
    {
        return amount <= 0;
    }
    protected void DeCreaseAmount()
    {
        amount--;
        ChangeTextAmount();
    }
    protected void ChangeTextAmount()
    {
        text.text = amount.ToString();
    }
    public override void OnExit()
    {
        base.OnExit();
        text.transform.DOKill();
        text.transform.DOScale(Vector3.zero, .3f).SetEase(Ease.InBack);
        freezeIcon.DOFade(0, .3f);
        var vfx= VFXManager.Instance.GetObject(EVfxType.VFX_Freeze);
        vfx.transform.SetParent(Tf);
        vfx.transform.localPosition = Vector3.zero;
        // call fx break freeze

    }
}
