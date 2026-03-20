using UnityEngine;

public class SlotCupElement : BLBMono
{
    [SerializeField] private SlotCupElementVisual visual;
    private Vector2Int map;
    public SlotCupElementVisual Visual => visual;
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<CupPlaceSlotEvent>(OnCupPlaceSlot);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<CupPlaceSlotEvent>(OnCupPlaceSlot);
    }
    private void OnCupPlaceSlot(CupPlaceSlotEvent param) {
        if (param.map != map) return;
        if (param.map.y > 0) return;
        visual.CupPlaceSlot(param.colorType);
    }
    public void OnInit(Vector2Int map)
    {
        visual.OnInit();
        this.map = map;
    }
}
