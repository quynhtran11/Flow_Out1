using UnityEngine;
[CreateAssetMenu(menuName = "Data/ColorData", fileName = "ColorData")]

public class ColorInfor : AHardData<ColorData, EColorType>
{
    public override ColorData GetData(EColorType type)
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
public class ColorData
{
    public EColorType type;
    public Color color;
}
