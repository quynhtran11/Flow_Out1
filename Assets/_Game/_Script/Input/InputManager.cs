using UnityEngine;

public class InputManager : BLBMono
{
    private bool showClickGizmo = false;
    private bool isTouch;
    private Vector3 lastClickPos;
    private float timePress;
    private float maxTimePress;
    private bool isPress;
    private bool endGame;
    private void OnEnable()
    {
        timePress = 0;
        maxTimePress = 1;
        endGame = false;
        EventDispatcher.RegisterEvent<IncreaseSpeedWaterEvent>(OnIncreaseSpeedGame);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<IncreaseSpeedWaterEvent>(OnIncreaseSpeedGame);
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ClickObject();
            UnPressScreen();
        }
        if (Input.GetMouseButton(0))
        {
            PressScreen();
        }
    }
    private void OnIncreaseSpeedGame(IncreaseSpeedWaterEvent param)
    {
        endGame = true;
    }
    private void PressScreen()
    {
        if (endGame) return;
        timePress += Time.deltaTime;
        if (timePress >= maxTimePress && !isPress)
        {
            EventDispatcher.Dispatch(new IncreaseSpeedGameForTapEvent()
            {
                isAddSpeed = true
            });
            isPress = true;
        }
    }
    private void UnPressScreen()
    {
        if (endGame) return;
        if (!isPress) return;
        EventDispatcher.Dispatch(new IncreaseSpeedGameForTapEvent()
        {
            isAddSpeed = false
        });
        timePress = 0;
        isPress = false;
        Debug.LogError("2pressFalse");
    }

    private void ClickObject()
    {
        showClickGizmo = true;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            lastClickPos = hit.point;

            Collider[] colliders = Physics.OverlapSphere(
                lastClickPos,
                GameData.Instance.ClickRadius
            );

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

            if (nearestBlock != null)
            {
                bool isFull = false;
                EventDispatcher.Dispatch(new CheckFullSlotConveyorEvent()
                {
                    isFullSlot = (x) =>
                    {
                        isFull = x;
                    }
                });
                if (nearestBlock.IsBusy || isFull)
                {
                    Debug.LogError("touchFail");
                    EventDispatcher.Dispatch(new TouchFailedCupEvent()
                    {
                        cup = nearestBlock,
                    });
                }
                else
                {
                    EventDispatcher.Dispatch(new TouchSuccessCupEvent()
                    {
                        cup = nearestBlock,
                    });
                    Debug.LogError("touchSuccess");
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (showClickGizmo)
        {
            Gizmos.color = new Color(0, 1, 0, 0.35f);
            Gizmos.DrawSphere(lastClickPos, GameData.Instance.ClickRadius);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(lastClickPos, GameData.Instance.ClickRadius);
        }
    }
}
