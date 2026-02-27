using UnityEngine;

public class StorageElementVisual : BaseElementVisual<StorageData>
{
    [SerializeField] private Transform spawnParent;
    public Transform SpawnParent => spawnParent;
    public override void AfterInit()
    {
    }

    
}
