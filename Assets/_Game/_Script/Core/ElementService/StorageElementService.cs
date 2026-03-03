using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class StorageElementService : BaseElementService<StorageElement>
{
    protected override void RegisterEvent()
    {
        EventDispatcher.RemoveEvent<CheckFillWaterEvent>(OnCheckFillWater);
        EventDispatcher.RemoveEvent<CheckAllQualifiedFillEvent>(OnCheckAllQualifiedFill);
        EventDispatcher.RegisterEvent<CheckFillWaterEvent>(OnCheckFillWater);
        EventDispatcher.RegisterEvent<CheckAllQualifiedFillEvent>(OnCheckAllQualifiedFill);
    }
    private void OnCheckFillWater(CheckFillWaterEvent param)
    {
        if (allElements == null || allElements.Count <= 0) return;
        for (int i = 0; i < allElements.Count; i++)
        {
            if (allElements[i] == null) continue;
            if (Mathf.Abs( param.pos.x - allElements[i].Tf.position.x) > .5f) continue;
            param.callBack.Invoke(allElements[i].GetWaterFill(param.color));
        }
    }
    private void OnCheckAllQualifiedFill(CheckAllQualifiedFillEvent param)
    {
        if (allElements == null || allElements.Count <= 0) return;
        List<EColorType> colors = new List<EColorType>();
        for (int i = 0; i < allElements.Count; i++)
        {
            if (allElements[i].IsBusy || allElements[i].FirstWater() == null) continue;
            colors.Add(allElements[i].FirstWater().Data.color);
        }
        if (colors.Count <= 0) return;
        bool isLose= true;
        for (int i = 0; i < param.cups.Count; i++)
        {
            for (int j = 0; j < colors.Count; j++)
            {
                if (param.cups[i].Data.color == colors[j]) {
                    isLose = false; break;
                }
            }
            if (!isLose) break;
        }
        if (!isLose) return;
        EventDispatcher.Dispatch(new EndGameEvent()
        {
            isWin = false,
            loseType = ELoseType.OutOfSpace
        });

    }
    public override void InitElement(LevelInfor level)
    {
        foreach (var storage in allElements)
        {
            storage.OnInit();
        }
    }
}
