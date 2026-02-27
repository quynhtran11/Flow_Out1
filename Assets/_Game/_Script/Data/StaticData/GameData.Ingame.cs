using UnityEngine;

public partial class GameData
{
    [SerializeField] private bool loadResouces;
    [SerializeField] private int initSlot;
    [SerializeField] private float clickRadius;
    [SerializeField] private float speedConveyor;
    [SerializeField] private SoundInfor soundData;
    [SerializeField] private ColorInfor colorData;
    [SerializeField] private ElementInfor elementInfor;
    #region Properties
    public SoundInfor SoundData => soundData;
    public ColorInfor ColorData => colorData;
    public ElementInfor ElementInfor => elementInfor;
    public bool LoadResouces=>loadResouces;
    public float ClickRadius => clickRadius;
    public float SpeedConveyor => speedConveyor;
    public int InitSlot => initSlot;

    #endregion
}

