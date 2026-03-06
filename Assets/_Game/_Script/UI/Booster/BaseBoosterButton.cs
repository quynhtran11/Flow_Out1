using UnityEngine;

public abstract class BaseBoosterButton : BLBMono
{
    [SerializeField] private ButtonEffect btn;
    [SerializeField] private EBoosterType type;
    private void OnEnable()
    {
        btn.onClick.AddListener(ClickBooster);
    }
    private void OnDisable()
    {
        btn.onClick.RemoveListener(ClickBooster);
    }
    private void ClickBooster()
    {
        UseBooser();
    }
    protected abstract void UseBooser();
}
