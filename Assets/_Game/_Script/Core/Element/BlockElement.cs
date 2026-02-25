using UnityEngine;

public class BlockElement : BaseElement<BlockElementVisual,BlockData>
{
    protected Vector2Int matrix;
    public Vector2Int Matrix => matrix;
    public void SetMatrix(Vector2Int matrix)
    {
        this.matrix = matrix;
    }
    public override void SetBusy(bool isBusy)
    {
        base.SetBusy(isBusy);
        visual.SetBusy(isBusy);
    }
    public void NextMatrix(Vector2Int matrix,Vector3 pos)
    {
        SetMatrix(matrix);
        visual.MoveNextMatrix(pos);
    }
}
