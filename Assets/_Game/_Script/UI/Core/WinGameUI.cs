using TMPro;
using UnityEngine;

public class WinGameUI : UIBase
{
    [SerializeField] private ButtonEffect btnContinue;
    [SerializeField] private ButtonEffect btnAds;
    [SerializeField] private TextMeshProUGUI textComplete;
    [SerializeField] private TextMeshProUGUI textAmount;
    private void OnEnable()
    {
        btnContinue.onClick.AddListener(ClickContinue);
        btnAds.onClick.AddListener(ClickAds);
        OnInit();
    }
    private void OnDisable()
    {
        btnContinue.onClick.RemoveListener(ClickContinue);
        btnAds.onClick.RemoveListener(ClickAds);
    }
    private void OnInit()
    {
        textComplete.text = $"Level {UserData.CurrentLevel()} " +
            $"COMPLETE!";
        textAmount.text = GameData.Instance.CoinLevelReward.ToString();
    }
    private void ClickContinue()
    {
        UserData.InCreaseLevel();
        ClaimReward((int)GameData.Instance.CoinLevelReward);
    }
    private void ClickAds()
    {
        float valueCoin = GameData.Instance.CoinLevelReward * 2f;
        ClaimReward((int)valueCoin);
    }
    private void ClaimReward(int coin)
    {
        UserData.InCreaseCoin(coin, callBack: () =>
        {
            var service = ServiceLocator.Get<SceneService>();
            if (service == null) return;
            service.LoadScene(ESceneType.Ingame);
        });
    }
}