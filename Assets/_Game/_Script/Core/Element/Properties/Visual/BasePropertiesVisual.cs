using UnityEngine;

public class BasePropertiesVisual<T> : BLBMono
{
    protected T data;
    protected virtual void OnEnable()
    {
        OnRegister();

    }
    protected virtual void OnDisable()
    {
        OnUnregister();
    }
    public virtual void OnInit(T data) 
    {
        this.data = data;
    }
    public virtual void OnExit() { }
    protected virtual void OnFacePolygonConnectSuccess()
    {
    }
    protected virtual void OnRegister()
    {

    }
    protected virtual void OnUnregister()
    {

    }
}
