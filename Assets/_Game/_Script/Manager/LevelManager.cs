using UnityEngine;

public class LevelManager : BLBMono
{
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
        });
    }
}
