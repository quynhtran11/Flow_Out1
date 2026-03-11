using UnityEngine;
public class CupElement : BaseElement<CupElementVisual, CupData>
{
    protected EColorType color;
    protected Vector2Int matrix;
    public Vector2Int Matrix => matrix;
    private bool isCheck = false;
    public bool IsCheck => isCheck;
    private int currentWater = 0;
    private int maxWater = 0;
    public int CurrentWater => currentWater;
    public EColorType Color => color;
    public override void OnInit()
    {
        base.OnInit();
        color = data.color;
        maxWater = data.amount;
    }
    protected override void SetUpProperties()
    {
        base.SetUpProperties();
        allPros = PropertisFactory.GetCupProperties(data,this);
    }
    public void SetMatrix(Vector2Int matrix)
    {
        this.matrix = matrix;
    }
    public override void SetBusy(bool isBusy)
    {
        base.SetBusy(isBusy);
        visual.SetBusy(isBusy);
    }
    public void NextMatrix(Vector2Int matrix, Vector3 pos)
    {
        SetMatrix(matrix);
        visual.MoveNextMatrix(pos);
    }
    public void OutMatrix()
    {
        visual.OutMatrix(); // test
        EventDispatcher.Dispatch(new CupToConveyorEvent()
        {
            cup = this
        });
    }
    public void MoveToConveyor(Vector3 pos)
    {
        
        visual.MoveToConveyor(pos, () =>
        {
            isCheck = true;
        });
    }
    public void MoveFailed()
    {
        visual.MoveFailed();
    }
    public void StopCheck()
    {
        isCheck = false;
    }
    public void WaterFill(bool isRevive = false)
    {
        currentWater++;
        visual.WaterFill();
        if (currentWater >= maxWater || isRevive )
        {
            isBusy = true;
            CheckClearCup();
        }
        else
        {
        EventDispatcher.Dispatch(new CheckLoseEvent() { });
        }
    }
    public int RemainingWater()
    {
        int remain = maxWater - currentWater;
        return remain;
    }
    private void CheckClearCup()
    {
        if (currentWater < maxWater) return;
        EventDispatcher.Dispatch(new ClearCupEvent()
        {
            cup = visual
        });
    }
    public void ToggleCup(EColorType type,int max)
    {
        this.color = type;
        this.maxWater = max;
        visual.Toggle(color, maxWater);
    }
    public void StartHidden()
    {
        visual.StartHidden();
    }
    public void StopHidden()
    {
        visual.StopHidden();
    }
}