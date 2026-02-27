using UnityEngine;

public partial class LevelInfor
{
    [SerializeField] private int levelID;
    [SerializeField] private Vector2Int map;
    [SerializeField] private EModeType mode;
    [SerializeField] private CupData[] allCups;
    [SerializeField] private StorageData[] allStorages;
}
[System.Serializable]
public struct CupData
{
    public int id;
    public int amount;
    public EColorType color;
    public Vector2Int pos;
    public HiddenData hiddenData;
    public ConnectData connectData;
}
[System.Serializable]
public struct StorageData
{
    public WaterData[] waterDatas;
}
[System.Serializable]
public struct WaterData
{
    public int waterID;
    public int waterGroupID;
    public EColorType color;
}

[System.Serializable]
public struct HiddenData
{
    public bool isHidden;
}
[System.Serializable]
public struct ConnectData
{
    public int idConnect;
}