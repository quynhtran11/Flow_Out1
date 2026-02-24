using System;

public static class LevelDataLoader { 
    public static void GetLevelInfor(int index,Action<LevelInfor> callBack)
    {
        JsonManager.GetLevelInfor(index, (level) =>
        {
            if (level == null) return;
            callBack?.Invoke(level);
        });
    }
}
