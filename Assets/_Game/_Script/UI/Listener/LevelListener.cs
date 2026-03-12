using TMPro;
using UnityEngine;

public class LevelListener : BLBMono
{
    [SerializeField] private TextMeshProUGUI textLevel;
    private void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        textLevel.text = "Lv " + UserData.CurrentLevel();
    }
}
