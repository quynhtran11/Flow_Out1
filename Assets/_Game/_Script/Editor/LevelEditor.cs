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

        EditorGUILayout.LabelField("Editing: " + levelTool.name);
        EditorGUILayout.Space(10);
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

        }
        if (CreateButton("CLEAR ", 40, Color.white, Color.white))
        {
            Clear();
        }
    }
    private void CupGUI(SerializedObject serializedObj)
    {
        bool isEditing = levelTool.isEditCup;
        string edit = "CUP " + (isEditing ? "EDITTING" : "EDIT");

        Color c = isEditing ? Color.green : Color.white;
        if (CreateButton(edit, 40, Color.white, c))
        {
            levelTool.isEditCup = !levelTool.isEditCup;
        }
        ViewStat(serializedObj);
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
