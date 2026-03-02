using UnityEngine;

public class GameHUD : HUD
{
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<WinGameEvent>(OnWinGame);
        EventDispatcher.RegisterEvent<LoseGameEvent>(OnLoseGame);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<WinGameEvent>(OnWinGame);
        EventDispatcher.RemoveEvent<LoseGameEvent>(OnLoseGame);
    }
    private void DelayOpenWinUI()
    {
        Debug.LogError("winui");

        OpenUI<WinGameUI>();

    }
    private void OnWinGame(WinGameEvent param)
    {
        Invoke(nameof(DelayOpenWinUI), 1f);
    }
    private void OnLoseGame(LoseGameEvent param)
    {
        OpenUI<LoseGameUI>();
    }
}
