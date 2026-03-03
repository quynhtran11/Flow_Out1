using UnityEngine;

public class GameHUD : HUD
{
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<WinGameEvent>(OnWinGame);
        EventDispatcher.RegisterEvent<LoseGameEvent>(OnLoseGame);
        EventDispatcher.RegisterEvent<ReviveGameEvent>(OnReviveGame);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<WinGameEvent>(OnWinGame);
        EventDispatcher.RemoveEvent<LoseGameEvent>(OnLoseGame);
        EventDispatcher.RemoveEvent<ReviveGameEvent>(OnReviveGame);
    }
    private void DelayOpenWinUI()
    {
        Debug.LogError("winui");
        OpenUI<WinGameUI>();
    }
    private void DelayOpenLoseUI()
    {
        Debug.LogError("loseui");

        OpenUI<LoseGameUI>();

    }
    private void OnWinGame(WinGameEvent param)
    {
        float t = GameData.Instance.DelayCallWinUI;
        Invoke(nameof(DelayOpenWinUI), t);
    }
    private void OnLoseGame(LoseGameEvent param)
    {
        float t = GameData.Instance.DelayCallLoseUI;
        Invoke(nameof(DelayOpenLoseUI), t);
    }
    private void OnReviveGame(ReviveGameEvent param)
    {
        GameHUD.Instance.CloseUI<LoseGameUI>();
    }
}
