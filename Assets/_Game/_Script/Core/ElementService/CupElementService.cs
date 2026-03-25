using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CupElementService : BaseElementService<CupElement>
{
    private CupElement[,] maxtrix;
    private Vector3[,] centerPos;
    private Vector2Int lenghtMatrix;
    public CupElementService()
    {
        allElements.Clear();
        RegisterEvent();
    }
    protected override void RegisterEvent()
    {
        EventDispatcher.RemoveEvent<TouchSuccessCupEvent>(OnTouchSuccessCup);
        EventDispatcher.RemoveEvent<UseShuffleEvent>(OnShuffle);
        EventDispatcher.RemoveEvent<TouchFailedCupEvent>(OnTouchFailCup);
        EventDispatcher.RemoveEvent<PrePickUpEvent>(OnPrePickUp);
        EventDispatcher.RemoveEvent<UsePickUpEvent>(OnUsePickUp);
        EventDispatcher.RegisterEvent<PrePickUpEvent>(OnPrePickUp);
        EventDispatcher.RegisterEvent<TouchSuccessCupEvent>(OnTouchSuccessCup);
        EventDispatcher.RegisterEvent<TouchFailedCupEvent>(OnTouchFailCup);
        EventDispatcher.RegisterEvent<UseShuffleEvent>(OnShuffle);
        EventDispatcher.RegisterEvent<UsePickUpEvent>(OnUsePickUp);

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
    private void OnShuffle(UseShuffleEvent param)
    {
        if (allElements == null || allElements.Count <= 0) return;
        List<EColorType> colorCups  = new List<EColorType>();
        List<WaterElement> waters = new List<WaterElement>();
        for (int i = 0; i < allElements.Count; i++)
        {
            if (allElements[i].HasProperties()) continue;
            colorCups.Add(allElements[i].Color);
        }
        EventDispatcher.Dispatch(new GetShuffleObjectEvent()
        {
            callBack = (x) =>
            {
                waters = x;
            }
        });


        for (int i = 0; i < allElements.Count; i++) // test case random 
        {
            if (allElements[i] == null) continue;
            int rand = Random.Range(0, colorCups.Count);
            allElements[i].Shuffle(colorCups[rand]);
            colorCups.RemoveAt(rand);
        }
        Debug.LogError("coloir_" + colorCups.Count);
        Debug.LogError("waters_" + waters.Count);

    }
    private void OnUsePickUp(UsePickUpEvent param)
    {
        if (param.cup == null || !allElements.Contains(param.cup)) return;
        for (int i = 0; i < allElements.Count; i++)
        {
            if (allElements[i] == param.cup) continue;
            allElements[i].PopSorting();
        }
        param.cup.SetBusy(false);
        CalculatorMatrix(param.cup);
        allElements.Remove(param.cup);
    }
    private void OnPrePickUp(PrePickUpEvent param)
    {
        if (allElements == null || allElements.Count <= 0) return;
        for (int i = 0; i < allElements.Count; i++)
        {
            allElements[i].PushSorting();
        }
    }
    private void CalculatorMatrix(CupElement cup)
    {
        if (cup == null) return; 
        int row = cup.Matrix.x;
        maxtrix[cup.Matrix.x, cup.Matrix.y] = null;
        cup.OutMatrix(); // test
        Queue<CupElement> queueBlocks = new Queue<CupElement>();
        for (int i = 0; i < lenghtMatrix.y; i++)
        {
            var value = maxtrix[row, i];
            if (value == null) continue;
            queueBlocks.Enqueue(value);
        }
        float t =0;
        for (int i = 0; i < lenghtMatrix.y; i++)
        {
            if (queueBlocks == null || queueBlocks.Count <= 0)
            {
                maxtrix[row, i] = null;
            }
            else
            {
                var value = queueBlocks.Dequeue();
                if (cup.Matrix.y > i) continue;
                t += .05f;
                if(value ==null || value.Matrix.y<=0) continue;
                Vector2Int newMatrix = new Vector2Int(value.Matrix.x, value.Matrix.y - 1);
                value.NextMatrix(newMatrix,centerPos[newMatrix.x, newMatrix.y],t);
                maxtrix[row, i] = value;
            }
        }
        var valueFinal = maxtrix[row, 0];
        if (valueFinal == null) return;
        valueFinal.SetBusy(false);
        EventDispatcher.Dispatch(new CupQualifiedInteractEvent()
        {
            cup = valueFinal
        });
    }
    public override void InitElement(LevelInfor level)
    {
        maxtrix = new CupElement[level.Map.x, level.Map.y];
        lenghtMatrix = new Vector2Int(level.Map.x, level.Map.y);
        centerPos = new Vector3[level.Map.x, level.Map.y];
        Queue<CupElement> stack = new Queue<CupElement>();
        allElements.Sort((a, b) =>
        {
            int compareY = b.Data.pos.y.CompareTo(a.Data.pos.y); 
            if (compareY != 0) return compareY;

            return a.Data.pos.x.CompareTo(b.Data.pos.x); 
        });
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
