using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameData", fileName = "GameData")]
public partial class GameData : ScriptableObject
{
    private static GameData instance;

    public static GameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameData>("Data/GameData");
                if (instance == null)
                {
                    Debug.LogError("Not assign GameData");
                }
            }
            return instance;
        }
    }
}