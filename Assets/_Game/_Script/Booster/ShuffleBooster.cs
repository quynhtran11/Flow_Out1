using System;
using UnityEngine;

public class ShuffleBooster : IBooster
{
    public EBoosterType boosterType => EBoosterType.Shuffle;

    public void OnClick()
    {
    }

    public void OnExit()
    {
    }

    public void OnInit()
    {
        //var vfx 
        //var prefab = GameData.Instance.BoosterData.GetData(boosterType).visual;
        //var vfx = GameObject.Instantiate(prefab);
        EventDispatcher.Dispatch(new ShuffleEvent() { });

        EventDispatcher.Dispatch(new ExitBoosterGuidEvent() { });
    }

    public void OnUpdate(Action callBack)
    {
    }
}
