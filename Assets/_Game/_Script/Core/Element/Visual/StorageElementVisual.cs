using UnityEngine;

public class StorageElementVisual : BaseElementVisual<StorageData>
{
    [SerializeField] private Transform spawnParent;
    [SerializeField] private Transform targetEnd;
    public Transform TargetEnd => targetEnd;
    public Transform SpawnParent => spawnParent;
    public override void AfterInit()
    {
    }

    
}
