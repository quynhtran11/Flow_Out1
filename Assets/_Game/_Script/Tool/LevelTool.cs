using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public partial class LevelTool : MonoBehaviour
{
    public void Load(int index)
    {
        LevelInfor lv = JsonManager.LoadLevelInfor(index);
        allCups = lv.AllCups.ToList();
        Debug.LogError(lv.Map);
        for (int i = 0; i < lv.AllCups.Length; i++)
        {
            Spawn(toolVisual.CupPrefab(), lv.AllCups[i].pos,lv.AllCups[i].color);
        }
    }
    public void Save()
    {
    }
    public void Clear()
    {
        GameObject[] allCup = GameObject.FindGameObjectsWithTag("Cup");
        for (int i = 0; i < allCup.Length; i++)
        {
            DestroyImmediate(allCup[i]);
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
    private void Spawn(GameObject prefab,Vector2 pos,EColorType color)
    {
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        obj.gameObject.SetActive(true);
        SpriteRenderer icon  = obj.GetComponent<SpriteRenderer>();
        icon.color = GameData.Instance.ColorData.GetData(color).color;
        obj.transform.position = pos;
    }
}
