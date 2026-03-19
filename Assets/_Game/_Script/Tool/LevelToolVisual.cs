#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class LevelToolVisual : MonoBehaviour
{
    [SerializeField] private GameObject cupPrefab;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private GameObject storagePrefab;
    [SerializeField] private GameObject hiddenWaterPrefab;
    [SerializeField] private GameObject freezeWaterPrefab;
    public GameObject CupPrefab()
    {
        return cupPrefab;
    }
    public GameObject WaterPrefab()
    {
        return waterPrefab;
    }
    public GameObject StoragePrefab()
    {
        return storagePrefab;
    }
    public GameObject HiddenWaterPrefab()
    {
#if UNITY_EDITOR
        GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(hiddenWaterPrefab, transform);
        return go;
#endif
        return null;
    }
    public GameObject FreezeWaterPrefab()
    {
#if UNITY_EDITOR
        GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(freezeWaterPrefab, transform);
        return go;
#endif
        return null;
    }

}
