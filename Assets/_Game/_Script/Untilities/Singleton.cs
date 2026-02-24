using UnityEngine;

public abstract class Singleton<T> : BLBMono where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<T>();
                if (instance == null)
                {
                    var obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected abstract bool dondestroy { get; }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            if (dondestroy)
                DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject); 
            return;
        }
    }
}
