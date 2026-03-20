using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public partial class LevelEditor : EditorWindow
{
    Vector2 scrollPos;
    [MenuItem("Water_Loop/LevelTool")]
    public static void ShowWindow()
    {
        GetWindow<LevelEditor>("Level Editor");
    }
    void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos,false,true);

        GUILayout.Label("Level Editor", EditorStyles.boldLabel);
        SetupLevelTool();
        SerializedObject serializedObj = new SerializedObject(levelTool);
        LevelGUI();
        CupGUI(serializedObj);
        StorageGUI(serializedObj);
        WaterGUI(serializedObj);
        serializedObj.ApplyModifiedProperties();
        EditorGUILayout.EndScrollView();
    }
    private void SetupLevelTool()
    {
        levelTool = (LevelTool)EditorGUILayout.ObjectField(
            "Level Tool",
            levelTool,
            typeof(LevelTool),
            true
        );
        levelTool.forDev = EditorGUILayout.Toggle("forDev", levelTool.forDev);

        EditorGUILayout.LabelField("Editing: " + levelTool.name);
        EditorGUILayout.Space(20);

    }
    private void LevelGUI()
    {
        levelID = EditorGUILayout.IntField("LevelID", levelID);
        mode = (EModeType)EditorGUILayout.EnumPopup("Mode", mode);

        if (CreateButton("LOAD " +levelID, 40, Color.white, Color.white))
        {
            Load();
        }
        if (CreateButton("SAVE " + levelID, 40, Color.white, Color.white))
        {
            Save();
        }
        if (CreateButton("CLEAR ", 40, Color.white, Color.white))
        {
            Clear();
        }
    }
    private void CupGUI(SerializedObject serializedObj)
    {
        ViewStat(serializedObj);
        levelTool.isEditCup = CreateProperties("EDITTING_CUP", "EDIT_CUP", levelTool.isEditCup);
        if (!levelTool.isEditCup)
        {
            levelTool.isSpawnCup = false;
            levelTool.isRemoveCup = false;
            levelTool.isColorCup = false;
        }
        else
        {
            levelTool.isEditStorage = false;
            levelTool.isEditWater = false;
        }
        CupProperties();
    }
    private void StorageGUI(SerializedObject serializedObj)
    {
        ViewStatStorage(serializedObj);
        levelTool.isEditStorage = CreateProperties("EDITTING_STORAGE", "EDIT_STORAGE", levelTool.isEditStorage);
        if (!levelTool.isEditStorage)
        {
            levelTool.isRemoveStorage = false;
        }
        else
        {
            levelTool.isEditCup = false;
            levelTool.isEditWater = false;
        }
        StorageProperties();
    }
    private void WaterGUI(SerializedObject serializedObj)
    {
        levelTool.isEditWater = CreateProperties("EDITTING_WATER", "EDIT_WATER", levelTool.isEditWater);
        if (!levelTool.isEditWater)
        {
            levelTool.isSpawnWater = false;
            levelTool.isRemoveWater = false;
            levelTool.isColorWater = false;
            levelTool.isHiddenWater = false;
            levelTool.isFreezeWater = false;
        }
        else
        {
             levelTool.isEditCup = false;
             levelTool.isEditStorage = false;
        }
        WaterProperties(serializedObj);
    }
    private bool CreateButton(string name, float size, Color co, Color backColor)
    {
        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        Color backColorTemp = GUI.backgroundColor;
        GUI.backgroundColor = backColor;

        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = co;
        style.fontStyle = FontStyle.Bold;
        bool pressed = GUILayout.Button(name, style, GUILayout.Height(size));
        GUI.backgroundColor = backColorTemp;
        EditorGUILayout.EndHorizontal();
        return pressed;
    }
}
