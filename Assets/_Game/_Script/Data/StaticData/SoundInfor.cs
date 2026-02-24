using UnityEngine;
[CreateAssetMenu(menuName = "Data/SoundData", fileName = "SoundData")]

public class SoundInfor : AHardData<SoundData, ESoundKey>
{
    public override SoundData GetData(ESoundKey type)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i].key != type) continue;
            return datas[i];
        }
        return null;
    }
}
[System.Serializable]
public class SoundData
{
    public ESoundKey key;
    public AudioClip clip;
}
