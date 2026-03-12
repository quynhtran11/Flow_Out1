using UnityEngine;

public class LevelToolVisual : MonoBehaviour
{
    [SerializeField] private GameObject cupPrefab;
    [SerializeField] private GameObject waterPrefab;
    public GameObject CupPrefab()
    {
        return cupPrefab;
    }
    public GameObject WaterPrefab()
    {
        return waterPrefab;
    }
    
}
