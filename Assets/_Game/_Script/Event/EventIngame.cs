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
public struct TouchSuccessBlockEvent : IEvenParam // call when click success block 
{
    public BlockElement block;
}

