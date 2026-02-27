using UnityEngine;
[CreateAssetMenu(menuName = "Data/ElementData", fileName = "ElementData")]

public class ElementInfor : AHardData<ElementData, EElementType>
{
    public override ElementData GetData(EElementType type)
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
public class ElementData
{
    public EElementType type;
    public GameObject prefab;
}