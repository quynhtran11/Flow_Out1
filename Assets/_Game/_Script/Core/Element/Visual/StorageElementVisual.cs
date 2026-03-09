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
        speed = GameData.Instance.GetTimeActiveFill();
    }
    public override void AfterInit()
    {
        speed = GameData.Instance.GetTimeActiveFill();
    }
    public void WaterFills(WaterElement waterFill)
    {
        ParticleSystem go = VFXManager.Instance.GetObject(EVfxType.VFX_WaterFill);
        go.transform.position = storageFillStart.transform.position;
        WaterFill water = go.GetComponent<WaterFill>();
        Color c = GameData.Instance.ColorData.GetData(waterFill.Data.color).color;
        water.OnInit(storageFillStart, storageFillEnd,speed,c);

        var buble = VFXManager.Instance.GetObject(EVfxType.VFX_BubleLarge);
        buble.transform.position = storageFillStart.position;

    }
    public void CompleteStorage()
    {
        Debug.LogError("CompleteStorage");
    }
}
