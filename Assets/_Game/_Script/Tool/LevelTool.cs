using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
public partial class LevelTool : MonoBehaviour
{
    public float offsetStorage = 4f;
    private void LoadSpawnCup(CupData[] datas)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            if (datas[i].hiddenData.isHidden)
            {
                GameObject hid = ToolVisual.HiddenWaterPrefab();
                hid.transform.position = new Vector2(datas[i].pos.x, datas[i].pos.y);
            }
        }
    }
    private void LoadSpawnWater(StorageData[] datas)
    {
        for (int i = 0; i < datas.Length; i++)
        {
            for (int j = 0; j < datas[i].waterDatas.Length; j++)
            {
                float x = (int)i * (int)offsetStorage;
                float y = (datas[i].waterDatas[j].waterID + 1) * 4;
                Vector2 pos = new Vector2(x, y);
                if (datas[i].waterDatas[j].hiddenData.isHidden)
                {
                    GameObject hid = ToolVisual.HiddenWaterPrefab();
                    hid.transform.position = new Vector2(pos.x, pos.y);
                }
                if (datas[i].waterDatas[j].freezeData.amount > 0)
                {
                    GameObject hid = ToolVisual.FreezeWaterPrefab();
                    hid.transform.position = new Vector2(pos.x, pos.y);
                    WaterFreezePropertiesVisual v = hid.GetComponent<WaterFreezePropertiesVisual>();
                    v.Text.text = datas[i].waterDatas[j].freezeData.amount.ToString();
                }
            }
        }
    }
    public void Load(int index)
    {
        Clear();
        LevelInfor lv = JsonManager.LoadLevelInfor(index);
        allCups = lv.AllCups.ToList();
        allStorages = lv.AllStorages.ToList();
        for (int i = 0; i < lv.AllCups.Length; i++)
        {
            Spawn(toolVisual.CupPrefab(), lv.AllCups[i].pos, lv.AllCups[i].color, "Cup_" + lv.AllCups[i].id);
        }
        for (int i = 0; i < lv.AllStorages.Length; i++)
        {
            Vector2 pos = new Vector2(i * offsetStorage, 10);
            Spawn(toolVisual.StoragePrefab(), pos, EColorType.None, "Storage_" + i);
            for (int j = 0; j < lv.AllStorages[i].waterDatas.Length; j++)
            {
                Vector2 posWater = new Vector2(pos.x, (j + 1) * 4);
                Spawn(toolVisual.WaterPrefab(), posWater, lv.AllStorages[i].waterDatas[j].color, "Water_" + j);
            }
        }
        LoadSpawnCup(lv.AllCups);
        LoadSpawnWater(lv.AllStorages);
    }
    public void Save(int index, EModeType type)
    {
        LevelInfor lv = new LevelInfor();
        lv.LevelID = index;
        lv.Mode = type;
        List<CupData> allCupsNew = new List<CupData>();
        for (int i = 0; i < this.allCups.Count; i++)
        {
            CupData cup = allCups[i];
            cup.id = i;
            allCupsNew.Add(cup);
        }
        int x = 0;
        int y = 0;
        Dictionary<float, float> allX = new Dictionary<float, float>();
        Dictionary<float, float> allY = new Dictionary<float, float>();
        for (int i = 0; i < allCupsNew.Count; i++)
        {
            if (!allX.ContainsKey(allCupsNew[i].pos.x))
            {
                x++;
                allX.Add(allCupsNew[i].pos.x, 0);
            }
            if (!allY.ContainsKey(allCupsNew[i].pos.y))
            {
                y++;
                allY.Add(allCupsNew[i].pos.y, 0);
            }
        }
        lv.Map = new Vector2Int(x, y);
        lv.AllCups = allCupsNew.ToArray();
        lv.AllStorages = this.allStorages.ToArray();
        JsonManager.SaveLevelInfor(lv);
    }
    public void Clear()
    {
        GameObject[] allCup = GameObject.FindGameObjectsWithTag("Cup");
        for (int i = 0; i < allCup.Length; i++)
        {
            DestroyImmediate(allCup[i]);
        }
        allCups = new List<CupData>();

        GameObject[] allStorage = GameObject.FindGameObjectsWithTag("Storage");
        for (int i = 0; i < allStorage.Length; i++)
        {
            DestroyImmediate(allStorage[i]);
        }
        allStorages = new List<StorageData>();


        GameObject[] allWaters = GameObject.FindGameObjectsWithTag("Water");
        for (int i = 0; i < allWaters.Length; i++)
        {
            DestroyImmediate(allWaters[i]);
        }
        int index = toolVisual.transform.childCount;
        Debug.LogError("index_" + index);
        List<Transform> allTf = new List<Transform>();
        for (int i = 0; i < index; i++)
        {
            int indexOf = i;
            Transform value = toolVisual.transform.GetChild(indexOf).GetComponent<Transform>();
            allTf.Add(value);
        }
        foreach (var item in allTf)
        {
            DestroyImmediate(item.gameObject);
        }
    }
    public Dictionary<EColorType, int> GetCupAmount()
    {
        Dictionary<EColorType, int> dic = new Dictionary<EColorType, int>();
        for (int i = 0; i < allCups.Count; i++)
        {
            if (dic.ContainsKey(allCups[i].color))
            {
                dic[allCups[i].color]++;
            }
            else
            {
                dic.Add(allCups[i].color, 1);
            }
        }
        return dic;
    }
    public Dictionary<EColorType, int> GetWaterAmount()
    {
        Dictionary<EColorType, int> dic = new Dictionary<EColorType, int>();
        for (int i = 0; i < allStorages.Count; i++)
        {
            for (int j = 0; j < allStorages[i].waterDatas.Length; j++)
            {
                if (dic.ContainsKey(allStorages[i].waterDatas[j].color))
                {
                    dic[allStorages[i].waterDatas[j].color]++;
                }
                else
                {
                    dic.Add(allStorages[i].waterDatas[j].color, 1);
                }
            }
        }
        return dic;
    }
    //public Dictionary<EColorType, int> GetWaterAmount()
    //{
    //    Dictionary<EColorType, int> dic = new Dictionary<EColorType, int>(GetCupAmount());
    //    Dictionary<EColorType, int> dicAll = new Dictionary<EColorType, int>();
    //    foreach (var w in dic)
    //    {
    //        int value = w.Value * 3;
    //        dicAll.Add(w.Key, value);
    //    }
    //    return dicAll;
    //}
    public bool IsEditCup()
    {
        return isEditCup;
    }
    public bool IsEditStorage()
    {
        return isEditStorage;
    }
    public bool IsEditWater()
    {
        return isEditWater;
    }
    public void SpawnPerCup(Vector2 pos, bool isCup)
    {
        if (allCups.Count <= 0)
        {
            Vector2 posNew = Vector2.zero;
            SpawnCup(new Vector2Int((int)posNew.x, (int)posNew.y), 0);
        }
        else
        {
            if (isCup)
            {
                Vector2 posNew = new Vector2(pos.x, pos.y - 3);
                if (IsCup(posNew)) return;
                SpawnCup(new Vector2Int((int)posNew.x, (int)posNew.y), allCups.Count);
            }
        }
    }
    public void SpawnCup(Vector2Int pos, int id)
    {
        CupData cup = new CupData();
        cup.id = id;
        cup.color = cupColor;
        cup.pos = pos;
        allCups.Add(cup);
        Spawn(toolVisual.CupPrefab(), pos, cupColor, $"Cup_{id}");
    }
    public void SpawnStorage(int id)
    {
        StorageData storage = new StorageData();
        storage.id = id;
        allStorages.Add(storage);
        Spawn(toolVisual.StoragePrefab(), new Vector2(id * offsetStorage, 10f), EColorType.None, $"Storage_{id}");
    }
    public void SpawnPerWater(int id, Vector2 pos)
    {
        WaterData water = new WaterData();
        water.waterID = id;
        int idGroup = (int)pos.x / (int)offsetStorage;

        water.waterGroupID = idGroup;
        water.color = waterColor;
        List<WaterData> allWaters = new List<WaterData>();
        StorageData storage = new StorageData();
        for (int i = 0; i < allStorages.Count; i++)
        {
            if (allStorages[i].id != idGroup) continue;
            storage = allStorages[i];
            allWaters = storage.waterDatas.ToList();
            allWaters.Add(water);
            storage.waterDatas = allWaters.ToArray();
            allStorages[i] = storage;
        }
        Spawn(toolVisual.WaterPrefab(), new Vector2(pos.x, storage.waterDatas.Length * 4), waterColor, $"Water_{id}");
    }
    public void RemoveCup(GameObject pos)
    {
        int index = -1;
        for (int i = 0; i < allCups.Count; i++)
        {
            if (Vector2.Distance(pos.gameObject.transform.position, allCups[i].pos) < .1f)
            {
                index = i; break;
            }
        }
        if (index < 0) return;
        allCups.RemoveAt(index);
        DestroyImmediate(pos);
    }
    public void RemoveWater(GameObject pos)
    {
        int idGroup = (int)pos.transform.position.x / (int)offsetStorage;
        int id = ((int)pos.transform.position.y / 4) - 1;
        List<WaterData> allWater = AllStorages[idGroup].waterDatas.ToList();
        allWater.RemoveAt(id);
        StorageData storage = allStorages[idGroup];
        storage.waterDatas = allWater.ToArray();
        AllStorages[idGroup] = storage;
        DestroyImmediate(pos);
        UpdateStatStorage();
    }
    public void RemoveStorage(GameObject pos)
    {
        int index = -1;
        for (int i = 0; i < allStorages.Count; i++)
        {
            float x = i * offsetStorage;
            Vector2 posNew = new Vector2(x, 10);
            if (Vector2.Distance(pos.gameObject.transform.position, posNew) < .1f)
            {
                index = i; break;
            }
        }
        if (index < 0) return;
        allStorages.RemoveAt(index);
        RemoveWaterForStorage(pos);
        UpdateStatStorage();
        DestroyImmediate(pos); // call last
    }
    private void RemoveWaterForStorage(GameObject pos)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Water");
        if (gos == null || gos.Length <= 0 || pos == null) return;
        List<GameObject> go = new List<GameObject>();
        for (int i = 0; i < gos.Length; i++)
        {
            if (Mathf.Abs(gos[i].transform.position.x - pos.transform.position.x) <= .1f)
            {
                go.Add(gos[i]);
            }
        }
        foreach (var item in go)
        {
            DestroyImmediate(item);
        }
    }
    public Vector2Int GetAutoPosCup()
    {
        if (allCups.Count <= 0)
        {
            return Vector2Int.zero;
        }
        float t = float.MinValue;
        for (int i = 0; i < allCups.Count; i++)
        {
            if (allCups[i].pos.x > t)
            {
                t = allCups[i].pos.x;
            }
        }
        return new Vector2Int((int)(t + 3), 0);
    }
    private bool IsCup(Vector2 pos)
    {
        for (int i = 0; i < allCups.Count; i++)
        {
            if (Vector2.Distance(allCups[i].pos, pos) <= .1f) return true;
        }
        return false;
    }
    private void UpdateStatStorage()
    {
        for (int i = 0; i < allStorages.Count; i++)
        {
            StorageData storage = allStorages[i];
            storage.id = i;
            allStorages[i] = storage;

            for (int j = 0; j < allStorages[i].waterDatas.Length; j++)
            {
                WaterData water = allStorages[i].waterDatas[j];
                water.waterID = j;
                allStorages[i].waterDatas[j] = water;
            }
        }
    }
    private void Spawn(GameObject prefab, Vector2 pos, EColorType color, string name)
    {
#if UNITY_EDITOR
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        obj.gameObject.SetActive(true);
        obj.transform.position = pos;
        obj.gameObject.name = name;
        if (color == EColorType.None) return;
        SpriteRenderer icon = obj.GetComponent<SpriteRenderer>();
        icon.color = GameData.Instance.ColorData.GetData(color).color;
#endif
    }
}
