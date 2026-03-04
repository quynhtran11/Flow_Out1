using UnityEngine;
[CreateAssetMenu(menuName = "Data/VfxData", fileName = "VfxData")]

public class VFXInfor : AHardData<VfxData, EVfxType>
{
    public override VfxData GetData(EVfxType type)
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
public class VfxData
{
    public EVfxType type;
    public GameObject prefab;
}