using System;
using UnityEngine;
public partial class UserData
{
    public static int CurrentLevel( int defaultValue = 1)
    {
        int value = PlayerPrefs.GetInt(Constans.KeyCurrentLevelUnlock, defaultValue);
        return value;
    }
    public static void InCreaseLevel( int amount = 1, int defaultValue = 1)
    {
        int value = PlayerPrefs.GetInt(Constans.KeyCurrentLevelUnlock, defaultValue);
        PlayerPrefs.SetInt(Constans.KeyCurrentLevelUnlock, value + amount);
    }
    public static void SetLevel(int amount = 1)
    {
        // test 
        int value = PlayerPrefs.GetInt(Constans.KeyCurrentLevelUnlock, 1);
        PlayerPrefs.SetInt(Constans.KeyCurrentLevelUnlock,amount);
    }
    public static int CurrentCoin()
    {
        return PlayerPrefs.GetInt(Constans.KeyCurrentCoin, 0);
    }
    public static void InCreaseCoin(int amount, int defaultValue = 0,Action callBack = null)
    {
        int value = PlayerPrefs.GetInt(Constans.KeyCurrentCoin, defaultValue);
        PlayerPrefs.SetInt(Constans.KeyCurrentCoin, value + amount);
        int coin = PlayerPrefs.GetInt(Constans.KeyCurrentCoin, defaultValue);

        EventDispatcher.Dispatch(new ChangeCoinEvent()
        {
            coin = coin,
            callBack = callBack,
            isAnim = true,
            oldCoin = value
        });
    }
    public static void DeCreaseCoin( int amount = 1, int defaultValue = 0)
    {
        int value = PlayerPrefs.GetInt(Constans.KeyCurrentCoin, defaultValue);
        PlayerPrefs.SetInt(Constans.KeyCurrentCoin, value - amount);

        int coin = PlayerPrefs.GetInt(Constans.KeyCurrentCoin, defaultValue);

        EventDispatcher.Dispatch(new ChangeCoinEvent()
        {
            coin = coin
        });
    }
}