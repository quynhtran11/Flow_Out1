using UnityEngine;

public partial class GameData
{
    [SerializeField] private bool loadResouces;
    [SerializeField] private int maxLevel;
    [SerializeField] private int initSlot;
    [SerializeField] private float clickRadius;
    [SerializeField] private float coinLevelReward;
    [SerializeField] private float speedConveyor; // toc do conveyor
    [SerializeField] private float speedWaterFill; // toc do nuoc roi 
    [SerializeField] private float timeActiveFill; // toc do line nuoc 
    [SerializeField] private float distanceCheckFill; // khoang cach check fill
    [SerializeField] private float speedConveyorEndGame;
    [SerializeField] private float speedWaterFillEndGame;
    [SerializeField] private float timeActiveFillEndGame;
    [SerializeField] private float delayCallWinUI;
    [SerializeField] private float delayCallLoseUI;
    [SerializeField] private SoundInfor soundData;
    [SerializeField] private ColorInfor colorData;
    [SerializeField] private ElementInfor elementInfor;
    [SerializeField] private VFXInfor vfxInfor;
    #region Properties
    public SoundInfor SoundData => soundData;
    public ColorInfor ColorData => colorData;
    public ElementInfor ElementInfor => elementInfor;
    public VFXInfor VfxInfor => vfxInfor;
    public bool LoadResouces=>loadResouces;
    public float ClickRadius => clickRadius;
    public float CoinLevelReward => coinLevelReward;
    public float DelayCallWinUI => delayCallWinUI;
    public float DelayCallLoseUI => delayCallLoseUI;
    public float DistanceCheckFill => distanceCheckFill;
    public int InitSlot => initSlot;
    public int MaxLevel => maxLevel;

    #endregion
    private void OnValidate()
    {
        var levels = Resources.LoadAll("Data/Level");
        maxLevel = levels.Length;
    }
    public float GetTimeActiveFill()
    {
        if (GameManager.Instance.IsSpeed)
        {
            return timeActiveFillEndGame;
        }
        else
        {
            return timeActiveFill;
        }
    }
    public float GetSpeedConveyor()
    {
        if (GameManager.Instance.IsSpeed)
        {
            return speedConveyorEndGame;
        }
        else
        {
            return speedConveyor;
        }
    }
    public float GetSpeedWaterFill()
    {
        if (GameManager.Instance.IsSpeed)
        {
            return speedWaterFillEndGame;
        }
        else
        {
            return speedWaterFill;
        }
    }
}

