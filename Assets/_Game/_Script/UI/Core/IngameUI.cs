using TMPro;
using UnityEngine;

public class IngameUI : UIBase
{
    [SerializeField] private TextMeshProUGUI textLevel;
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<StartGameplayEvent>(OnStartGame);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<StartGameplayEvent>(OnStartGame);
    }
    private void OnStartGame(StartGameplayEvent param)
    {
        textLevel.text = "Level_"+param.level.LevelID.ToString();
    }
}
