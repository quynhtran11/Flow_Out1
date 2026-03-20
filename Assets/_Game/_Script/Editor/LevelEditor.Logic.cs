using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public partial class LevelEditor
{
    int levelID;
    EModeType mode;

    LevelTool levelTool;
    public void Clear()
    {
        levelID = 1;
        mode = EModeType.Easy;
        levelTool.Clear();
    }
    public void Load()
    {
        levelTool.Load(levelID);
    }
    public void Save()
    {
        levelTool.Save(levelID, mode);
    }
    public void ViewStat(SerializedObject serializedObj)
    {
        if (levelTool == null) return;
        EditorGUILayout.LabelField("CUP INFOR: ");
        if (levelTool == null) return;
        if (levelTool.forDev)
        {
            SerializedProperty cupProperties = serializedObj.FindProperty("allCups");
            serializedObj.Update();
            EditorGUILayout.PropertyField(cupProperties, true);
        }
        // 
        float buttonSize = 50;
        float spacing = 5;

        float windowWidth = position.width - 20;
        int column = Mathf.FloorToInt(windowWidth / (buttonSize + spacing));
        column = Mathf.Max(column, 1);

        Dictionary<EColorType, int> dicCups = levelTool.GetCupAmount();
        Dictionary<EColorType, int> dicWater = levelTool.GetWaterAmount();
        Dictionary<EColorType, int> dicNew = new Dictionary<EColorType, int>();

        foreach (var item in dicWater)
        {
            float waterAmount = item.Value;

            int neededCups = (int)Mathf.CeilToInt(waterAmount / 3.0f);

            int currentCups = dicCups.ContainsKey(item.Key) ? dicCups[item.Key] : 0;

            int remain = neededCups - currentCups;

            dicNew.Add(item.Key, remain);
        }
        int index = 0;

        foreach (var item in dicNew)
        {
            if (index % column == 0)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
            }

            Color c = GameData.Instance.ColorData.GetData(item.Key).color;

            string text = item.Key.ToString() + "\n" + item.Value;

            CreateButtonHeighWith(text, buttonSize, buttonSize, c);

            if (index % column == column - 1)
            {
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

            index++;
        }

        if (index % column != 0)
        {
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
    }
    public void ViewStatStorage(SerializedObject serializedObj)
    {
        EditorGUILayout.LabelField("STORAGE INFOR: ");
        if (levelTool == null) return;
        if (levelTool.forDev)
        {
            SerializedProperty cupProperties = serializedObj.FindProperty("allStorages");
            serializedObj.Update();
            EditorGUILayout.PropertyField(cupProperties, true);
        }
        float buttonSize = 50;
        float spacing = 5;

        float windowWidth = position.width - 20;
        int column = Mathf.FloorToInt(windowWidth / (buttonSize + spacing));
        column = Mathf.Max(column, 1);
        Dictionary<EColorType, int> dicCups = levelTool.GetWaterAmount();
        int index = 0;

        foreach (var item in dicCups)
        {
            if (index % column == 0)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
            }

            Color c = GameData.Instance.ColorData.GetData(item.Key).color;

            string text = item.Key.ToString() + "\n" + item.Value + "\n" + (item.Value % 3 == 0 ? "true" : "false");

            CreateButtonHeighWith(text, buttonSize, buttonSize, c);

            if (index % column == column - 1)
            {
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

            index++;
        }

        if (index % column != 0)
        {
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
    }
    private void CreateSquare(string name, Color c, EColorType type)
    {
        if (CreateButton(name, 40, Color.white, c))
        {
            levelTool.cupColor = type;
        }
    }
    private void CreateSquareColorWater(string name, Color c, EColorType type)
    {
        if (CreateButton(name, 40, Color.white, c))
        {
            levelTool.waterColor = type;
        }
    }
    private bool CreateProperties(string name, string name2, bool isEdit)
    {
        bool isEditing = isEdit;
        string edit = (isEditing ? name : name2);
        Color c = isEditing ? Color.green : Color.white;
        if (CreateButton(edit, 40, Color.white, c))
        {
            return !isEditing;
        }
        return isEditing;
    }
    private void CupProperties()
    {
        if (!levelTool.isEditCup) return;
        if (CreateButton("AUTOSPAWN_CUP", 40, Color.white, Color.white))
        {
            levelTool.SpawnCup(levelTool.GetAutoPosCup(), levelTool.AllCups.Count);
        }
        levelTool.isSpawnCup = CreateProperties("SPAWNING_CUP", "SPAWN_CUP", levelTool.isSpawnCup);
        if (levelTool.isSpawnCup)
        {

        }
        levelTool.isRemoveCup = CreateProperties("REMOVING_CUP", "REMOVE_CUP", levelTool.isRemoveCup);
        if (levelTool.isRemoveCup)
        {
        }
        levelTool.isColorCup = CreateProperties("COLORING", "COLOR", levelTool.isColorCup);
        if (levelTool.isColorCup)
        {
            EditorGUILayout.LabelField("--------------");
            for (int i = 1; i < (int)EColorType.Black; i++)
            {
                EColorType name = (EColorType)i;
                string desc = name.ToString();
                if (name == levelTool.cupColor)
                {
                    desc += "_Use";
                }
                CreateSquare(desc, GameData.Instance.ColorData.GetData(name).color, name);
            }
            EditorGUILayout.LabelField("--------------");
        }
        levelTool.isHiddenCup = CreateProperties("HIDDING", "HIDDEN", levelTool.isHiddenCup);
        if (levelTool.isHiddenCup)
        {
            //hiddenAmount = EditorGUILayout.IntField("HiddenAmount", hiddenAmount);
        }
        levelTool.isToggleCup = CreateProperties("TOGGING", "TOGGLE", levelTool.isToggleCup);
        if (levelTool.isToggleCup)
        {

        }
    }
    private void StorageProperties()
    {
        if (!levelTool.isEditStorage) return;
        if (CreateButton("AUTOSPAWN_STORAGE", 40, Color.white, Color.white))
        {
            levelTool.SpawnStorage(levelTool.AllStorages.Count);
        }
        levelTool.isRemoveStorage = CreateProperties("REMOVING_STORAGE", "REMOVE_STORAGE", levelTool.isRemoveStorage);
    }
    private void WaterProperties(SerializedObject serializedObj)
    {
        if (!levelTool.isEditWater) return;
        levelTool.isSpawnWater = CreateProperties("SPAWNING_WATER", "SPAWN_WATER", levelTool.isSpawnWater);
        if (levelTool.isSpawnWater)
        {

        }
        levelTool.isRemoveWater = CreateProperties("REMOVING_WATER", "REMOVE_WATER", levelTool.isRemoveWater);
        if (levelTool.isRemoveWater)
        {

        }

        levelTool.isColorWater = CreateProperties("COLORING", "COLOR", levelTool.isColorWater);
        if (levelTool.isColorWater)
        {
            EditorGUILayout.LabelField("--------------");
            for (int i = 1; i < (int)EColorType.Black; i++)
            {
                EColorType name = (EColorType)i;
                string desc = name.ToString();
                if (name == levelTool.waterColor)
                {
                    desc += "_Use";
                }
                CreateSquareColorWater(desc, GameData.Instance.ColorData.GetData(name).color, name);
            }
            EditorGUILayout.LabelField("--------------");
        }
        levelTool.isHiddenWater = CreateProperties("HIDDING", "HIDDEN", levelTool.isHiddenWater);
        if (levelTool.isHiddenWater)
        {
        }
        levelTool.isFreezeWater = CreateProperties("FREEZING", "FREEZE", levelTool.isFreezeWater);
        if (levelTool.isFreezeWater)
        {
            SerializedProperty freezeProp = serializedObj.FindProperty("FreezeWater");
            EditorGUILayout.PropertyField(freezeProp, true);
        }
        EditorGUILayout.Space(20);
    }
    private bool CreateButtonHeighWith(string name, float sizeX, float sizeY, Color c)
    {
        EditorGUILayout.Space(10);
        Color backColorTemp = GUI.backgroundColor;
        GUI.backgroundColor = c;

        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = Color.white;
        style.fontStyle = FontStyle.Bold;
        bool pressed = GUILayout.Button(name, style, GUILayout.Height(sizeY), GUILayout.Width(sizeX));
        GUI.backgroundColor = backColorTemp;
        return pressed;
    }
}
