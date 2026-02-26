using UnityEngine;

public partial class GameData
{
    [SerializeField] private bool loadResouces;
    [SerializeField] private int initSlot;
    [SerializeField] private float clickRadius;
    [SerializeField] private SoundInfor soundData;
    [SerializeField] private ColorInfor colorData;
    #region Properties
    public SoundInfor SoundData => soundData;
    public ColorInfor ColorData => colorData;
    public bool LoadResouces=>loadResouces;
    public float ClickRadius => clickRadius;
    public int InitSlot => initSlot;

    #endregion
}

