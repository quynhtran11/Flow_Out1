using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
public static class JsonManager
{

    public static void GetLevelInfor(int index, Action<LevelInfor> callBack)
    {
        string name = index.ToString("000");
#if UNITY_EDITOR
        if (GameData.Instance.LoadResouces)
        { // load resouces
            string path = $"Data/Level/Level_{name}";
            string json = Resources.Load<TextAsset>(path).text;
            LevelInfor levelInfor = JsonUtility.FromJson<LevelInfor>(json);
            callBack?.Invoke(levelInfor);
        }
        else
        { // load path 
            string path = Application.dataPath + "/_Game/Data/Level/Level_" + name + ".json";
            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                LevelInfor levelInfor = JsonUtility.FromJson<LevelInfor>(json);
                callBack?.Invoke(levelInfor);
                Debug.LogError("2b_" + levelInfor.LevelID);
                return;
            }
            else
            {
                Debug.LogError($"not find path level {name}");
            }
        }
#else
        if (GameData.Instance.LoadResouces)
        { // load resouces
            string path = $"Data/Level/Level_{name}";
            string json = Resources.Load<TextAsset>(path).text;
            LevelInfor levelInfor = JsonUtility.FromJson<LevelInfor>(json);
            callBack?.Invoke(levelInfor);
        }
        else{ // load addressable

        //AdressableManager.GetObject<TextAsset>(Constans.PathLevel + name, (text) =>
        //{
        //    if (text == null) return;
        //    LevelInfor levelInfor = JsonUtility.FromJson<LevelInfor>(text.text);
        //    callBack?.Invoke(levelInfor);
        //});
        }
#endif
    }
    public static void SaveLevelInfor(LevelInfor level)
    {
        string name = level.LevelID.ToString("000");
        string json = JsonUtility.ToJson(level, true);

        string filePath = Application.dataPath + $"/_Game/Data/Level/Level_{name}.json";

        string directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllText(filePath, json);
        Debug.Log($" Game saved to: {filePath}");
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
    public static LevelInfor LoadLevelInfor(int levelID)
    {
        string name = levelID.ToString("000");
        string filePath = Application.dataPath + $"/_Game/Data/Level/Level_{name}.json";
        string text = File.ReadAllText(filePath);
        LevelInfor lv = JsonUtility.FromJson<LevelInfor>(text);
        return lv;

    }
}
