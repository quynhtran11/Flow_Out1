using UnityEngine;

public abstract class AHardData<T,R> : ScriptableObject
{
    [SerializeField] protected T[] datas;
    public abstract T GetData(R type);
}
