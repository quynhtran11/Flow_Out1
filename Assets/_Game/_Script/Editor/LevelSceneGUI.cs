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

        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        if (e.type == EventType.MouseUp)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            float z = 0f;
            float distance = (z - ray.origin.z) / ray.direction.z;
            Vector3 worldPos = ray.origin + ray.direction * distance;
            if (tool.IsEditCup())
            {
                if (!tool.isSpawnCup)
                {
                    Collider2D hitCollider = Physics2D.OverlapPoint(worldPos);
                    if (hitCollider == null || !hitCollider.CompareTag("Cup")) return;
                    if (tool.isRemoveCup)
                    {
                        Debug.LogError("remove");
                        tool.RemoveCup(hitCollider.gameObject);
                    }
                    else
                    {
                        Debug.LogError("spawn");

                        ChangeCupPropertiesVisualTool(hitCollider.gameObject);
                        ChangeCupPropertiesTool(hitCollider.gameObject);
                    }
                }
                else
                {
                    Collider2D hitCollider = Physics2D.OverlapPoint(worldPos);
                    if (hitCollider != null && hitCollider.CompareTag("Cup"))
                    {
                        worldPos = hitCollider.transform.position;
                        tool.SpawnPerCup(worldPos, true);
                    }
                    else
                    {
                        tool.SpawnPerCup(worldPos, false);
                    }
                }
            }
            if (tool.IsEditStorage())
            {
                Collider2D[] hitCollider = Physics2D.OverlapPointAll(worldPos);
                for (int i = 0; i < hitCollider.Length; i++)
                {
                    if (hitCollider[i] == null || !hitCollider[i].CompareTag("Storage")) continue;
                    if (tool.isRemoveStorage)
                    {
                        Debug.LogError("remove");
                        tool.RemoveStorage(hitCollider[i].gameObject);
                    }
                }
            }
            if (tool.IsEditWater())
            {
                Collider2D[] hitCollider = Physics2D.OverlapPointAll(worldPos);
                for (int i = 0; i < hitCollider.Length; i++)
                {
                    if (hitCollider != null && hitCollider[i].CompareTag("Storage"))
                    {
                        if (tool.isSpawnWater)
                        {
                            tool.SpawnPerWater(tool.AllStorages.Count, hitCollider[i].transform.position);
                        }
                    }
                    if (hitCollider == null || !hitCollider[i].CompareTag("Water")) continue;
                    if (tool.isRemoveWater)
                    {
                        tool.RemoveWater(hitCollider[i].gameObject);
                    }
                    else
                    {
                        ChangeWaterPropertiesTool(hitCollider[i].gameObject);
                        ChangeWaterPropertiesVisualTool(hitCollider[i].gameObject);
                    }
                }
            }
            e.Use();
        }
    }
    private void ChangeWaterPropertiesTool(GameObject go)
    {
        if (go == null || tool == null) return;
        int idGroup = (int)go.transform.position.x / (int)tool.offsetStorage;
        if (tool.AllStorages.Count <= 0 || idGroup >= tool.AllStorages.Count) return;
        int id = ((int)go.transform.position.y / 4) - 1;
        WaterData water = tool.AllStorages[idGroup].waterDatas[id];
        if (tool.isColorWater)
        {
            water.color = tool.waterColor;
        }
        if (tool.isHiddenWater)
        {
            water.hiddenData.isHidden = true;
        }
        tool.AllStorages[idGroup].waterDatas[id] = water;
    }
    private void ChangeCupPropertiesTool(GameObject go)
    {
        if (go == null || tool == null) return;
        CupData data = new CupData();
        for (int i = 0; i < tool.AllCups.Count; i++)
        {
            if (Vector2.Distance(tool.AllCups[i].pos, go.transform.position) <= .1f)
            {
                data = tool.AllCups[i];
                tool.AllCups.RemoveAt(i);
                break;
            }
        }
        if (tool.isColorCup)
        {
            data.color = tool.cupColor;
        }
        if (tool.isHiddenCup)
        {
            data.hiddenData.isHidden = true;
        }
        if (tool.isToggleCup)
        {

        }
        tool.AllCups.Add(data);
    }
    private void ChangeCupPropertiesVisualTool(GameObject go)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Cup");
        GameObject final = null;
        if (gos == null || gos.Length <= 0 || go == null) return;
        for (int i = 0; i < gos.Length; i++)
        {
            if (Vector3.Distance(go.transform.position, gos[i].transform.position) <= .1f)
            {
                final = gos[i];
                break;
            }
        }
        if (final == null) return;
        if (tool.isColorCup)
        {
            SpriteRenderer s = final.GetComponent<SpriteRenderer>();
            s.color = GameData.Instance.ColorData.GetData(tool.cupColor).color;
        }

        if (tool.isHiddenCup)
        {
            GameObject hid = tool.ToolVisual.HiddenWaterPrefab();
            Debug.LogError(hid.gameObject.name);
            Debug.LogError(final.gameObject.name);
            hid.transform.position = final.transform.position;
        }
        if (tool.isToggleCup)
        {
            Debug.LogError("game_" + final.gameObject.name);
        }
    }

    private void ChangeWaterPropertiesVisualTool(GameObject go)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Water");
        GameObject final = null;
        if (gos == null || gos.Length <= 0 || go == null) return;
        for (int i = 0; i < gos.Length; i++)
        {
            if (Vector3.Distance(go.transform.position, gos[i].transform.position) <= .1f)
            {
                final = gos[i];
                break;
            }
        }
        if (final == null) return;
        if (tool.isColorWater)
        {
            SpriteRenderer s = final.GetComponent<SpriteRenderer>();
            Debug.LogError(s == null);
            s.color = GameData.Instance.ColorData.GetData(tool.waterColor).color;
        }
        if (tool.isHiddenWater)
        {
            GameObject hid = tool.ToolVisual.HiddenWaterPrefab();
            hid.transform.position = final.transform.position;
        }
        if (tool.isFreezeWater)
        {

        }
    }

}
