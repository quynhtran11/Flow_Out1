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
        if (storageSpawn.AllWaters == null || storageSpawn.AllWaters.Count <= 0) return null;
        currentWater = storageSpawn.AllWaters[0];
        if (currentWater == null || currentWater.IsBusy || currentWater.Data.color != type) return null;
        storageSpawn.AllWaters.Remove(currentWater);
        storageSpawn.CalculatorPosition();
        return currentWater;
    }
}