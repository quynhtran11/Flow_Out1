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
    private bool isPause;
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
        EventDispatcher.RegisterEvent<AddSlotEvent>(OnAddSlot);
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
        EventDispatcher.RemoveEvent<AddSlotEvent>(OnAddSlot);
    }
    private void Update()
    {
        if (GameManager.Instance.GameState != EGameState.Playing) return;
        if (isBusy || isPause) return;
        for (int i = maxAllSlots.Count - 1; i >= 0; i--)
        {
            if (maxAllSlots[i] == null) continue;
            maxAllSlots[i].OnUpdate(startPoint.position, endPoint.position);
        }
    }
    private void OnInit(LevelInfor level)
    {
        isBusy = true;
        isPause = false;
        maxSlot = GameData.Instance.InitSlot;
        currentSlot = 0;
        CalculatorSlot();
        visual.OnInit(currentSlot, maxSlot, level.Map.x);
    }
    private ConveyorSlotElement SpawnConveyor()
    {
        ConveyorSlotElement conveyorSlot = Instantiate(GameData.Instance.ElementInfor.GetData(EElementType.ConveyorSlot)
    .prefab, transform).GetComponent<ConveyorSlotElement>();
        return conveyorSlot;
    }
    private void CalculatorSlot()
    {
        maxAllSlots = new List<ConveyorSlotElement>();
        currentAllSlots = new List<ConveyorSlotElement>();
        float startX = startPoint.position.x;
        float endX = endPoint.position.x;

        float size = endX - startX;
        float offset = size / (maxSlot);
        float delay = .1f;
        for (int i = 0; i < maxSlot; i++)
        {
            ConveyorSlotElement conveyorSlot = SpawnConveyor();

            float posX = startX + offset * 0.5f + offset * i;

            Vector3 pos = new Vector3(
                posX,
                startPoint.position.y,
                startPoint.position.z
            );
            conveyorSlot.OnInit(i,delay*i);
            conveyorSlot.transform.position = pos;
            maxAllSlots.Add(conveyorSlot);
        }
        isBusy = false;
    }
    private void CaculatorConveyorSlot()
    {
        maxSlot ++;
        isPause = true;
        ConveyorSlotElement slot = SpawnConveyor();
        maxAllSlots.Add(slot);
        slot.OnInit(maxSlot,0);

        float startX = startPoint.position.x;
        float endX = endPoint.position.x;

        float size = endX - startX;
        float offset = size / (maxSlot);
        List<Vector3> allPos = new List<Vector3>();
        for (int i = 0; i < maxAllSlots.Count; i++)
        {
            float posX = startX + offset * 0.5f + offset * i;
            Vector3 pos = new Vector3(posX,startPoint.position.y,startPoint.position.z);
            allPos.Add(pos);
            //maxAllSlots[i].Tf.position = pos;
        }
        // check vi tri slot gan bang voi conveyor xong roi domove
        // 
        Dictionary<ConveyorSlotElement, Vector3> dicConveyor = new Dictionary<ConveyorSlotElement, Vector3>();
        List<ConveyorSlotElement> slotTemps = new List<ConveyorSlotElement>(maxAllSlots);
        for (int i = 0; i < allPos.Count; i++)
        {
            ConveyorSlotElement c = null;
            float distance = float.MaxValue;
            int count = slotTemps.Count;
            for (int j = 0; j < count; j++)
            {
                float t = Mathf.Abs(allPos[i].x - slotTemps[j].Tf.position.x);
                if ( t< distance)
                {
                    c = slotTemps[j];
                    distance = t;
                }
            }
            slotTemps.Remove(c);
            dicConveyor.Add(c, allPos[i]);
        }
        // set vi tri tu dic
        int current = 0;
        foreach (var item in dicConveyor)
        {
            item.Key.Tf.DOMove(item.Value, .3f).OnComplete(() =>
            {
                if (current >= dicConveyor.Count)
                {
                    isPause = false;
                }

            });
            current++;
        }
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
        if (currentSlot == (maxSlot - 1))
        {
            // warning
            WarningConveyor();
        }
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
        if (!IsFullSlot()) return; // check them dieu kien neu day conveyor nhung co cocs dang dc fill thif phari bo qua 
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
            maxAllSlots[i].ChangeSpeed(GameData.Instance.GetSpeedConveyor());
        }
        EventDispatcher.Dispatch(new IncreaseSpeedWaterEvent() { });
    }
    private void WarningConveyor()
    {
        for (int i = 0; i < maxAllSlots.Count; i++)
        {
            maxAllSlots[i].WarningConveyor();
        }
    }
    private void OnAddSlot(AddSlotEvent param)
    {
        CaculatorConveyorSlot();
        visual.AddSlot(currentSlot,maxSlot);
    }
}
