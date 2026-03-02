using System;
using UnityEngine;

public class ConveyorSlotElement : BLBMono
{
    private int idSlot;
    private CupElement objectOwner = null;
    public CupElement ObjectOwner => objectOwner;
    public bool IsBusy()
    {
        if(objectOwner != null)
        {
            return true;
        }
        return false;
    }

    public void OnInit(int id)
    {
        this.idSlot = id;
    }
    public void RegisterObject(CupElement cup)
    {
        this.objectOwner = cup;
        cup.transform.SetParent(Tf);
    }
    public void UnRegisterObject()
    {
        this.objectOwner = null;
    }
    public void OnUpdate(Vector2 start, Vector2 end)
    {
        Vector2 speed = Vector2.right * GameData.Instance.SpeedConveyor * Time.deltaTime;
        Tf.Translate(speed);
        if (Tf.position.x >= end.x)
        {
            Tf.position = new Vector3(start.x, start.y, Tf.position.z);
        }
        if (this.objectOwner == null) return;
        if (!this.objectOwner.IsCheck || this.objectOwner.IsBusy) return;
        EventDispatcher.Dispatch(new CheckFillWaterEvent()
        {
            callBack = (x) =>
            {
                WaterFillCup(x,objectOwner);
            },
            pos = Tf.position,
            color = this.objectOwner.Data.color
        });
    }
    private void WaterFillCup(WaterElement water,CupElement cup)
    {
        if (water == null) return;
        water.WaterFill(cup);
        Debug.LogError("fill_" + water.Data.color + "ID" + water.Data.waterID);
    }
}
