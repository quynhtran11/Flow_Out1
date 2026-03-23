using UnityEngine;

public class BasePropertiesVisual<T,R> : BLBMono
{
    protected T data;
    protected R pro;
    protected bool isBusy = false;
    protected virtual void OnEnable()
    {
        OnRegister();

    }
    protected virtual void OnDisable()
    {
        OnUnregister();
    }
    public virtual void OnInit(T data,R pro) 
    {
        this.data = data;
        this.pro = pro;
        isBusy = false;
    }
    public virtual void OnExit() { 
        isBusy = true;
    }
    protected virtual void OnRegister()
    {

    }
    protected virtual void OnUnregister()
    {

    }
}
