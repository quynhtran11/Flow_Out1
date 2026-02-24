using UnityEngine;

public partial class UserData {
    public static bool IsSound()
    {
        int value = PlayerPrefs.GetInt(Constans.KeySound, 1);
        return value == 1;
    }
    public static bool IsMusic()
    {
        int value = PlayerPrefs.GetInt(Constans.KeyMusic, 1);
        return value == 1;
    }
    public static bool IsVibrate()
    {
        int value = PlayerPrefs.GetInt(Constans.KeyVibrate, 1);
        return value == 1; 
    }
    public static void SetSound()
    {
        int value = PlayerPrefs.GetInt(Constans.KeySound, 1);
        PlayerPrefs.SetInt(Constans.KeySound, value == 1 ? 0 : 1);
    }
    public static void SetMusic()
    {
        int value = PlayerPrefs.GetInt(Constans.KeyMusic, 1);
        PlayerPrefs.SetInt(Constans.KeyMusic, value == 1 ? 0 : 1);
    }
    public static void SetVibrate()
    {
        int value = PlayerPrefs.GetInt(Constans.KeyVibrate, 1);
        PlayerPrefs.SetInt(Constans.KeyVibrate, value == 1 ? 0 : 1);
    }
}
