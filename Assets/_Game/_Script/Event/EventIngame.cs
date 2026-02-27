using System;
using System.Collections.Generic;
using UnityEngine;

public struct OpenAppEvent : IEvenParam // call for tracking .. 
{

}
public struct StartGameplayEvent : IEvenParam // call when play ingame
{
    public LevelInfor level;
}
public struct PauseGameEvent : IEvenParam // call when pause game
{
    public bool isSetting;
}
public struct ContinueGameEvent : IEvenParam // call when from pause game 
{

}
public struct EndGameEvent : IEvenParam // call when end game maybe win or lose
{
    public ELoseType loseType;
}
public struct LoseGameEvent : IEvenParam // lose game 
{
    public ELoseType loseType;
}
public struct WinGameEvent : IEvenParam // win game
{
}
public struct ChangeCoinEvent : IEvenParam // call when change coin 
{
    public int coin;
    public int oldCoin;
    public Action callBack;
    public bool isAnim;
}
public struct TouchSuccessCupEvent : IEvenParam // call when click success cup
{
    public CupElement cup;
}
public struct TouchFailedCupEvent : IEvenParam // call when click failed cup
{
    public CupElement cup;
}
public struct ClearCupEvent : IEvenParam // call when clear 1 cup
{

}
public struct CupToConveyorEvent : IEvenParam // call when cup to conveyor
{
    public CupElement cup;  
}
public struct CheckFullSlotConveyorEvent : IEvenParam // call check full slot
{
    public Action<bool> isFullSlot;
}
public struct CheckFillWaterEvent : IEvenParam 
{
    // call check water qualified to cup
    // 
    public Action<WaterElement> callBack;
    public Vector3 pos;
    public EColorType color;
}
