using UnityEngine;

public class BasePropertiesVisual : BLBMono
{
    protected virtual void OnEnable()
    {
        OnRegister();

    }
    protected virtual void OnDisable()
    {
        OnUnregister();
    }
    public virtual void OnInit()
    {
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
