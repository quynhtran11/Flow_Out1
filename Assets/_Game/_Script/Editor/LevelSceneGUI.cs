using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(LevelTool))]
public class LevelSceneGUI : Editor
{
    private LevelTool tool;
    private void OnSceneGUI()
    {
        Event e = Event.current;
        tool = (LevelTool)target;

        if (tool.IsEditCup())
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            if (e.type == EventType.MouseUp)
            {
                Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                float z = 0f;
                float distance = (z - ray.origin.z) / ray.direction.z;
                Vector3 worldPos = ray.origin + ray.direction * distance;

                Collider2D hitCollider = Physics2D.OverlapPoint(worldPos);
                if (hitCollider == null || !hitCollider.CompareTag("Cup")) return;
                ChangePropertiesVisualTool(hitCollider.gameObject);
                ChangePropertiesTool(hitCollider.gameObject);
                e.Use();
            }
        }
    }
    private void ChangePropertiesTool(GameObject go)
    {
        if (go == null || tool == null ) return;
        CupData data = new CupData();
        List<CupData> allDatas = new List<CupData>(tool.AllCups);
        for (int i = 0; i < tool.AllCups.Count; i++)
        {
            if (Vector2.Distance(tool.AllCups[i].pos, go.transform.position) <= .1f)
            {
                data = tool.AllCups[i];
                tool.AllCups.RemoveAt(i);
                break;
            }
        }
        if (tool.isHiddenCup)
        {
            data.hiddenData.isHidden = true;
            allDatas.Add(data);
            Debug.LogError("hidden");
        }
        if (tool.isToggleCup)
        {

        }
        tool.AllCups.Add(data);
    }
    private void ChangePropertiesVisualTool(GameObject go)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Cup");
        GameObject final = null;
        if (gos == null || gos.Length <= 0 || go == null) return;
        for (int i = 0; i < gos.Length; i++)
        {
            if(Vector3.Distance(go.transform.position, gos[i].transform.position) <= .1f)
            {
                final = gos[i];
                break;
            }
        }
        if (final == null) return;
        if (tool.isHiddenCup)
        {
            Debug.LogError("hidden");
            
        }
        if (tool.isToggleCup)
        {
            Debug.LogError("game_" + final.gameObject.name);
        }
    }
}
