using UnityEngine;

public abstract class BaseButton : BLBMono
{
    [SerializeField] protected ButtonEffect btn;
    private void OnEnable()
    {
        btn.onClick.AddListener(Click);
    }
    private void OnDisable()
    {
        btn.onClick.RemoveListener(Click);
    }
    protected abstract void Click();
}
