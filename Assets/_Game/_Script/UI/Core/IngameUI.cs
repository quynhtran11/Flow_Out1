using TMPro;
using UnityEngine;

public class IngameUI : UIBase
{
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private ButtonEffect btnSetting;
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<StartGameplayEvent>(OnStartGame);
        btnSetting.onClick.AddListener(ClickSetting);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<StartGameplayEvent>(OnStartGame);
        btnSetting.onClick.RemoveListener(ClickSetting);
    }
    private void OnStartGame(StartGameplayEvent param)
    {
        textLevel.text = "Level_"+param.level.LevelID.ToString();
    }
    private void ClickSetting()
    {
        // open setting ui 
        GameHUD.Instance.OpenUI<SettingUI>();
    }
}
