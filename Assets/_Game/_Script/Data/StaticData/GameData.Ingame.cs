using UnityEngine;

public partial class GameData
{
    [SerializeField] private bool loadResouces;
    [SerializeField] private SoundInfor soundData;
    #region Properties
    public SoundInfor SoundData => soundData;
    public bool LoadResouces=>loadResouces;
    #endregion
}

