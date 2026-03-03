using UnityEngine;

public class LevelManager : BLBMono
{
    private int maxBlockAmount;
    private int currentBlockAmount;
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<ClearCupEvent>(OnClearBlock);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<ClearCupEvent>(OnClearBlock);
    }
    private void Start()
    {
        LevelInfor level = new LevelInfor();
        int levelIndex = UserData.CurrentLevel();
        LevelDataLoader.GetLevelInfor(levelIndex, (levelData) =>
        {
            level = levelData;
            EventDispatcher.Dispatch(new StartGameplayEvent()
            {
                level = level
            });
            OnInit(level);
        });
    }

    private void OnClearBlock(ClearCupEvent param)
    {
        currentBlockAmount++;
        if (currentBlockAmount >= maxBlockAmount)
        {
            EventDispatcher.Dispatch(new EndGameEvent()
            {
                isWin = true,
                loseType = ELoseType.None
            });
        }
        EventDispatcher.Dispatch(new IncreaseSpeedGameEvent()
        {
            amount = maxBlockAmount - currentBlockAmount
        });
    }
    private void OnInit(LevelInfor level)
    {
        currentBlockAmount = 0;
        maxBlockAmount = level.AllCups.Length;
    }
}
