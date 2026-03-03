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

    public int CurrentSlot => currentSlot;
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<StartGameplayEvent>(OnStartGame);
        EventDispatcher.RegisterEvent<CupToConveyorEvent>(OnCupToConveyor);
        EventDispatcher.RegisterEvent<CheckFullSlotConveyorEvent>(OnCheckFullSlotConveyor);
        EventDispatcher.RegisterEvent<ClearCupEvent>(OnClearCup);
        EventDispatcher.RegisterEvent<CheckLoseEvent>(OnCheckLoseEvent);
        EventDispatcher.RegisterEvent<ReviveGameEvent>(OnReviveGame);
        EventDispatcher.RegisterEvent<IncreaseSpeedGameEvent>(OnIncreaseSpeedGame);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<StartGameplayEvent>(OnStartGame);
        EventDispatcher.RemoveEvent<CupToConveyorEvent>(OnCupToConveyor);
        EventDispatcher.RemoveEvent<CheckFullSlotConveyorEvent>(OnCheckFullSlotConveyor);
        EventDispatcher.RemoveEvent<ClearCupEvent>(OnClearCup);
        EventDispatcher.RemoveEvent<CheckLoseEvent>(OnCheckLoseEvent);
        EventDispatcher.RemoveEvent<ReviveGameEvent>(OnReviveGame);
        EventDispatcher.RemoveEvent<IncreaseSpeedGameEvent>(OnIncreaseSpeedGame);
    }
    private void Update()
    {
        if (GameManager.Instance.GameState != EGameState.Playing) return;
        if (isBusy) return;
        for (int i = maxAllSlots.Count - 1; i >= 0; i--)
        {
            if (maxAllSlots[i] == null) continue;
            maxAllSlots[i].OnUpdate(startPoint.position, endPoint.position);
        }
    }
    private void OnInit(LevelInfor level)
    {
        isBusy = true;
        maxSlot = GameData.Instance.InitSlot;
        currentSlot = 0;
        CalculatorSlot();
        visual.OnInit(currentSlot,maxSlot,level.Map.x);
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
                maxAllSlots[i].IsBusy()) continue;
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
        OnInit(param.level);
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
        visual.ChangeTextAmount(currentSlot, maxSlot);
        CheckAllFillQualified(); // check lose
    }
    private void OnCheckFullSlotConveyor(CheckFullSlotConveyorEvent param)
    {
        param.isFullSlot.Invoke(IsFullSlot());
    }
    private void OnClearCup(ClearCupEvent param)
    {
        ConveyorSlotElement c = null;
        for (int i = 0; i < currentAllSlots.Count; i++)
        {
            if (currentAllSlots[i].ObjectOwner == null || currentAllSlots[i].ObjectOwner.Visual != param.cup) continue;
            c = currentAllSlots[i];
            break;
        }
        currentSlot--;
        visual.ChangeTextAmount(currentSlot, maxSlot);
        c.UnRegisterObject();
        currentAllSlots.Remove(c);
    }
    private void CheckAllFillQualified()
    {
        if(!IsFullSlot()) return; // check them dieu kien neu day conveyor nhung co cocs dang dc fill thif phari bo qua 
        List<CupElement> allCups = new List<CupElement>();
        for (int i = 0; i < currentAllSlots.Count; i++)
        {
            allCups.Add(currentAllSlots[i].ObjectOwner);
        }
        EventDispatcher.Dispatch(new CheckAllQualifiedFillEvent()
        {
            cups = allCups
        });
    }
    private void OnCheckLoseEvent(CheckLoseEvent param) 
    {
        CheckAllFillQualified();
    }
    private void OnReviveGame(ReviveGameEvent param)
    {
        Debug.LogError("revive");
        CupElement cup = null;
        ConveyorSlotElement conveyorSlot = null;
        int math = int.MinValue;

        // tim cup dang gan fill het 
        for (int i = 0; i < currentAllSlots.Count; i++)
        {
            if (currentAllSlots[i].ObjectOwner.CurrentWater > math)
            {
                cup = currentAllSlots[i].ObjectOwner;
                math = currentAllSlots[i].ObjectOwner.CurrentWater;
                conveyorSlot = currentAllSlots[i];
            }
        }
        // lay mau cup va clear water va clear cup 
        EventDispatcher.Dispatch(new ReviveStorageEvent()
        {
            cup = cup,
            conveyorSlot = conveyorSlot
        });
    }
    private void OnIncreaseSpeedGame(IncreaseSpeedGameEvent param)
    {
        if (param.amount > maxSlot) return;
        for (int i = 0; i < maxAllSlots.Count; i++)
        {
            maxAllSlots[i].ChangeSpeed(GameData.Instance.SpeedConveyorEndGame);
        }
        EventDispatcher.Dispatch(new IncreaseSpeedWaterEvent() { });
    }
}
