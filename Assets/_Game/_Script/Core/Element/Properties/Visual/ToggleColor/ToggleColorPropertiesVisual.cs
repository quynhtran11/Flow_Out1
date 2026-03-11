using DG.Tweening;
using UnityEngine;

public class ToggleColorPropertiesVisual<T> : BasePropertiesVisual<T>
{
    [SerializeField] protected SpriteRenderer icon;
    protected EColorType color1;
    protected EColorType color2;
    protected int count1;
    protected int count2;
    protected T dataOposite;
    protected bool isChange = false;
    protected bool isPause = false;
    public override void OnInit(T data)
    {
        base.OnInit(data);
        isChange = false;
    }
    protected override void OnRegister()
    {
        base.OnRegister();
        EventDispatcher.RegisterEvent<CupToConveyorSuccessEvent>(OnCupToConveyorSuccess);
    }
    protected override void OnUnregister()
    {
        base.OnUnregister();
        EventDispatcher.RemoveEvent<CupToConveyorSuccessEvent>(OnCupToConveyorSuccess);
    }
    protected virtual void OnCupToConveyorSuccess(CupToConveyorSuccessEvent param)
    {
    }
    protected virtual void ChangeColorObject(EColorType type, int count)
    {

    }
    protected void ChangeColor(EColorType type)
    {
        icon.color = GameData.Instance.ColorData.GetData(type).color;
    }
    protected void CalculatorPosition()
    {
        if (isChange)
        {
            ChangeColor(color2);
            ChangeColorObject(color1,count1);
        }
        else
        {
            ChangeColor(color1);
            ChangeColorObject(color2,count2);
        }
        isChange = !isChange;
    }
    protected virtual void BreakToggle()
    {
        isPause = true;
        icon.transform.DOKill();
        icon.DOFade(0, .5f);
    }
    public virtual void AddOposite(T data)
    {
        this.dataOposite = data;
    }
}
