using UnityEngine;

public partial class LevelInfor
{
    [SerializeField] private int levelID;
    [SerializeField] private Vector2Int map;
    [SerializeField] private EModeType mode;
    [SerializeField] private BlockData[] allBlocks;
}
[System.Serializable]
public struct BlockData
{
    public int id;
    public int amount;
    public Vector2Int pos;
    public HiddenProperties hiddenProperties;
    public ConnectProperties connectProperties;
}
[System.Serializable]

public struct HiddenProperties
{
    public bool isHidden;
}
[System.Serializable]
public struct ConnectProperties
{
    public int idConnect;
}