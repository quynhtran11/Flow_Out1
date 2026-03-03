using UnityEngine;

public class LoseGameUI : UIBase
{
    [SerializeField] private ButtonEffect btnBuy;
    private void OnEnable()
    {
        btnBuy.onClick.AddListener(ClickBuy);
    }
    private void OnDisable()
    {
        btnBuy.onClick.RemoveListener(ClickBuy);
    }
    private void ClickBuy()
    {
        var service = ServiceLocator.Get<SceneService>();
        if (service == null) return;
        service.LoadScene(ESceneType.Ingame);
    }
}
