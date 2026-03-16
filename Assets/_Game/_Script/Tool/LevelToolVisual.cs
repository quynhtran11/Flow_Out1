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
        return hiddenWaterPrefab;
    }

}
