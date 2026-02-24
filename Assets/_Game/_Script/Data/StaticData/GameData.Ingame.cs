using UnityEngine;

public partial class GameData
{
    [SerializeField] private SoundInfor soundData;
    #region Properties
    public SoundInfor SoundData => soundData;
    #endregion
}

