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
    public bool isWin;
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
public struct ChangeSceneEvent : IEvenParam  // call when changescene
{
    public ESceneType sceneType;
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
    public CupElementVisual cup;
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
public struct CheckAllQualifiedFillEvent : IEvenParam // call check when full cup in conveyor
{
    public List<CupElement> cups;
}
public struct CheckLoseEvent : IEvenParam// call check when check lose
{

}
public struct ReviveGameEvent : IEvenParam // call when revive game
{ 
}
public struct ReviveStorageEvent : IEvenParam // call when revive 
{
    public CupElement cup;
    public ConveyorSlotElement conveyorSlot;
}
public struct IncreaseSpeedGameEvent : IEvenParam // call when add speed game
{
    public int amount;
}
public struct IncreaseSpeedWaterEvent : IEvenParam // call when add speed game
{
}
public struct AddSlotEvent : IEvenParam // call when add slot 
{

}
public struct UseShuffleEvent : IEvenParam // call when use shuffle
{

}
public struct FillPauseGameEvent : IEvenParam // call when fill 
{

}
public struct FillContinueGame : IEvenParam // call when fill complete
{

}
public struct CompleteHiddenPropertiesEvent : IEvenParam // call when hidden complete
{
    public WaterElement water;
}