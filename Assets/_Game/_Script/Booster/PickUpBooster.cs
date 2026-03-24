using System;
using UnityEngine;

public class PickUpBooster : IBooster
{
    public EBoosterType boosterType => EBoosterType.PickUp;

    

    public void OnExit()
    {
    }

    public void OnInit()
    {
        EventDispatcher.Dispatch(new ActiveMaskBoosterEvent() { isActive = true });
        EventDispatcher.Dispatch(new PrePickUpEvent() { });
    }
    public void OnUpdate(Action callBack)
    {
        if (Input.GetMouseButtonUp(0))
        {
            ClickCup(callBack);
        }
    }

    public void OnClick()
    {
    }
    private void ClickCup(Action callBack)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 lastClickPos = hit.point;
            Collider[] colliders = Physics.OverlapSphere(lastClickPos, GameData.Instance.ClickRadius);

            CupElement nearestBlock = null;
            float nearestSqrDistance = float.MaxValue;
            foreach (var col in colliders)
            {
                CupElement block = col.GetComponent<CupElement>();
                if (block == null) continue;

                float sqrDist = (lastClickPos - col.transform.position).sqrMagnitude;

                if (sqrDist < nearestSqrDistance)
                {
                    nearestBlock = block;
                    nearestSqrDistance = sqrDist;
                }
            }
            if (nearestBlock == null) return;
            EventDispatcher.Dispatch(new UsePickUpEvent()
            {
                cup = nearestBlock
            });
            callBack?.Invoke();
            return;
        }
    }
}