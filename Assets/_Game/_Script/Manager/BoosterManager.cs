using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterManager : BLBMono
{
    private List<IBooster> allBooster = new List<IBooster>();
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<ClickBoosterGuidEvent>(OnClickBoosterGuid);
        EventDispatcher.RegisterEvent<ExitBoosterGuidEvent>(OnExitBoosterGuid);

    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<ClickBoosterGuidEvent>(OnClickBoosterGuid);
        EventDispatcher.RemoveEvent<ExitBoosterGuidEvent>(OnExitBoosterGuid);

    }
    private void Update()
    {
        for (int i = allBooster.Count-1; i < 0; i--)
        {
            int index = i;
            if (allBooster.Count <= 0) return;
            if (index >= allBooster.Count) continue;
            if (allBooster[index] == null) continue;
            allBooster[index].OnUpdate(() =>
            {

            });
        }
    }
    private void OnClickBoosterGuid(ClickBoosterGuidEvent param)
    {
        StopAllCoroutines();
        StartCoroutine(AddBooster(param.data.type));
    }
    private void OnExitBoosterGuid(ExitBoosterGuidEvent param)
    {
        for (int i = 0; i < allBooster.Count; i++)
        {
            int index = i;
            allBooster[index].OnExit();
        }
        allBooster.Clear();
        //EventDispatcher.Dispatch(new ContinueGameEvent() { });
    }
    private IEnumerator AddBooster(EBoosterType boosterType)
    {
        yield return null;
        yield return new WaitForSeconds(.1f);
        IBooster booster = FactoryBooster(boosterType);
        booster.OnInit();
        allBooster.Add(booster);
    }
    private IBooster FactoryBooster(EBoosterType boosterType)
    {
        IBooster booster = null;
        switch (boosterType)
        {
            case EBoosterType.Shuffle:
                booster = new ShuffleBooster();
                break;
            case EBoosterType.InstantFill:
                booster = new InstantFillBooster();
                break;
            case EBoosterType.PickUp:
                booster = new PickUpBooster();
                break;
        }
        return booster;
    }
}
