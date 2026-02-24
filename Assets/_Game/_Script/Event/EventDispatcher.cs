using System;
using System.Collections.Generic;
public class EventDispatcher
{
    private static Dictionary<Type, Delegate> eventDictionary = new Dictionary<Type, Delegate>();
    private static Dictionary<Type, Action> eventDictionaryNoParam = new Dictionary<Type, Action>();
    public static void RegisterEvent<T>(Action<T> action) where T : IEvenParam
    {
        Type type = typeof(T);
        if (eventDictionary.ContainsKey(type))
        {
            eventDictionary[type] = Delegate.Combine(eventDictionary[type], action);
        }
        else
        {
            eventDictionary[type] = action;
        }
    }

    public static void RegisterEvent<T>(Action action) where T : IEvenParam
    {
        Type type = typeof(T);
        if (eventDictionaryNoParam.ContainsKey(type))
        {
            eventDictionaryNoParam[type] += action;
        }
        else
        {
            eventDictionaryNoParam[type] = action;
        }
    }

    public static void RemoveEvent<T>(Action<T> action) where T : IEvenParam
    {
        Type type = typeof(T);
        if (eventDictionary.ContainsKey(type))
        {
            Delegate currentDelegate = Delegate.Remove(eventDictionary[type], action);
            if (currentDelegate == null)
            {
                eventDictionary.Remove(type);
            }
            else
            {
                eventDictionary[type] = currentDelegate;
            }
        }
    }

    public static void RemoveEvent<T>(Action action) where T : IEvenParam
    {
        Type type = typeof(T);
        if (eventDictionaryNoParam.ContainsKey(type))
        {
            eventDictionaryNoParam[type] -= action;
            if (eventDictionaryNoParam[type] == null)
            {
                eventDictionaryNoParam.Remove(type);
            }
        }
    }

    public static void Dispatch<T>(T param) where T : IEvenParam
    {
        Type type = typeof(T);
        if (eventDictionary.ContainsKey(type) && eventDictionary[type] is Action<T> action)
        {
            action.Invoke(param);
        }
    }

    public static void Dispatch<T>() where T : IEvenParam
    {
        Type type = typeof(T);
        if (eventDictionaryNoParam.ContainsKey(type))
        {
            eventDictionaryNoParam[type]?.Invoke();
        }
    }

    public static void ClearEvents()
    {
        eventDictionary.Clear();
        eventDictionaryNoParam.Clear();
    }
}