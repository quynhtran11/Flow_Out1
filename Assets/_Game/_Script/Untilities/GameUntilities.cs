using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class GameUntilities
{
    public static bool IsPointerOverUI()
    {
        if (EventSystem.current == null) return false;

#if UNITY_EDITOR
        return EventSystem.current.IsPointerOverGameObject();
#else
    for (int i = 0; i < Input.touchCount; i++)
    {
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId))
            return true;
    }
    return false;
#endif
    }
    public static float SizeMap(CupData[] datas)
    {
        float size = float.MinValue;
        for (int i = 0; i < datas.Length; i++)
        {
            float v = datas[i].pos.x;
            if (v > size)
            {
                size = v;
            }
        }
        return size;
    }
}
