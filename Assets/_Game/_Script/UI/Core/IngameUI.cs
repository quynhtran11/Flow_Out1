using TMPro;
using UnityEngine;

public class IngameUI : UIBase
{
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private ButtonEffect btnSetting;
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<StartGameplayEvent>(OnStartGame);
        EventDispatcher.RegisterEvent<ReviveGameEvent>(OnReviveGame);
        EventDispatcher.RegisterEvent<EndGameEvent>(OnEndGame);
        btnSetting.onClick.AddListener(ClickSetting);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<StartGameplayEvent>(OnStartGame);
        EventDispatcher.RemoveEvent<EndGameEvent>(OnEndGame);
        EventDispatcher.RemoveEvent<ReviveGameEvent>(OnReviveGame);
        btnSetting.onClick.RemoveListener(ClickSetting);
    }
    private void OnStartGame(StartGameplayEvent param)
    {
        textLevel.text = "Level " + param.level.LevelID.ToString();
        btnSetting.interactable = true;
    }
    private void OnReviveGame(ReviveGameEvent param)
    {
        btnSetting.interactable = true;
    }
    private void OnEndGame(EndGameEvent param)
    {
        btnSetting.interactable = false;
    }
    private void ClickSetting()
    {
        // open setting ui 
        GameHUD.Instance.OpenUI<SettingUI>();
        EventDispatcher.Dispatch(new PauseGameEvent()
        {
            isSetting = true
        });
    }
}
