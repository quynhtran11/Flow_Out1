using System;
using UnityEngine;

public class PickUpBooster : IBooster
{
    public EBoosterType boosterType => EBoosterType.PickUp;

    public void OnClick()
    {
    }

    public void OnExit()
    {
    }

    public void OnInit()
    {
    }

    public void OnUpdate(Action callBack)
    {
    }
}