using System.Collections.Generic;
using UnityEngine;

public class StorageElement : BaseElement<StorageElementVisual, StorageData>
{
    private StorageSpawn storageSpawn;
    private WaterElement currentWater = null;
    public override void OnInit()
    {
        base.OnInit();
        storageSpawn = new StorageSpawn(visual.SpawnParent,visual.TargetEnd);
        storageSpawn.OnInit(data);
    }
    public WaterElement GetWaterFill(EColorType type)
    {
        if (storageSpawn.AllWaters == null || storageSpawn.AllWaters.Count <= 0) {
            currentWater = null;
            return currentWater;
        }
        currentWater = storageSpawn.AllWaters[0];
        if (currentWater == null || currentWater.IsBusy || currentWater.Data.color != type) return null;
        storageSpawn.AllWaters.Remove(currentWater);
        storageSpawn.CalculatorPosition();
        if (storageSpawn.AllWaters.Count <= 0)
        {
            isBusy = true;
        }
        return currentWater;
    }
    public WaterElement FirstWater()
    {
        if (storageSpawn.AllWaters == null || storageSpawn.AllWaters.Count <= 0) return null;
        if (isBusy) return null;
        return storageSpawn.AllWaters[0];
    }
    public void ClearWater(WaterElement water)
    {
        storageSpawn.ClearWater(water);
    }
    public List<WaterElement> AllWater()
    {
        return storageSpawn.AllWaters;
    }
    public void AllCalculatorPosition()
    {
        storageSpawn.CalculatorPosition();
    }
    public void ChangeSpeedWater()
    {
        storageSpawn.ChangeSpeedWater();
    }
}