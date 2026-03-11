using UnityEngine;

public class CupToggleColorPropertiesVisual : ToggleColorPropertiesVisual<CupElement>
{
    public override void OnInit(CupElement data)
    {
        base.OnInit(data);
        color1 = this.data.Data.color;
        color2 = this.data.Data.toggleColorData.colorType;
        count1 = this.data.Data.amount;
        ChangeColor(color2);
    }
    public override void AddOposite(CupElement data)
    {
        base.AddOposite(data);
        count2 = data.Data.amount;
    }
    protected override void OnCupToConveyorSuccess(CupToConveyorSuccessEvent param)
    {
        base.OnCupToConveyorSuccess(param);
        // todo
        if (param.cup == data || param.cup == dataOposite) {
            BreakToggle();
            return;
        }
        if (isPause) return;
        CalculatorPosition();
    }
    protected override void ChangeColorObject(EColorType type,int count)
    {
        if (this.data == null) return;
        this.data.ToggleCup(type,count);
    }
}