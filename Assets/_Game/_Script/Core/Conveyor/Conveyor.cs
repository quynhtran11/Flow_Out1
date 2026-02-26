using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ConveyorSlot
{
    public int idSlot;
    public Vector3 slotPostion;
}
public class Conveyor : BLBMono
{
    [SerializeField] private ConveyorVisual visual;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    private int maxSlot;
    private int currentSlot;
    private List<CupElement> allItems = new List<CupElement>();
    private List<ConveyorSlot> allSlots = new List<ConveyorSlot>();
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<StartGameplayEvent>(OnStartGame);
        EventDispatcher.RegisterEvent<CupToConveyorEvent>(OnCupToConveyor);
        EventDispatcher.RegisterEvent<CheckFullSlotConveyorEvent>(OnCheckFullSlotConveyor);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<StartGameplayEvent>(OnStartGame);
        EventDispatcher.RemoveEvent<CupToConveyorEvent>(OnCupToConveyor);
        EventDispatcher.RemoveEvent<CheckFullSlotConveyorEvent>(OnCheckFullSlotConveyor);
    }
    private void OnInit()
    {
        maxSlot = GameData.Instance.InitSlot;
        currentSlot = 0;
        allItems = new List<CupElement>();
        CalculatorSlot();
    }
    private void CalculatorSlot()
    {
        allSlots = new List<ConveyorSlot>();
        float size = endPoint.position.x - startPoint.position.x;
        float offset = size / (float)maxSlot;
        for (int i = 0; i < maxSlot; i++)
        {
            ConveyorSlot conveyorSlot = new ConveyorSlot();
            conveyorSlot.idSlot = i;
            float posX = startPoint.position.x + (offset * (i));
            Vector3 pos = new Vector3(posX, endPoint.position.y,endPoint.position.z);
            conveyorSlot.slotPostion = pos;
            allSlots.Add(conveyorSlot);
        }
    }
    private bool IsFullSlot()
    {
        if (currentSlot >= maxSlot) return true;
        return false;
    }
    private void OnStartGame(StartGameplayEvent param)
    {
        OnInit();
    }
    private void OnCupToConveyor(CupToConveyorEvent param)
    {
        if (allItems.Contains(param.cup) || IsFullSlot()) return;
        param.cup.DOKill();
        param.cup.transform.DOMove(allSlots[currentSlot].slotPostion, .4f); // test
        currentSlot++;
        allItems.Add(param.cup);
    }
    private void OnCheckFullSlotConveyor(CheckFullSlotConveyorEvent param)
    {
        param.isFullSlot.Invoke(IsFullSlot());
    }
}
