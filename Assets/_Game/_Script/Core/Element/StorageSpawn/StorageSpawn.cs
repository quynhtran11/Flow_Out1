using System.Collections.Generic;
using UnityEngine;

public class StorageSpawn
{
    private Transform tf;
    private List<WaterElement> allWaters = new List<WaterElement>();
    public List<WaterElement> AllWaters => allWaters;
    public StorageSpawn(Transform tf)
    {
        this.tf = tf;
    }
    public void OnInit(StorageData data)
    {
        SpawnWater(data);
        InitWater();
    }
    public void SpawnWater(StorageData data)
    {
        float spacing = 2;
        for (int i = 0; i < data.waterDatas.Length; i++)
        {
            WaterElement water = GameObject
                .Instantiate(GameData.Instance.ElementInfor.GetData(EElementType.Water).prefab,tf).GetComponent<WaterElement>();
            water.gameObject.name = "Water_" + i;
            water.Initilize(data.waterDatas[i]);
            //water.Tf.position = // TODO
            float size = spacing * i;
            water.Tf.position = new Vector3(tf.position.x, tf.position.y, tf.position.z + size);
            allWaters.Add(water);
        }
    }
    public void InitWater()
    {
        for (int i = 0; i < allWaters.Count; i++)
        {
            allWaters[i].OnInit();
        }
    }
}