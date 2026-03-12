using System.Collections.Generic;
using UnityEngine;

public partial class LevelTool
{
    [HideInInspector] public bool isEditCup;
    [HideInInspector] public bool isHiddenCup;
    [HideInInspector] public bool isToggleCup;
    [HideInInspector] public bool isEditWater;

    [SerializeField] private LevelToolVisual toolVisual;
    [SerializeField] List<CupData> allCups = new List<CupData>();


    public List<CupData> AllCups => allCups;
}
