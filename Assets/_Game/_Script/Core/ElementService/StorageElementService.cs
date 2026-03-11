using System.Collections.Generic;
using UnityEngine;
public class StorageElementService : BaseElementService<StorageElement>
{
    protected override void RegisterEvent()
    {
        EventDispatcher.RemoveEvent<CheckFillWaterEvent>(OnCheckFillWater);
        EventDispatcher.RemoveEvent<CheckAllQualifiedFillEvent>(OnCheckAllQualifiedFill);
        EventDispatcher.RemoveEvent<ReviveStorageEvent>(OnReviveStorage);
        EventDispatcher.RemoveEvent<IncreaseSpeedWaterEvent>(OnIncreaseSpeedWater);
        EventDispatcher.RegisterEvent<CheckFillWaterEvent>(OnCheckFillWater);
        EventDispatcher.RegisterEvent<CheckAllQualifiedFillEvent>(OnCheckAllQualifiedFill);
        EventDispatcher.RegisterEvent<ReviveStorageEvent>(OnReviveStorage);
        EventDispatcher.RegisterEvent<IncreaseSpeedWaterEvent>(OnIncreaseSpeedWater);
    }
    private void OnCheckFillWater(CheckFillWaterEvent param)
    {
        if (allElements == null || allElements.Count <= 0) return;
        for (int i = 0; i < allElements.Count; i++)
        {
            if (allElements[i] == null) continue;
            if (param.pos.x > allElements[i].Tf.position.x) continue;
            if (Mathf.Abs( param.pos.x - allElements[i].Tf.position.x) > GameData.Instance.DistanceCheckFill) continue;
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
                if (param.cups[i].Color == colors[j]) {
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
    public void OnReviveStorage(ReviveStorageEvent param)
    {
        if (param.cup == null) return;
        int remainWater = param.cup.RemainingWater();
        List<WaterElement> allWaters = new List<WaterElement>();
        for (int i = 0; i < allElements.Count; i++)
        {
            if (allElements[i].IsBusy) continue;
            for (int j = 0; j < allElements[i].AllWater().Count; j++)
            {
                var value = allElements[i].AllWater()[j];
                if(value == null ) continue;
                if (allElements[i].AllWater()[j].IsBusy) continue;
                if(value.Data.color == param.cup.Color)
                {
                    allWaters.Add(value);
                }
            }
        }
        allWaters.Sort((a, b) =>a.transform.position.y.CompareTo(b.transform.position.y));
        if (allWaters.Count < remainWater) return;
        for (int i = 0; i < remainWater; i++)
        {
            var value = allWaters[i];
            for (int j = 0; j < allElements.Count; j++)
            {
                allElements[j].ClearWater(value);
            }
            param.conveyorSlot.WaterFillCup(value, param.cup,true);
        }
        for (int i = 0; i < allElements.Count; i++)
        {
            allElements[i].AllCalculatorPosition();
        }
        param.conveyorSlot.UnRegisterObject();

    }
    private void OnIncreaseSpeedWater(IncreaseSpeedWaterEvent param)
    {
        for (int a = 0; a < allElements.Count; a++)
        {
            allElements[a].ChangeSpeedWater();
        }
    }
}
