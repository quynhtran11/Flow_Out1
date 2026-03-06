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
        Debug.LogError("init");
    }

    public void OnUpdate(Action callBack)
    {
    }
}
