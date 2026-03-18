using UnityEngine;

public class StorageElementVisual : BaseElementVisual<StorageData>
{
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Transform targetEnd;
    [SerializeField] private Transform storageFillStart;
    [SerializeField] private Transform storageFillEnd;
    [SerializeField] private Transform parentFill;
    [SerializeField] private ParticleSystem vfxLoop;
    public Transform TargetEnd => targetEnd;
    public Transform SpawnParent => spawnParent;
    public override void AfterInit()
    {
    }
    public void WaterFills(WaterElement waterFill)
    {
        ParticleSystem go = VFXManager.Instance.GetObject(EVfxType.VFX_WaterFill);
        go.transform.position = storageFillStart.transform.position;
        WaterFill water = go.GetComponent<WaterFill>();
        Color c = GameData.Instance.ColorData.GetData(waterFill.Data.color).color;
        water.OnInit(storageFillStart, storageFillEnd, GameData.Instance.GetTimeActiveFill(), c);

        var buble = VFXManager.Instance.GetObject(EVfxType.VFX_BubleLarge);
        buble.transform.position = storageFillStart.position;

        var bubleFill = VFXManager.Instance.GetObject(EVfxType.VFX_BubleFill);
        bubleFill.transform.position = parentFill.position;


    }
    public void CompleteStorage()
    {
        var emi = vfxLoop.emission;
        emi.enabled = false;
        Debug.LogError("CompleteStorage");
    }
}
