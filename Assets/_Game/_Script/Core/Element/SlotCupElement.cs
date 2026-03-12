using UnityEngine;

public class SlotCupElement : BLBMono
{
    [SerializeField] private SlotCupElementVisual visual;

    public SlotCupElementVisual Visual => visual;
    public void OnInit()
    {
        visual.OnInit();
    }
}
