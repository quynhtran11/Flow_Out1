using UnityEngine;

public class StorageElementVisual : BaseElementVisual<StorageData>
{
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Transform targetEnd;
    [SerializeField] private Transform storageFillStart;
    [SerializeField] private Transform storageFillEnd;
    public Transform TargetEnd => targetEnd;
    public Transform SpawnParent => spawnParent;
    private float speed;
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<IncreaseSpeedWaterEvent>(OnIncreaseSpeedWater);

    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<IncreaseSpeedWaterEvent>(OnIncreaseSpeedWater);

    }
    private void OnIncreaseSpeedWater(IncreaseSpeedWaterEvent param)
    {
        speed = GameData.Instance.TimeActiveFillEndGame;
    }
    public override void AfterInit()
    {
        speed = GameData.Instance.TimeActiveFill;
    }
    public void WaterFills(WaterElement waterFill)
    {
        Debug.LogError("water");
        GameObject go = VFXManager.Instance.GetObject(EVfxType.VFX_WaterFill);
        go.transform.position = storageFillStart.transform.position;
        WaterFill water = go.GetComponent<WaterFill>();
        Color c = GameData.Instance.ColorData.GetData(waterFill.Data.color).color;
        water.OnInit(storageFillStart, storageFillEnd,speed,c);
    }
}
