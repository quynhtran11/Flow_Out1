using System.Collections.Generic;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    private Dictionary<EVfxType, Queue<ParticleSystem>> allVfx = new Dictionary<EVfxType, Queue<ParticleSystem>>();

    protected override bool dondestroy => true;
    private void OnEnable()
    {
        EventDispatcher.RegisterEvent<ChangeSceneEvent>(OnChangeScene);
    }
    private void OnDisable()
    {
        EventDispatcher.RemoveEvent<ChangeSceneEvent>(OnChangeScene);
    }
    private void Start()
    {
        OnInit();
    }
    private void OnChangeScene(ChangeSceneEvent param)
    {
        foreach(var ob in allVfx)
        {
            int count = ob.Value.Count;
            for (int i = 0; i < count; i++)
            {
                var v = ob.Value.Dequeue();
                if (v.gameObject.activeSelf)
                {
                    ReturnObject(ob.Key, v);
                }
            }
        }
    }
    public ParticleSystem GetObject(EVfxType vfxType)
    {
        if (!allVfx.ContainsKey(vfxType)) return null;
        ParticleSystem go = null;
        if (allVfx[vfxType].Count <= 0)
        {
            var value = GameData.Instance.VfxInfor;
            go = SpawnObject(value.GetData(vfxType).prefab).GetComponent<ParticleSystem>();
        }
        else
        {
            go = allVfx[vfxType].Dequeue();
            if (go == null) return null;
            go.gameObject.SetActive(true);
        }
        return go;
    }
    public void ReturnObject(EVfxType vfxType, ParticleSystem go)
    {
        allVfx[vfxType].Enqueue(go);
        go.gameObject.SetActive(false);
    }
    private void OnInit()
    {
        var value = GameData.Instance.VfxInfor;
        for (int i = 0; i < value.Data.Length; i++)
        {
            Queue<ParticleSystem> obj = new Queue<ParticleSystem>();
            for (int j = 0; j < 5; j++)
            {
                ParticleSystem go = SpawnObject(value.Data[i].prefab).GetComponent<ParticleSystem>();
                obj.Enqueue(go);
                go.gameObject.SetActive(false);
            }
            if (obj.Count <= 0) continue;
            allVfx.Add(value.Data[i].type, obj);
        }
    }
    private GameObject SpawnObject(GameObject prefab)
    {
        GameObject gObj = Instantiate(prefab,transform);
        return gObj;
    }

}
