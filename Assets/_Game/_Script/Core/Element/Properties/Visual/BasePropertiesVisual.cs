using UnityEngine;

public class BasePropertiesVisual<T> : BLBMono
{
    protected T data;
    protected bool isBusy = false;
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
        isBusy = false;
    }
    public virtual void OnExit() { 
        isBusy = true;
    }
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
