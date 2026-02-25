using UnityEngine;

public class InputManager : BLBMono
{
    private bool showClickGizmo = false;
    private bool isTouch;
    private Vector3 lastClickPos;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ClickObject();
        }
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

            BlockElement nearestBlock = null;
            float nearestSqrDistance = float.MaxValue;

            foreach (var col in colliders)
            {
                BlockElement block = col.GetComponent<BlockElement>();
                if (block == null || block.IsBusy) continue;

                float sqrDist = (lastClickPos - col.transform.position).sqrMagnitude;

                if (sqrDist < nearestSqrDistance)
                {
                    nearestBlock = block;
                    nearestSqrDistance = sqrDist;
                }
            }

            if (nearestBlock != null)
            {
                EventDispatcher.Dispatch(new TouchSuccessBlockEvent()
                {
                    block = nearestBlock,
                });
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
