using UnityEngine;

public class StorageElement : BaseElement<StorageElementVisual, StorageData>
{
    private StorageSpawn storageSpawn;

    public override void OnInit()
    {
        base.OnInit();
        storageSpawn = new StorageSpawn(visual.SpawnParent);
        storageSpawn.OnInit(data);
    }
    public WaterElement GetWaterFill(EColorType type)
    {
        Debug.LogError("1");
        if(storageSpawn.AllWaters==null|| storageSpawn.AllWaters.Count<=0||
            storageSpawn.AllWaters[0] == null || storageSpawn.AllWaters[0].IsBusy 
            || storageSpawn.AllWaters[0].Data.color != type) return null;
        var value = storageSpawn.AllWaters[0];
        storageSpawn.AllWaters.Remove(value);
        return value;
    }
}
