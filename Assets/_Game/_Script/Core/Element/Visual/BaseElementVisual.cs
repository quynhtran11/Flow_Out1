using UnityEngine;

public abstract class BaseElementVisual<R> : BLBMono
{
    protected R data;
    protected Vector3 centerPos;

    [SerializeField] protected Transform skin;
    [SerializeField] protected Collider elementCollider;
    public Vector3 CenterPos => centerPos;
    public Collider ElementCollider => elementCollider;
    public virtual void OnInit(R data)
    {
        this.data = data;
    }
    public virtual void ActivePhysict(bool isActive) { }
    public virtual void SetBusy(bool isActive) { }
    public virtual void SetCenterPos(Vector3 center)
    {
        this.centerPos = center;
        AfterInit();
    }
    public abstract void AfterInit();
}
