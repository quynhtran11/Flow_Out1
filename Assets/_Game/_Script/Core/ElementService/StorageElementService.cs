using UnityEngine;

public class StorageElementService : BaseElementService<StorageElement>
{
    protected override void RegisterEvent()
    {
        EventDispatcher.RemoveEvent<CheckFillWaterEvent>(OnCheckFillWater);
        EventDispatcher.RegisterEvent<CheckFillWaterEvent>(OnCheckFillWater);
    }
    private void OnCheckFillWater(CheckFillWaterEvent param)
    {
        if(allElements == null || allElements.Count<=0)  return;
        Debug.LogError("12");
        for (int i = 0; i < allElements.Count; i++) {
            if (param.pos.x - allElements[i].Tf.position.x > .1f) continue;
            param.callBack.Invoke(allElements[i].GetWaterFill(param.color));
        }
    }
    public override void InitElement(LevelInfor level)
    {
        foreach(var storage in allElements)
        {
            storage.OnInit();
        }
    }
}
