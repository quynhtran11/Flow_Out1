using UnityEngine;

public class SettingUI : UIBase
{
    [SerializeField] private ButtonEffect btnHome;
    [SerializeField] private ButtonEffect btnRestart;
    [SerializeField] private ButtonEffect btnExit;
    private void OnEnable()
    {
        btnHome.onClick.AddListener(ClickBackHome);
        btnRestart.onClick.AddListener(ClickRestart);
        btnExit.onClick.AddListener(ClickExit);
    }
    private void OnDisable()
    {
        btnHome.onClick.RemoveListener(ClickBackHome);
        btnRestart.onClick.RemoveListener(ClickRestart);
        btnExit.onClick.RemoveListener(ClickExit);
    }
    private void ClickBackHome()
    {
        var service = ServiceLocator.Get<SceneService>();
        if (service == null) return;
        service.LoadScene(ESceneType.Home);
    }
    private void ClickRestart()
    {
        var service = ServiceLocator.Get<SceneService>();
        if (service == null) return;
        service.LoadScene(ESceneType.Ingame);
    }
    private void ClickExit()
    {
        GameHUD.Instance.CloseUI<SettingUI>();
    }
}
