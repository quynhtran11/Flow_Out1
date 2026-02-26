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
        LevelDataLoader.GetLevelInfor(1, (levelData) =>
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
            Debug.LogError("win");
        }
    }
    private void OnInit(LevelInfor level)
    {
        currentBlockAmount = 0;
        maxBlockAmount = level.AllCups.Length;
    }
}
