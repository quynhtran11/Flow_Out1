using System.Collections.Generic;
using UnityEngine;
public class CupElementService : BaseElementService<CupElement>
{
    private CupElement[,] maxtrix;
    private Vector3[,] centerPos;
    private Vector2Int lenghtMatrix;
    protected override void RegisterEvent()
    {
        EventDispatcher.RemoveEvent<TouchSuccessCupEvent>(OnTouchSuccessCup);
        EventDispatcher.RegisterEvent<TouchSuccessCupEvent>(OnTouchSuccessCup);
        EventDispatcher.RemoveEvent<TouchFailedCupEvent>(OnTouchFailCup);
        EventDispatcher.RegisterEvent<TouchFailedCupEvent>(OnTouchFailCup);
    }
    private void OnTouchSuccessCup(TouchSuccessCupEvent param)
    {
        if (allElements == null || allElements.Count <= 0
            || !allElements.Contains(param.cup) || param.cup.Matrix.y > 0) return;
        bool isFull = false;
        //// check connect
        //List<BlockElement> blocks = new List<BlockElement>();
        //blocks.Add(param.block);
        //for (int i = 0; i < allElements.Count; i++)
        //{
        //    if (allElements[i].Matrix.x-1 == param.block.Matrix.x &&
        //        allElements[i].Matrix.y == param.block.Matrix.y)
        //    {
        //        blocks.Add(allElements[i]);
        //        break;
        //    }
        //}
        //Debug.LogError("value_" + blocks.Count);
        //for (int i = 0; i < blocks.Count; i++)
        //{
        //    CalculatorMatrix(blocks[i]);
        //    allElements.Remove(blocks[i]);
        //}
        CalculatorMatrix(param.cup);
        allElements.Remove(param.cup);
    }
    private void OnTouchFailCup(TouchFailedCupEvent param)
    {
        param.cup.MoveFailed();
    }
    private void CalculatorMatrix(CupElement block)
    {
        int row = block.Matrix.x;
        maxtrix[block.Matrix.x, block.Matrix.y] = null;
        block.OutMatrix(); // test
        Queue<CupElement> queueBlocks = new Queue<CupElement>();
        for (int i = 0; i < lenghtMatrix.y; i++)
        {
            var value = maxtrix[row, i];
            if (value == null) continue;
            queueBlocks.Enqueue(value);
        }
        for (int i = 0; i < lenghtMatrix.y; i++)
        {
            if (queueBlocks == null || queueBlocks.Count <= 0)
            {
                maxtrix[row, i] = null;
            }
            else
            {
                var value = queueBlocks.Dequeue();
                Vector2Int newMatrix = new Vector2Int(value.Matrix.x, value.Matrix.y - 1);
                value.NextMatrix(newMatrix,
                    centerPos[newMatrix.x, newMatrix.y]);
                maxtrix[row, i] = value;
            }
        }
        var valueFinal = maxtrix[row, 0];
        if (valueFinal == null) return;
        valueFinal.SetBusy(false);

    }
    public override void InitElement(LevelInfor level)
    {
        maxtrix = new CupElement[level.Map.x, level.Map.y];
        lenghtMatrix = new Vector2Int(level.Map.x, level.Map.y);
        centerPos = new Vector3[level.Map.x, level.Map.y];
        Queue<CupElement> stack = new Queue<CupElement>();
        for (int i = 0; i < allElements.Count; i++)
        {
            stack.Enqueue(allElements[i]);
        }
        for (int j = 0; j < level.Map.y; j++)
        {
            for (int i = 0; i < level.Map.x; i++)
            {
                CupElement block = stack.Dequeue();
                block.SetMatrix(new Vector2Int(i, j));
                maxtrix[i, j] = block;
            }
        }
        foreach (var value in maxtrix)
        {
            value.OnInit();
            bool isBusy = value.Matrix.y > 0;
            value.SetBusy(isBusy);
            centerPos[value.Matrix.x, value.Matrix.y] = value.Visual.CenterPos;
        }
    }
}
