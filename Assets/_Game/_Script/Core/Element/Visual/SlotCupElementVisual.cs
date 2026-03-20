using DG.Tweening;
using UnityEngine;

public class SlotCupElementVisual : BLBMono
{
    [SerializeField] private SpriteRenderer skin;
    [SerializeField] private Color baseColor;
    public void OnInit()
    {
        skin.color = baseColor;
    }

    public void CupPlaceSlot(EColorType type)
    {
        skin.DOKill();
        Color c = GameData.Instance.ColorData.GetData(type).color;
        skin.DOColor(c, .15f).OnComplete(() =>
        {
            skin.DOColor(baseColor, .15f);
        });
    }
}
