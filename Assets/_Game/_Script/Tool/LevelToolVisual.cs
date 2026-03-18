#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class LevelToolVisual : MonoBehaviour
{
    [SerializeField] private GameObject cupPrefab;
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private GameObject hiddenWaterPrefab;
    public GameObject CupPrefab()
    {
        return cupPrefab;
    }
    public GameObject WaterPrefab()
    {
        return waterPrefab;
    }
    public GameObject HiddenWaterPrefab()
    {
#if UNITY_EDITOR
        GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(hiddenWaterPrefab, transform);
        return go;
#endif
        return null;
    }

}
