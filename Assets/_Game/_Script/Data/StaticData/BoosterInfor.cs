using UnityEngine;
[CreateAssetMenu(menuName = "Data/BoosterData", fileName = "BoosterData")]

public class BoosterInfor : AHardData<BoosterData, EBoosterType>
{
    public override BoosterData GetData(EBoosterType type)
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
public class BoosterData
{
    public GameObject visual;
    public Sprite icon;
    public EBoosterType type;
    public int levelUnlock;
    public string desc;

}
