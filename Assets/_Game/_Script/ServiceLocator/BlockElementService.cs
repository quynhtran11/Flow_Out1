using System.Collections.Generic;
using UnityEngine;

public class BlockElementService : BaseElementService<BlockElement>
{
    private BlockElement[,] maxtrix;
    private Vector3[,] centerPos;
    private Vector2Int lenghtMatrix;
    protected override void RegisterEvent()
    {
        EventDispatcher.RemoveEvent<TouchSuccessBlockEvent>(OnTouchSuccessBlock);
        EventDispatcher.RegisterEvent<TouchSuccessBlockEvent>(OnTouchSuccessBlock);
    }
    private void OnTouchSuccessBlock(TouchSuccessBlockEvent param)
    {
        Debug.LogError("1fsaf");
        if (allElements == null || allElements.Count <= 0
            || !allElements.Contains(param.block) || param.block.Matrix.y > 0) return;
        Debug.LogError("2fsaf");
        CalculatorMatrix(param.block);

        allElements.Remove(param.block);
    }
    private void CalculatorMatrix(BlockElement block)
    {
        int row = block.Matrix.x;
        maxtrix[block.Matrix.x, block.Matrix.y] = null;
        block.gameObject.SetActive(false); // test
        Queue<BlockElement> queueBlocks = new Queue<BlockElement>();
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
        maxtrix = new BlockElement[level.Map.x, level.Map.y];
        lenghtMatrix = new Vector2Int(level.Map.x, level.Map.y);
        centerPos = new Vector3[level.Map.x, level.Map.y];
        Queue<BlockElement> stack = new Queue<BlockElement>();
        for (int i = 0; i < allElements.Count; i++)
        {
            stack.Enqueue(allElements[i]);
        }
        for (int j = 0; j < level.Map.y; j++)
        {
            for (int i = 0; i < level.Map.x; i++)
            {
                BlockElement block = stack.Dequeue();
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
