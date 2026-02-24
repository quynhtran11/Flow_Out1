using UnityEngine;

public partial class LevelInfor
{
    public int LevelID
    {
        get => levelID;
        set => levelID = value;
    }
    public Vector2Int Map
    {
        get => map;
        set
        {
            map = value;
        }
    }
    public EModeType Mode
    {
        get => mode;
        set
        {
            mode = value;
        }
    }
    public BlockData[] AllBlocks
    {
        get => allBlocks;
        set
        {
            allBlocks = value;
        }
    }
}
