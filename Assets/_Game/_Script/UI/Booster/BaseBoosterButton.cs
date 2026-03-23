using UnityEngine;

public class BaseBoosterButton : BLBMono
{
    [SerializeField] private ButtonEffect btn;
    [SerializeField] private EBoosterType boosterType;
    protected BoosterData data;
    public EBoosterType BoosterType => boosterType;

    public string KeyBooster
    {
        get
        {
            return GameUntilities.GetKeyBooster(boosterType);
        }
    }
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
    protected void UseBooser()
    {
        int amount = UserData.GetBooster(KeyBooster);
        EventDispatcher.Dispatch(new ClickBoosterGuidEvent()
        {
            data = data
        });
    }
    public void OnInit(BoosterData booterData)
    {
        this.data = booterData;

    }
}
