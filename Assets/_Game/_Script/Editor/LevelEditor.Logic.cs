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

    public void ViewStat(SerializedObject serializedObj)
    {
        if (levelTool == null) return;
        EditorGUILayout.LabelField("CUP INFOR: ");
        if (levelTool == null) return;
        SerializedProperty cupProperties = serializedObj.FindProperty("allCups");
        serializedObj.Update();
        EditorGUILayout.PropertyField(cupProperties, true);
        // 
        float buttonSize = 50;
        float spacing = 5;

        float windowWidth = position.width - 20;
        int column = Mathf.FloorToInt(windowWidth / (buttonSize + spacing));
        column = Mathf.Max(column, 1);

        Dictionary<EColorType, int> dicCups = levelTool.GetCupAmount();

        int index = 0;

        foreach (var item in dicCups)
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
    private bool CreateProperties(string name,string name2,bool isEdit)
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
        levelTool.isHiddenCup = CreateProperties("HIDDING", "HIDDEN", levelTool.isHiddenCup);
        levelTool.isToggleCup = CreateProperties("TOGGING", "TOGGLE", levelTool.isToggleCup);
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
