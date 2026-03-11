using System;
using UnityEngine;

public class ConveyorSlotElement : BLBMono
{
    [SerializeField] private ConveyorSlotElementVisual visual;
    private int idSlot;
    private CupElement objectOwner = null;
    public CupElement ObjectOwner => objectOwner;
    public bool IsBusy()
    {
        if (objectOwner != null)
        {
            return true;
        }
        return false;
    }
    public void OnInit(int id,float delay)
    {
        this.idSlot = id;
        visual.OnInit(delay);
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
        Vector2 calculatorSpeed = Vector2.right * GameData.Instance.GetSpeedConveyor() * Time.deltaTime;
        Tf.Translate(calculatorSpeed);
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
                WaterFillCup(x, objectOwner);
            },
            pos = Tf.position,
            color = this.objectOwner.Color
        });       
    }
    public void WaterFillCup(WaterElement water, CupElement cup,bool isRevive = false)
    {
        if (water == null) return;
        cup.WaterFill(isRevive);
        water.WaterFill();
        //cup.StopCheck();
        //Debug.LogError("fill_" + water.Data.color + "ID" + water.Data.waterID);
    }
    public void WarningConveyor()
    {
        visual.WarningConveyor();
    }
}
