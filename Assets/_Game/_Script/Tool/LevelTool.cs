using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public partial class LevelTool : MonoBehaviour
{
    public void Load(int index)
    {
        Clear();
        LevelInfor lv = JsonManager.LoadLevelInfor(index);
        allCups = lv.AllCups.ToList();
        Debug.LogError(lv.Map);
        for (int i = 0; i < lv.AllCups.Length; i++)
        {
            Spawn(toolVisual.CupPrefab(), lv.AllCups[i].pos, lv.AllCups[i].color, "Cup_" + lv.AllCups[i].id);
        }
    }
    public void Save(int index, EModeType type)
    {
        LevelInfor lv = new LevelInfor();
        lv.LevelID = index;
        lv.Mode = type;
        lv.AllCups = allCups.ToArray();
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
        int index = toolVisual.transform.childCount;
        for (int i = 0; i < index; i++)
        {
            Transform value = toolVisual.transform.GetChild(i).GetComponent<Transform>();
            DestroyImmediate(value.gameObject);
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
    public bool IsEditCup()
    {
        return isEditCup;
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
    private void Spawn(GameObject prefab, Vector2 pos, EColorType color, string name)
    {
#if UNITY_EDITOR
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        obj.gameObject.SetActive(true);
        SpriteRenderer icon = obj.GetComponent<SpriteRenderer>();
        icon.color = GameData.Instance.ColorData.GetData(color).color;
        obj.transform.position = pos;
        obj.gameObject.name = name;
#endif
    }
}
