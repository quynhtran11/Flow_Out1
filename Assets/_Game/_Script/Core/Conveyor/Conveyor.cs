using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
public class Conveyor : BLBMono
{
    [SerializeField] private ConveyorVisual visual;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    private int maxSlot;
    private int currentSlot;
    private bool isBusy;
    private List<ConveyorSlotElement> maxAllSlots = new List<ConveyorSlotElement>();
    private List<ConveyorSlotElement> currentAllSlots = new List<ConveyorSlotElement>();
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
    private void Update()
    {
        if (isBusy) return;
        for (int i = maxAllSlots.Count-1; i >= 0; i--)
        {
            if (maxAllSlots[i] == null) continue;
            maxAllSlots[i].OnUpdate(startPoint.position,endPoint.position);
        }
    }
    private void OnInit()
    {
        isBusy = true;
        visual.OnInit();
        maxSlot = GameData.Instance.InitSlot;
        currentSlot = 0;
        CalculatorSlot();
    }
    private void CalculatorSlot()
    {
        maxAllSlots = new List<ConveyorSlotElement>();
        currentAllSlots = new List<ConveyorSlotElement>();
        float startX = startPoint.position.x;
        float endX = endPoint.position.x;

        float size = endX - startX;
        float offset = size / (maxSlot);

        for (int i = 0; i < maxSlot; i++)
        {
            ConveyorSlotElement conveyorSlot = Instantiate(GameData.Instance.ElementInfor.GetData(EElementType.ConveyorSlot)
                .prefab, transform).GetComponent<ConveyorSlotElement>();

            float posX = startX + offset * 0.5f + offset * i;

            Vector3 pos = new Vector3(
                posX,
                startPoint.position.y,
                startPoint.position.z
            );
            conveyorSlot.OnInit(i);
            conveyorSlot.transform.position = pos;
            maxAllSlots.Add(conveyorSlot);
        }
        isBusy = false;
    }
    private bool IsFullSlot()
    {
        if (currentSlot >= maxSlot) return true;
        return false;
    }
    private ConveyorSlotElement GetConveyorSlotQualified()
    {
        float x = float.MaxValue;
        ConveyorSlotElement c = null;
        for (int i = 0; i < maxAllSlots.Count; i++)
        {
            if (maxAllSlots[i].Tf.position.x <= startPoint.position.x ||
                maxAllSlots[i].IsBusy) continue;
            float distance = maxAllSlots[i].Tf.position.x - startPoint.position.x;
            if (distance < x)
            {
                x = distance;
                c = maxAllSlots[i];
            }
        }
        return c;
    }
    private void OnStartGame(StartGameplayEvent param)
    {
        OnInit();
    }
    private void OnCupToConveyor(CupToConveyorEvent param)
    {
        if (IsFullSlot()) return;
        param.cup.DOKill();
        ConveyorSlotElement c = GetConveyorSlotQualified();
        c.RegisterObject(param.cup);
        Vector3 pos = new Vector3(c.Tf.position.x, c.Tf.position.y + .5f,
            c.Tf.position.z);
        param.cup.MoveToConveyor(pos);
        currentSlot++;
        currentAllSlots.Add(c);
    }
    private void OnCheckFullSlotConveyor(CheckFullSlotConveyorEvent param)
    {
        param.isFullSlot.Invoke(IsFullSlot());
    }
}
