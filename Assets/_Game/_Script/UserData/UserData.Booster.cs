using UnityEngine;
public partial class UserData
{
    public static int GetBooster(string key)
    {
        int defaultValue = GameData.Instance.DefaulBooster;
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, defaultValue);
        }
        int value = PlayerPrefs.GetInt(key, defaultValue);
        return value;
    }
    public static void InCreaseBooster(string key, int amount = 1)
    {
        int defaultValue = GameData.Instance.DefaulBooster;

        int value = PlayerPrefs.GetInt(key, defaultValue);
        PlayerPrefs.SetInt(key, value + amount);
    }
    public static void SetBooster(string key, int amount = 1)
    {
        PlayerPrefs.SetInt(key, amount);
    }
    public static void DeCreaseBooster(string key, int amount = 1)
    {
        int defaultValue = GameData.Instance.DefaulBooster;

        int value = PlayerPrefs.GetInt(key, defaultValue);
        PlayerPrefs.SetInt(key, value - amount);
    }

}