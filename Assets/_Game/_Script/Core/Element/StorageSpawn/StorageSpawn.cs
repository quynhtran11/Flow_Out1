using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class StorageSpawn
{
    private float speed;
    private Transform tf;
    private Transform targetEnd;
    private List<WaterElement> allWaters = new List<WaterElement>();
    public List<WaterElement> AllWaters => allWaters;
    private List<Vector3> allPos = new List<Vector3>();
    public StorageSpawn(Transform tf, Transform targetEnd)
    {
        this.tf = tf;
        this.targetEnd = targetEnd;
    }
    public void OnInit(StorageData data)
    {
        SpawnWater(data);
        InitWater();
    }
    public void SpawnWater(StorageData data)
    {
        float spacing = 2.75f;
        float offset = .4f;
        for (int i = 0; i < data.waterDatas.Length; i++)
        {
            WaterElement water = GameObject
                .Instantiate(GameData.Instance.ElementInfor.GetData(EElementType.Water).prefab, tf).GetComponent<WaterElement>();
            water.gameObject.name = "Water_" + i;
            water.Initilize(data.waterDatas[i]);
            //water.Tf.position = // TODO
            float size = spacing * (i);
            Vector3 pos = new Vector3(tf.position.x, (tf.position.y+ size)+(-offset), tf.position.z);
            water.Tf.position = pos;
            allWaters.Add(water);
            allPos.Add(pos);
        }
    }
    public void InitWater()
    {
        for (int i = 0; i < allWaters.Count; i++)
        {
            allWaters[i].OnInit();
            allWaters[i].RegisterTarget(targetEnd);
        }
        speed = Mathf.Clamp(GameData.Instance.GetSpeedWaterFill(), 0, GameData.Instance.GetSpeedWaterFill());
    }
    public void CalculatorPosition(bool isFill = false)
    {
        float delay = 0;
        for (int i = 0; i < allWaters.Count; i++)
        {
            //delay += ((float)i * .05f);
            int index = i;
            allWaters[index].DOKill();
            allWaters[index].Tf.DOMove(allPos[index], speed).SetDelay(delay)/*.SetEase(Ease.OutBack)*/;
        }
        if (allWaters.Count <= 0) return;
        var value = allWaters[0];
        if (!isFill) return;
        Debug.LogError("ClearSuccessWaterEvent");
        EventDispatcher.Dispatch(new ClearSuccessWaterEvent()
        {
            water = value
        });
    }
    public void ClearWater(WaterElement water)
    {
        if (allWaters == null || allWaters.Count <= 0) return;
        if (!allWaters.Contains(water)) return;
        allWaters.Remove(water);
    }
    public void ChangeSpeedWater()
    {
        for (int i = 0; i < allWaters.Count; i++)
        {
            allWaters[i].ChangeSpeedWater();
        }
        speed = Mathf.Clamp(GameData.Instance.GetSpeedWaterFill(), 0, GameData.Instance.GetSpeedWaterFill());
    }
}