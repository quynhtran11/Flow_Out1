using UnityEngine;

public partial class GameData
{
    [SerializeField] private bool loadResouces;
    [SerializeField] private int maxLevel;
    [SerializeField] private int initSlot;
    [SerializeField] private float clickRadius;
    [SerializeField] private float coinLevelReward;
    [SerializeField] private float speedConveyor;
    [SerializeField] private float speedWaterFill;
    [SerializeField] private float speedWaterFillEndGame;
    [SerializeField] private float speedConveyorEndGame;
    [SerializeField] private float delayCallWinUI;
    [SerializeField] private float delayCallLoseUI;
    [SerializeField] private SoundInfor soundData;
    [SerializeField] private ColorInfor colorData;
    [SerializeField] private ElementInfor elementInfor;
    #region Properties
    public SoundInfor SoundData => soundData;
    public ColorInfor ColorData => colorData;
    public ElementInfor ElementInfor => elementInfor;
    public bool LoadResouces=>loadResouces;
    public float ClickRadius => clickRadius;
    public float CoinLevelReward => coinLevelReward;
    public float SpeedConveyor => speedConveyor;
    public float SpeedWaterFill => speedWaterFill;
    public float SpeedConveyorEndGame => speedConveyorEndGame;
    public float SpeedWaterFillEndGame => speedWaterFillEndGame;
    public float DelayCallWinUI => delayCallWinUI;
    public float DelayCallLoseUI => delayCallLoseUI;
    public int InitSlot => initSlot;
    public int MaxLevel => maxLevel;

    #endregion
    private void OnValidate()
    {
        var levels = Resources.LoadAll("Data/Level");
        maxLevel = levels.Length;
    }
}

