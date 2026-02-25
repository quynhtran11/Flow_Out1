using System.Collections.Generic;
using UnityEngine;

public abstract class BaseElementService<A>
{
    protected List<A> allElements = new List<A>();
    protected virtual void RegisterEvent() { }
    public void RegisterBlock(A value)
    {
        if (allElements.Contains(value)) return;
        allElements.Add(value);
    }
    public abstract void InitElement(LevelInfor level);
    public BaseElementService()
    {
        RegisterEvent();
    }
}
