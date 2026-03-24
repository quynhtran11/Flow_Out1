using UnityEngine;
public class CupSkinVisual : BLBMono
{
    [SerializeField] protected SpriteRenderer[] allSkins;
    public void PushSorting()
    {
        if (allSkins == null) return;
        for (int i = 0; i < allSkins.Length; i++)
        {
            if (allSkins[i] == null) continue;
            int s = allSkins[i].sortingOrder + GameData.Instance.DefaulSortingMaskBooster;
            allSkins[i].sortingOrder = s;
        }
    }
    public void PopSorting()
    {
        if (allSkins == null) return;
        for (int i = 0; i < allSkins.Length; i++)
        {
            if (allSkins[i] == null) continue;
            int s = allSkins[i].sortingOrder -GameData.Instance.DefaulSortingMaskBooster;
            allSkins[i].sortingOrder = s;
        }
    }
}
