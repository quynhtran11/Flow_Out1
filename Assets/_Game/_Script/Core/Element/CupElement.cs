using UnityEngine;
public class CupElement : BaseElement<CupElementVisual, CupData>
{
    protected Vector2Int matrix;
    public Vector2Int Matrix => matrix;
    private bool isCheck = false;
    public bool IsCheck => isCheck;
    private int currentWater = 0;
    public int CurrentWater => currentWater;
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
    public void WaterFill()
    {
        currentWater++;
        visual.WaterFill();
        if (currentWater >= Data.amount)
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
        int remain = data.amount - currentWater;
        return remain;
    }
    private void CheckClearCup()
    {
        if (currentWater < Data.amount) return;
        EventDispatcher.Dispatch(new ClearCupEvent()
        {
            cup = visual
        });
    }
}