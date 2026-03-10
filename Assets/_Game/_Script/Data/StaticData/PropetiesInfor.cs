using UnityEngine;
[CreateAssetMenu(menuName = "Data/PropetiesData", fileName = "PropetiesData")]
public class PropetiesInfor : AHardData<PropetiesData, EPropertiesType>
{
    public override PropetiesData GetData(EPropertiesType type)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i].type != type) continue;
            return datas[i];
        }
        return null;
    }
}
[System.Serializable]
public class PropetiesData
{
    public EPropertiesType type;
    public GameObject prefab;
}
