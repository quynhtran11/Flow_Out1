using System.Collections.Generic;
using UnityEngine;

public partial class LevelTool
{
    [HideInInspector] public bool forDev;
    [HideInInspector] public bool isEditCup;
    [HideInInspector] public bool isSpawnCup;
    [HideInInspector] public bool isRemoveCup;
    [HideInInspector] public bool isColorCup;
    [HideInInspector] public bool isHiddenCup;
    [HideInInspector] public bool isToggleCup;
    [HideInInspector] public bool isEditStorage;
    [HideInInspector] public bool isRemoveStorage;
    [HideInInspector] public bool isEditWater;
    [HideInInspector] public bool isSpawnWater;
    [HideInInspector] public bool isRemoveWater;
    [HideInInspector] public bool isColorWater;
    [HideInInspector] public bool isHiddenWater;
    [HideInInspector] public bool isFreezeWater;

    [HideInInspector] public EColorType cupColor;
    [HideInInspector] public EColorType waterColor;

    public FreezeData FreezeWater;
    [SerializeField] private LevelToolVisual toolVisual;
    [SerializeField] private List<CupData> allCups = new List<CupData>();
    [SerializeField] private List<StorageData> allStorages = new List<StorageData>();
    public LevelToolVisual ToolVisual => toolVisual;


    public List<CupData> AllCups => allCups;
    public List<StorageData> AllStorages => allStorages;
}
