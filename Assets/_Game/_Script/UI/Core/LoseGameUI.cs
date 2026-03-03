using UnityEngine;

public class LoseGameUI : UIBase
{
    [SerializeField] private ButtonEffect btnBuy;
    [SerializeField] private ButtonEffect btnExit;
    private void OnEnable()
    {
        btnBuy.onClick.AddListener(ClickBuy);
        btnExit.onClick.AddListener(ClickExit);
    }
    private void OnDisable()
    {
        btnBuy.onClick.RemoveListener(ClickBuy);
        btnExit.onClick.RemoveListener(ClickExit);
    }
    private void ClickBuy()
    {
        EventDispatcher.Dispatch(new ReviveGameEvent() { });
    }
    private void ClickExit()
    {
        var service = ServiceLocator.Get<SceneService>();
        if (service == null) return;
        service.LoadScene(ESceneType.Ingame);
    }
}
