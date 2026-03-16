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
    public void Save(int index,EModeType type)
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
    private void Spawn(GameObject prefab,Vector2 pos,EColorType color,string name)
    {
#if UNITY_EDITOR
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        obj.gameObject.SetActive(true);
        SpriteRenderer icon  = obj.GetComponent<SpriteRenderer>();
        icon.color = GameData.Instance.ColorData.GetData(color).color;
        obj.transform.position = pos;
        obj.gameObject.name = name;
#endif
    }
}
